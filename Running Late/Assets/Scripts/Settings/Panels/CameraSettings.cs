using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


public class CameraSettings : SettingsPanel_base
{
    Camera cam;
    PositionConstraint camCons;

    Transform player;

    float horizontal;

    [SerializeField]
    float rotSpeed = 20, resetFOV = 69;

    delegate void settings_delegate();
    settings_delegate set;

    [SerializeField]
    Button yaw_button, pitch_button, distance_button, fov_button;
    Button currentButton;

    [SerializeField]
    Vector3 resetPos, resetEuler;


    protected override void Start()
    {
        base.Start();

        cam = Camera.main;
        camCons = cam.GetComponent<PositionConstraint>();

        player = camCons.GetSource(0).sourceTransform;
    }

    void RotateAroundPlayerY()
    {
        cam.transform.RotateAround(player.position, player.up, -horizontal * Time.deltaTime * rotSpeed);
    }

    void RotateAroundPlayerX()
    {
        cam.transform.RotateAround(player.position, cam.transform.right, horizontal * Time.deltaTime * rotSpeed);
    }

    void ChangeCamDistance()
    {
        var pos = cam.transform.localPosition;
        pos += horizontal * Time.deltaTime * rotSpeed * .5f * cam.transform.TransformDirection(-Vector3.forward);
        cam.transform.localPosition = pos;
    }

    void ChangeCamFOV()
    {
        cam.fieldOfView += horizontal * Time.deltaTime * rotSpeed;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (set != null && horizontal != 0)
        {
            set();
        }
    }

    public void SelectSetting(Button selectedButton)
    {
        if (currentButton != null)
        {
            currentButton.image.color = currentButton.colors.normalColor;
        }

        currentButton = selectedButton;

        currentButton.image.color = currentButton.colors.selectedColor;

        camCons.constraintActive = false;
        //camCons.locked = false;    
    }

    public void Button_Yaw_change()
    {
        SelectSetting(yaw_button);

        set = RotateAroundPlayerY;
    }

    public void Button_Pitch_change()
    {
        SelectSetting(pitch_button);

        set = RotateAroundPlayerX;
    }

    public void Button_Distance_change()
    {
        SelectSetting(distance_button);

        set = ChangeCamDistance;
    }
    public void Button_FOV_change()
    {
        SelectSetting(fov_button);

        set = ChangeCamFOV;
    }

    public void Button_Done()
    {
        if (currentButton != null)
        {
            currentButton.image.color = currentButton.colors.normalColor;
            currentButton = null;
        }

        set = null;

        SaveSettings();

        camCons.constraintActive = true;       
    }

    void SaveSettings()
    {
        level.levelData.cameraOffsetPos = camCons.translationOffset = cam.transform.position - player.position;
        level.levelData.cameraEuler = cam.transform.localEulerAngles;
        level.levelData.cameraFOV = cam.fieldOfView;
    }

    public void Button_Reset()
    {
        cam.transform.position = resetPos;
        cam.transform.eulerAngles = resetEuler;
        cam.fieldOfView = resetFOV;

        Button_Done();
    }
}
