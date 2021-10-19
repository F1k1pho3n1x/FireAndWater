using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    private int TotalCrystalCount;
    private int CrystalCount;

    public Transform[] Unlockables;
    public bool[] BoughtUnlockables;

    public int bestScore;
    public int gamesPlayed;

    void Start()
    {
        bestScore = 0;
        gamesPlayed = 0;
        BoughtUnlockables = new bool[4];
        for (int i = 0; i < BoughtUnlockables.Length; i++)
        {
            BoughtUnlockables[i] = false;
        }
    }

    public void GainCrystal()
    {
        CrystalCount += 1;
    }

    public int GetCrystalCount()
    {
        return CrystalCount;
    }

    public void ResetCrystalCount()
    {
        CrystalCount = 0;
    }

    public void AddCrystalCountToTotal(int multiplierOrbCount)
    {
        CrystalCount = 100;
        TotalCrystalCount = CrystalCount;
    }
}
