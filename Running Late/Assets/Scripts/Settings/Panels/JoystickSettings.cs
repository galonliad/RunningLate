using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickSettings : SettingsPanel_base
{
    [SerializeField]
    Settings_float handleRange, deadZone;

    [SerializeField]
    Settings_int snapX;

    Joystick joystick;

    protected override void Start()
    {
        base.Start();

        joystick = FindObjectOfType<Joystick>();

        handleRange.SetStat(level.levelData.handleRange, SetHandleRange);
        deadZone.SetStat(level.levelData.deadZone, SetDeadZone);

        if (level.levelData.snapX == true)
        {
            snapX.SetStat(1, SetSnapX);
        }
        else
        {
            snapX.SetStat(0, SetSnapX);
        }

        Button_Done();
    }

    void SetHandleRange(float stat)
    {
        level.levelData.handleRange = (int)stat;
    }

    void SetDeadZone(float stat)
    {
        level.levelData.deadZone = (int)stat;
    }

    void SetSnapX(float stat)
    {
        if (stat > 0)
        {
            level.levelData.snapX = true;
            return;
        }

        level.levelData.snapX = false;
    }

    public void Button_Done()
    {
        joystick.HandleRange = level.levelData.handleRange;
        joystick.DeadZone = level.levelData.deadZone;
        joystick.SnapX = level.levelData.snapX;
    }
}
