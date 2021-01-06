using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : SettingsPanel_base
{
    [SerializeField]
    Settings_int levelTime, segmentCount, movementDelta;
    [SerializeField]
    Settings_float carsSpeed;


    protected override void Start()
    {
        base.Start();

        levelTime.SetStat(level.levelData.levelTime_sec, SetLevelTime);
        segmentCount.SetStat(level.levelData.segmentsInLevel, SetSegmentCount);
        movementDelta.SetStat(level.levelData.movementDelta, SetMovementDelta);
        carsSpeed.SetStat(level.levelData.carSpeed, SetCarsSpeed);
    }

    void SetLevelTime(float stat)
    {
        level.levelData.levelTime_sec = (int)stat;
    }

    void SetSegmentCount(float stat)
    {
        level.levelData.segmentsInLevel = (int)stat;
    }

    void SetMovementDelta(float stat)
    {
        level.levelData.movementDelta = (int)stat;
    }

    void SetCarsSpeed(float stat)
    {
        level.levelData.carSpeed = stat;
    }
}
