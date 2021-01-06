using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings_UiElement : MonoBehaviour
{
    public delegate void onValueChange_delegate(float value);

    protected onValueChange_delegate del;

    [SerializeField]
    protected TextMeshProUGUI stat_Txt;
}
