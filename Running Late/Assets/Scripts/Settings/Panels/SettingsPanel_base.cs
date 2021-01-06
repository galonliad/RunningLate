using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel_base : MonoBehaviour
{
    protected Level level;

    protected virtual void Start()
    {
        level = FindObjectOfType<GameManager>().Level;
    }
}
