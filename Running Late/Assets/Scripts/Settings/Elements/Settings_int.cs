using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_int : Settings_UiElement
{
    public int Stat { get { return stat; } }
    int stat;

    public void SetStat(int value, onValueChange_delegate del)
    {
        stat = value;
        stat_Txt.text = stat.ToString();
        this.del = del;
    }

    public void ChangeStat(int addValue)
    {
        stat += addValue;
        stat_Txt.text = stat.ToString();
        del(stat);
    }
}
