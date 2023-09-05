using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
[Serializable]

public class Item : ScriptableObject
{

    public int id; //���~�s��
    public string itemName; //���~�W��
    public Sprite itemIcon; //���~�ϥ�
    public int itemPrice; //���ө��Ϊ��ӫ~���
    public Dictionary<string, int> itemCounts = new Dictionary<string, int>(); // �s�x���P�D�㪺�ƶq
    public int itemValue; //���~�ƭ�
    [TextArea]
    public string itemDescript; //���~�y�z
    public ItemType itemType; //���~���O


    public enum ItemType
    {
        Fruit,
        Vegetable,
        Starch,
        Weapon
    }

}
