using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
[Serializable]

public class Item : ScriptableObject
{

    public int id; //物品編號
    public string itemName; //物品名稱
    public Sprite itemIcon; //物品圖示
    public int itemPrice; //給商店用的商品售價
    public Dictionary<string, int> itemCounts = new Dictionary<string, int>(); // 存儲不同道具的數量
    public int itemValue; //物品數值
    [TextArea]
    public string itemDescript; //物品描述
    public ItemType itemType; //物品類別


    public enum ItemType
    {
        Fruit,
        Vegetable,
        Starch,
        Weapon
    }

}
