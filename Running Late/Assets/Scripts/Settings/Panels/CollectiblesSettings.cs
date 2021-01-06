using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesSettings : SettingsPanel_base
{
    [SerializeField]
    Settings_float energyBaseAmount, energyAmountDelta, energyDropRate, cashBaseAmount, cashAmountDelta, cashDropRate, minCashToScooter;

    [SerializeField]
    Settings_int energyBasePicks, energyPicksDelta, cashBasePicks, cashPicksDelta, scootersInSegment, scootersPicksDelta;


    protected override void Start()
    {
        base.Start();

        energyBasePicks.SetStat(level.levelData.energy_basePickupsInSegment, SetEnergyBasePicks);
        energyPicksDelta.SetStat(level.levelData.energy_pickupsDelta, SetEnergyPicksDelta);
        energyBaseAmount.SetStat(level.levelData.energy_baseAmount, SetEnergyBaseAmount);
        energyAmountDelta.SetStat(level.levelData.energy_amountDelta, SetEnergyAmountDelta);
        energyDropRate.SetStat(level.levelData.energy_amountDelta, SetEnergyDropRate);

        cashBasePicks.SetStat(level.levelData.cash_basePickupsInSegment, SetCashBasePicks);
        cashPicksDelta.SetStat(level.levelData.cash_pickupsDelta, SetCashPicksDelta);
        cashBaseAmount.SetStat(level.levelData.cash_baseAmount, SetCashBaseAmount);
        cashAmountDelta.SetStat(level.levelData.cash_amountDelta, SetCashAmountDelta);
        cashDropRate.SetStat(level.levelData.cash_amountDelta, SetCashDropRate);

        scootersInSegment.SetStat(level.levelData.scooter_basePickupsInSegment, SetScootersInSegment);
        scootersPicksDelta.SetStat(level.levelData.scooter_pickupsDelta, SetScootersDelta);
        minCashToScooter.SetStat(level.levelData.scooter_pickupsDelta, SetMinCashScooter);
    }

    void SetEnergyBasePicks(float stat)
    {
        level.levelData.energy_basePickupsInSegment = (int)stat;
    }

    void SetEnergyPicksDelta(float stat)
    {
        level.levelData.energy_basePickupsInSegment = (int)stat;
    }

    void SetEnergyBaseAmount(float stat)
    {
        level.levelData.energy_baseAmount = (int)stat;
    }

    void SetEnergyAmountDelta(float stat)
    {
        level.levelData.energy_amountDelta = (int)stat;
    }

    void SetEnergyDropRate(float stat)
    {
        level.levelData.energyDrop = stat;
    }

    void SetCashBasePicks(float stat)
    {
        level.levelData.cash_basePickupsInSegment = (int)stat;
    }

    void SetCashPicksDelta(float stat)
    {
        level.levelData.cash_basePickupsInSegment = (int)stat;
    }

    void SetCashBaseAmount(float stat)
    {
        level.levelData.cash_baseAmount = (int)stat;
    }

    void SetCashAmountDelta(float stat)
    {
        level.levelData.cash_amountDelta = (int)stat;
    }

    void SetCashDropRate(float stat)
    {
        level.levelData.cashDrop = stat;
    }

    void SetScootersInSegment(float stat)
    {
        level.levelData.scooter_basePickupsInSegment = (int)stat;
    }

    void SetScootersDelta(float stat)
    {
        level.levelData.scooter_pickupsDelta = (int)stat;
    }

    void SetMinCashScooter(float stat)
    {
        level.levelData.minCashScooter = stat;
    }
}
