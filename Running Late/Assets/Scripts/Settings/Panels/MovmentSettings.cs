using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentSettings : SettingsPanel_base
{
    [SerializeField]
    Settings_float forwardSpeed_minFoot, forwardSpeed_maxFoot, horizontalSpeed_foot, forwardSpeed_scooter, horizontalSpeed_scooter;


    protected override void Start()
    {
        base.Start();

        forwardSpeed_minFoot.SetStat(level.levelData.forwardSpeed_minFoot, SetForwardSpeed_minFoot);
        forwardSpeed_maxFoot.SetStat(level.levelData.forwardSpeed_maxFoot, SetForwardSpeed_maxFoot);
        horizontalSpeed_foot.SetStat(level.levelData.horizontalSpeed_foot, SetHorizontalSpeed_foot);
        forwardSpeed_scooter.SetStat(level.levelData.forwardSpeed_scooter, SetForwardSpeed_scooter);
        horizontalSpeed_scooter.SetStat(level.levelData.horizontalSpeed_scooter, SetHorizontalSpeed_scooter);       
    }

    void SetForwardSpeed_minFoot(float stat)
    {
        level.levelData.forwardSpeed_minFoot = (int)stat;
    }

    void SetForwardSpeed_maxFoot(float stat)
    {
        level.levelData.forwardSpeed_maxFoot = (int)stat;
    }

    void SetHorizontalSpeed_foot(float stat)
    {
        level.levelData.horizontalSpeed_foot = (int)stat;
    }

    void SetForwardSpeed_scooter(float stat)
    {
        level.levelData.forwardSpeed_scooter = (int)stat;
    }

    void SetHorizontalSpeed_scooter(float stat)
    {
        level.levelData.horizontalSpeed_scooter = (int)stat;
    }
}
