using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_float : Settings_UiElement
{
    public float Stat { get { return stat; } }
    float stat;

    public void SetStat(float value, onValueChange_delegate del)
    {
        stat = value;
        stat_Txt.text = stat.ToString();
        this.del = del;
    }

    public void ChangeStat(float addValue)
    {
        stat += addValue;
        stat_Txt.text = stat.ToString();
        del(stat);
    }
}
