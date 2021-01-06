using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stat_element : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI stat_Txt;

    public Stat stat;


    public void SetStat(float value, bool isFlat)
    {
        stat = new Stat(value, isFlat);
        GUIUpdate();
    }

    public void ChangeStat(float addValue)
    {
        stat.ChangeStat(addValue);
        GUIUpdate();
    }

    void GUIUpdate()
    {
        stat_Txt.text = stat.ToString();
    }
}


public class Stat
{
    public float Value { get { return GetValue(); } }
    float value;
    bool flat;

    public Stat(float startValue, bool isFlat)
    {        
        value = startValue;
        flat = isFlat;       
    }

    float GetValue()
    {
        if (flat)
            return (int)value;
        else
            return value;
    }

    public void ChangeStat(float addValue)
    {
        value += addValue;
    }
}
