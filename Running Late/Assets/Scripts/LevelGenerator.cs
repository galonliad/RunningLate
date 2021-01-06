using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int SegmentLength { get { return segmentLength; } }

    Level level;
    CitySegment[] levelSegments;

    [SerializeField]
    CitySegment segmentCity_prefab, endSegment_prefab;

    [SerializeField]
    Collectible energy_prefab, cash_prefab, scooter_prefab, scooter_black_prefab;

    [SerializeField]
    int startSegmentOffset = -10;

    int segmentLength = 48;
    float segmentHalfWidth, nextSegmentSpawnPosZ;

    int energy_PickupsMax, energy_PickupsMin, cash_PickupsMax, cash_PickupsMin, scooter_PickupsMax, scooter_PickupsMin;
    float energy_AmountMax, energy_AmountMin, cash_AmountMax, cash_AmountMin;

    CitySegment segment_temp;
    Transform segmentTrans_temp;

    int[] gridSize, spawnPos_temp;
    bool[,] segmentGrid_temp;
    bool canSpawn_temp;


    public CitySegment[] CreateLevel(Level level)
    {
        this.level = level;

        SetLevelBaseCollectibles();

        CreateLevelSegments();

        CreateEndSegment();

        return levelSegments;
    }

    void SetLevelBaseCollectibles()
    {
        segmentHalfWidth = level.levelData.movementDelta * .5f;

        energy_PickupsMax = level.levelData.energy_basePickupsInSegment + level.levelData.energy_pickupsDelta + 1;
        energy_PickupsMin = level.levelData.energy_basePickupsInSegment - level.levelData.energy_pickupsDelta;
        if (energy_PickupsMin < 0)
            energy_PickupsMin = 0;

        energy_AmountMax = level.levelData.energy_baseAmount + level.levelData.energy_amountDelta;
        energy_AmountMin = level.levelData.energy_baseAmount - level.levelData.energy_amountDelta;
        if (energy_AmountMin < .1f)
            energy_AmountMin = .1f;


        cash_PickupsMax = level.levelData.cash_basePickupsInSegment + level.levelData.cash_pickupsDelta + 1;
        cash_PickupsMin = level.levelData.cash_basePickupsInSegment - level.levelData.cash_pickupsDelta;
        if (cash_PickupsMin < 0)
            cash_PickupsMin = 0;

        cash_AmountMax = level.levelData.cash_baseAmount + level.levelData.cash_amountDelta;
        cash_AmountMin = level.levelData.cash_baseAmount - level.levelData.cash_amountDelta;
        if (cash_AmountMin < .1f)
            cash_AmountMin = .1f;


        scooter_PickupsMax = level.levelData.scooter_basePickupsInSegment + level.levelData.scooter_pickupsDelta + 1;
        scooter_PickupsMin = level.levelData.scooter_basePickupsInSegment - level.levelData.scooter_pickupsDelta;
        if (scooter_PickupsMin < 0)
            scooter_PickupsMin = 0;
    }

    void CreateLevelSegments()
    {
        levelSegments = new CitySegment[level.levelData.segmentsInLevel + 2];

        spawnPos_temp = new int[2];
        gridSize = new int[2] { level.levelData.movementDelta, segmentLength };

        nextSegmentSpawnPosZ = startSegmentOffset;
        for (int i = 0; i < level.levelData.segmentsInLevel + 1; i++)
        {
            CreateSegment(i);
        }
    }

    void CreateSegment(int index)
    {
        segment_temp = Instantiate(segmentCity_prefab);
        segment_temp.name = "Segment " + index;

        segmentTrans_temp = segment_temp.transform;

        segmentGrid_temp = new bool[gridSize[0], gridSize[1]];

        if (index != 0)
            AddSegmentCollectibles();

        AddSegmentObstacles();

        segment_temp.transform.position = new Vector3(0, 0, nextSegmentSpawnPosZ);

        nextSegmentSpawnPosZ += segmentLength;
        levelSegments[index] = segment_temp;
    }

    void AddSegmentObstacles()
    {
        int avl = segment_temp.characters.Count;
        int charactersOff = UnityEngine.Random.Range(3, avl - 3);

        int index = -1;
        GameObject obj = null;
        for (int i = 0; i < charactersOff; i++)
        {
            index = UnityEngine.Random.Range(0, avl - i);

            obj = segment_temp.characters[index];
            segment_temp.characters.Remove(obj);
            Destroy(obj);
        }

        avl = segment_temp.cars.Count;
        for (int i = avl - 1; i >= 0; i--)
        {
            index = UnityEngine.Random.Range(0, 2);
            if (index == 0)
            {
                obj = segment_temp.cars[i];
                segment_temp.cars.Remove(obj);
                Destroy(obj);
            }
        }

        avl = segment_temp.cops.Count;
        for (int i = avl - 1; i >= 0; i--)
        {
            index = UnityEngine.Random.Range(0, 2);
            if (index == 0)
            {
                obj = segment_temp.cops[i];
                segment_temp.cops.Remove(obj);
                Destroy(obj);
            }
        }
    }

    void AddSegmentCollectibles()
    {
        AddCollectibles(CollectibleType.energy);

        AddCollectibles(CollectibleType.cash);

        AddCollectibles(CollectibleType.scooter);
    }

    void AddCollectibles(CollectibleType type)
    {
        int collectibles = 0;
        float amount = 0;

        Collectible prefab = GetCollectible(type);

        switch (type)
        {
            case CollectibleType.energy:
                collectibles = UnityEngine.Random.Range(energy_PickupsMin, energy_PickupsMax);
                amount = UnityEngine.Random.Range(energy_AmountMin, energy_AmountMax);
                break;

            case CollectibleType.cash:
                collectibles = UnityEngine.Random.Range(cash_PickupsMin, cash_PickupsMax);
                amount = UnityEngine.Random.Range(cash_AmountMin, cash_AmountMax);
                break;

            case CollectibleType.scooter:
                collectibles = UnityEngine.Random.Range(scooter_PickupsMin, scooter_PickupsMax);
                amount = 1;
                break;

            default:
                break;
        }

        Collectible collectible = null;
        for (int i = 0; i < collectibles; i++)
        {
            canSpawn_temp = SetSpawnPos();

            if (canSpawn_temp)
            {
                if (type == CollectibleType.scooter)
                {
                    prefab = GetCollectible(type);
                }

                collectible = Instantiate(prefab, segmentTrans_temp);
                collectible.transform.position = new Vector3(spawnPos_temp[0] - segmentHalfWidth, 0, spawnPos_temp[1]);
                collectible.amount = amount;
            }
            else
            {
                Debug.LogWarning("No Pos");
            }
        }
    }

    Collectible GetCollectible(CollectibleType type)
    {
        Collectible prefab = null;

        switch (type)
        {
            case CollectibleType.energy:
                prefab = energy_prefab;
                break;

            case CollectibleType.cash:
                prefab = cash_prefab;
                break;

            case CollectibleType.scooter:
                var black = UnityEngine.Random.Range(0, 2);
                if (black == 1)
                    prefab = scooter_black_prefab;
                else
                    prefab = scooter_prefab;
                break;

            default:
                break;
        }

        return prefab;
    }

    bool SetSpawnPos()
    {
        for (int i = 0; i < 50; i++)
        {
            SetRandom();

            if (!segmentGrid_temp[spawnPos_temp[0], spawnPos_temp[1]])
            {
                segmentGrid_temp[spawnPos_temp[0], spawnPos_temp[1]] = true;

                return true;
            }
        }

        return CheckGrid();
    }

    void SetRandom()
    {
        spawnPos_temp[0] = UnityEngine.Random.Range(0, gridSize[0]);
        spawnPos_temp[1] = UnityEngine.Random.Range(0, gridSize[1]);
    }

    bool CheckGrid()
    {
        for (int x = 0; x < gridSize[0]; x++)
        {
            for (int z = 0; z < gridSize[1]; z++)
            {
                if (!segmentGrid_temp[x,z])
                {
                    segmentGrid_temp[x,z] = true;

                    spawnPos_temp[0] = x;
                    spawnPos_temp[1] = z;

                    return true;
                }
            }
        }

        return false;
    }

    void CreateEndSegment()
    {
        segment_temp = Instantiate(endSegment_prefab);
        segment_temp.name = "End Segment ";
        segment_temp.transform.position = new Vector3(0, 0, nextSegmentSpawnPosZ);

        levelSegments[levelSegments.Length - 1] = segment_temp;
    }
}
