using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Level Level { get { return level; } }
    [SerializeField]
    Level level;
    CitySegment[] levelSegments;

    GuiManager ui;
    Player player;

    int currentSegment;
    float levelTime, carSpeed, nextSegmentZ;
    bool gamePaused;


    void Start()
    {
        levelTime = level.levelData.levelTime_sec;
        carSpeed = level.levelData.carSpeed;

        ui = FindObjectOfType<GuiManager>();
        player = FindObjectOfType<Player>();       

        var gen = GetComponent<LevelGenerator>();
        levelSegments = gen.CreateLevel(level);
        nextSegmentZ = levelSegments[1].transform.position.z;       

        PauseGame_Button(true);
    }

    void FixedUpdate()
    {
        if (!gamePaused)
        {
            levelTime -= Time.fixedDeltaTime;

            if (currentSegment < levelSegments.Length - 1)
            {
                SegmentUpdate();
            }

            if (player.transform.position.z > nextSegmentZ)
            {
                NextSegmentStart();
            }
        }        
    }

    void SegmentUpdate()
    {
        foreach (var car in levelSegments[currentSegment].cars)
        {
            car.transform.Translate(-car.transform.TransformDirection(car.transform.forward) * carSpeed * Time.fixedDeltaTime);
        }       
    }

    void NextSegmentStart()
    {
        currentSegment++;

        if (currentSegment < levelSegments.Length - 1)
        {
            nextSegmentZ = levelSegments[currentSegment + 1].transform.position.z;

            return;
        }

        LevelEnd();
    }

    void LevelEnd()
    {
        PauseGame_Button(true);

        if (levelTime > 0)
        {
            ui.LevelEnd(true);

            player.OnTime();

            return;
        }

        ui.LevelEnd(false);

        player.Late();
    }

    #region UI Buttons

    public void ResetLevel_Button()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit_Button()
    {
        Application.Quit();
    }

    public void PauseGame_Button(bool value)
    {
        player.PauseGame(value);

        ui.PauseGame(value);

        gamePaused = value;
    }

    #endregion
}
