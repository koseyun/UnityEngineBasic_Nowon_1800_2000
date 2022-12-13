using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new LevelData", menuName = "TowerDefence/Create LevelData")]
public class LevelData : ScriptableObject
{
    public int Level;
    public int LifeInit;
    public int MoneyInit;
    public List<StageData> StageDatas;
}

[Serializable]
public class StageData
{
    public List<SpawnData> SpawnDatas;
}

[Serializable]
public class SpawnData
{
    public GameObject Prefab;
    public int Num;
    public float Term;
    public float StartDeley;
    public int StartPointIndex;
    public int EndPointIndex;
}
