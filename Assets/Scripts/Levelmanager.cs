using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelmanager : MonoBehaviour
{
    public static Levelmanager instance;

    [Header("kill target based on level")]
    [SerializeField] List<int> TotalKills;

    private void Awake()
    {
        instance = this;
    }

    public int killtarget()
    {
        int level = Helper.GetLevel();
        return TotalKills[level - 1];
    }


}
