using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public LevelData levelData;
}


[System.Serializable]
public class LevelData
{
    public int levelTime_sec;

    // Segments
    public int segmentsInLevel, movementDelta;
    public float carSpeed;

    // Energy
    public int energy_basePickupsInSegment, energy_pickupsDelta;
    public float energy_baseAmount, energy_amountDelta, energyDrop;
    public float forwardSpeed_minFoot, forwardSpeed_maxFoot, horizontalSpeed_foot;

    // Coins
    public int cash_basePickupsInSegment, cash_pickupsDelta;
    public float cash_baseAmount, cash_amountDelta, cashDrop;

    // Scooters
    public int scooter_basePickupsInSegment, scooter_pickupsDelta;
    public float forwardSpeed_scooter, horizontalSpeed_scooter, minCashScooter;

    // Camera
    public Vector3 cameraOffsetPos;
    public Vector3 cameraEuler;
    public float cameraFOV;

    // Joystick
    public float handleRange;
    public float deadZone;
    public bool snapX;
}
