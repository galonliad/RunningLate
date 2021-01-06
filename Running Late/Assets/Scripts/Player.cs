using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Energy { get { return energy; } }
    public float Cash { get { return cash; } }

    GameManager manager;
    AudioManager am;
    Animator anim;
    Joystick joystick;

    Rigidbody rb;
    Transform scooter;

    Collectible collect_temp;

    Vector3 movClamp;
    Vector3 rotClamp;

    float input_horizontal,
          xClamp,
          energy, eMax, eDropRate, cash, cDropRate,
          forwardSpeed_minFoot, horizontalSpeed_foot, forwardSpeed_scooter, horizontalSpeed_scooter,
          minCashToScooter,
          temp_forwardSpeed, temp_speed;

    bool onScooter;
    bool gamePaused;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        manager = FindObjectOfType<GameManager>();

        joystick = FindObjectOfType<Joystick>();

        am = FindObjectOfType<AudioManager>();

        CacheStats();

        energy = eMax;

        PauseGame(true);
    }

    public void CacheStats()
    {
        xClamp = manager.Level.levelData.movementDelta * .5f;

        horizontalSpeed_foot = manager.Level.levelData.horizontalSpeed_foot;
        forwardSpeed_minFoot = manager.Level.levelData.forwardSpeed_minFoot;

        forwardSpeed_scooter = manager.Level.levelData.forwardSpeed_scooter;
        horizontalSpeed_scooter = manager.Level.levelData.horizontalSpeed_scooter;

        eDropRate = manager.Level.levelData.energyDrop;
        cDropRate = manager.Level.levelData.cashDrop;

        eMax = manager.Level.levelData.forwardSpeed_maxFoot - manager.Level.levelData.forwardSpeed_minFoot;

        cash = minCashToScooter = manager.Level.levelData.minCashScooter;
    }

    void Update()
    {
        if (!gamePaused)
        {
            input_horizontal = joystick.Horizontal;
        }
    }

    void FixedUpdate()
    {
        if (!gamePaused)
        {
            anim.SetFloat("energy", energy / eMax);

            Move();

            Rotate();

            if (onScooter)
            {
                ChangeCash(-cDropRate * Time.fixedDeltaTime);
            }
            else
            {
                ChangeEnergy(-eDropRate * Time.fixedDeltaTime);
            }
        }       
    }

    void Move()
    {
        if (rb.velocity != Vector3.zero)        // quick patch for car collitions. TODO change :)
            rb.velocity = Vector3.zero;

        movClamp = rb.position;
        temp_speed = 0;
        temp_forwardSpeed = 0;

        if (onScooter)
        {
            temp_speed = horizontalSpeed_scooter;
            temp_forwardSpeed = forwardSpeed_scooter;
        }
        else
        {
            temp_speed = horizontalSpeed_foot;
            temp_forwardSpeed = forwardSpeed_minFoot + energy;
        }

        movClamp.x += input_horizontal * temp_speed * Time.fixedDeltaTime;
        movClamp.z += temp_forwardSpeed * Time.fixedDeltaTime;

        movClamp.x = Mathf.Clamp(movClamp.x, -xClamp, xClamp);

        rb.MovePosition(movClamp);
    }

    void Rotate()
    {
        temp_speed = horizontalSpeed_foot * 2;

        if (onScooter)
            temp_speed = horizontalSpeed_scooter * 2;

        rotClamp = Vector3.zero;

        if (input_horizontal != 0)
        {
            rotClamp = transform.rotation.eulerAngles;

            rotClamp.y = Mathf.Sign(input_horizontal) * 40;

            temp_speed *= .5f;
        }

        rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotClamp), Time.fixedDeltaTime * temp_speed));
    }

    void OnTriggerEnter(Collider other)
    {
        collect_temp = other.GetComponent<Collectible>();

        if (collect_temp != null)
        {
            Collect();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Car"))
        {
            rb.velocity = Vector3.zero;

            anim.SetTrigger("hit");

            am.CarHonk();
        }
    }

    void Collect()
    {
        am.Collect(collect_temp.type);

        float amount = collect_temp.amount;
        switch (collect_temp.type)
        {
            case CollectibleType.energy:
                ChangeEnergy(amount);
                break;

            case CollectibleType.cash:
                ChangeCash(amount);
                break;

            case CollectibleType.scooter:
                if (!onScooter && cash >= minCashToScooter)
                    Scooter_start();
                break;

            default:
                break;
        }

        Destroy(collect_temp.gameObject);

        collect_temp = null;
    }

    void ChangeEnergy(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0, eMax);
    }

    void ChangeCash(float amount)
    {
        cash += amount;

        if (cash < 0)
        {
            cash = 0;
            Scooter_end();
        }
    }

    void Scooter_start()
    {
        scooter = collect_temp.transform.GetChild(0);
        scooter.transform.rotation = transform.rotation;
        scooter.SetParent(transform);
        scooter.transform.localPosition = Vector3.zero;

        onScooter = true;

        anim.SetBool("onScooter", true);
    }

    void Scooter_end()
    {
        onScooter = false;

        scooter.SetParent(null);
        scooter = null;

        anim.SetBool("onScooter", false);
    }

    public void ResetEmax()
    {
        var currentPrecent = energy / eMax;
        eMax = manager.Level.levelData.forwardSpeed_maxFoot - manager.Level.levelData.forwardSpeed_minFoot;
        energy = eMax * currentPrecent;
    }

    public void PauseGame(bool value)
    {
        gamePaused = value;

        if (!value)
            anim.StopPlayback();
        else
            anim.StartPlayback();
    }

    public void OnTime()
    {
        rb.isKinematic = true;

        rb.velocity = Vector3.zero;

        anim.SetTrigger("onTime");
    }

    public void Late()
    {
        anim.SetTrigger("late");
    }
}
