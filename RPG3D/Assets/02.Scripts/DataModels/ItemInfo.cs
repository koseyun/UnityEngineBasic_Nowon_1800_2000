using System;
using System.Collections;
using System.Collections.Generic;
using ULB.RPG;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemInfo", menuName = "RPG/Create ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public int id;
    public string description;
    public int maxNum;
    public Sprite icon;
    public Item prefab;

    public void CreateNewHashCode()
    {
        HashCode.Combine("몇년몇월몇시몇분몇초" + "유저ID" + "유저닉네임" + "좌표" + "블라블라");
    }
}
