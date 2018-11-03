using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDataManager : MonoBehaviour {

    private static GameplayDataManager gameplayDataManager = new GameplayDataManager();
    public static GameplayDataManager getInstance()
    {
        return gameplayDataManager;
    }


    private List<bool> listOfUnlockedUnit;
    private int totalUnits;
    private int idEquipedUnit;
    private int totalMedals;
    private int highScore;

    public int TotalUnits
    {
        get
        {
            return totalUnits;
        }

        set
        {
            totalUnits = value;
        }
    }

    public int IdEquipedUnit
    {
        get
        {
            return idEquipedUnit;
        }

        set
        {
            idEquipedUnit = value;
        }
    }

    public int TotalMedals
    {
        get
        {
            return totalMedals;
        }

        set
        {
            totalMedals = value;
        }
    }

    public int HighScore
    {
        get
        {
            return highScore;
        }

        set
        {
            highScore = value;
        }
    }

    public void reset()
    {
        idEquipedUnit = 1;
        TotalUnits = 3;
        totalMedals = 1000;
        resetListOfUnlockedUnit();
    }

    private void resetListOfUnlockedUnit()
    {
        listOfUnlockedUnit = new List<bool>();
        for (int i = 0; i < TotalUnits; i++)
        {
            listOfUnlockedUnit.Add(false);
        }
        listOfUnlockedUnit[0] = true;
    }

    public bool isUnitUnlocked(int _idUnit)
    {
        return listOfUnlockedUnit[_idUnit - 1];
    }

    public void unlockUnit(int _idUnit)
    {
        listOfUnlockedUnit[_idUnit - 1] = true;
    }
}
