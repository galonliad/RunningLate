using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuiManager : MonoBehaviour
{
    [SerializeField]
    Slider eSlider, cSlider, tSlider;

    [SerializeField]
    Image eSliderFill, cSliderFill, tSliderFill;

    [SerializeField]
    Gradient eGrad, cGrad, tGrad;

    [SerializeField]
    GameObject endPanels, levelEnd_onTime, levelEnd_late;

    [SerializeField]
    float uiUpdateTime = .1f;

    WaitForSeconds waitUiUpdate;

    Player player;


    void Start()
    {
        waitUiUpdate = new WaitForSeconds(uiUpdateTime);

        player = FindObjectOfType<Player>();

        var level = FindObjectOfType<GameManager>().Level.levelData;

        tSlider.value = tSlider.maxValue = level.levelTime_sec;

        eSlider.maxValue = level.forwardSpeed_maxFoot - level.forwardSpeed_minFoot;

        cSlider.maxValue = 10;
    }

    IEnumerator UpdateGUI()
    {
        TimeSliderUpdate();

        EnergySliderUpdate();

        CashSliderUpdate();

        yield return waitUiUpdate;

        yield return UpdateGUI();
    }

    public void TimeSliderUpdate()
    {
        tSlider.value -= uiUpdateTime;
        tSliderFill.color = tGrad.Evaluate(tSlider.normalizedValue);
    } 

    public void EnergySliderUpdate()
    {
        eSlider.value = player.Energy;
        eSliderFill.color = eGrad.Evaluate(eSlider.normalizedValue);
    }

    public void CashSliderUpdate()
    {
        cSlider.value = player.Cash;
        cSliderFill.color = cGrad.Evaluate(cSlider.normalizedValue);
    }

    public void PauseGame(bool value)
    {
        if (value)
        {
            StopAllCoroutines();

            return;
        }

        StartCoroutine(UpdateGUI());
    }

    public void LevelEnd(bool onTime)
    {
        StopAllCoroutines();

        endPanels.SetActive(true);

        if (onTime)
        {
            levelEnd_onTime.gameObject.SetActive(true);
        }
        else
        {
            levelEnd_late.gameObject.SetActive(true);
        }
    }
}
