using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
//using static UnityEditor.Progress;

public class ItemPickup : MonoBehaviour
{
    public Item itemToAdd;

    void Pickup()
    {
        //檢查背包是否已滿
        if (InventoryManager.Instance.Items.Count >= InventoryManager.Instance.maxCapacity)
        {
            Debug.Log("背包已滿!");
        }
        else //如果沒滿才可以裝
        {
            //檢查是否已存在相同道具
            Item isExist = InventoryManager.Instance.Items.Find(item => item.id == itemToAdd.id);

            if (isExist != null)
            {
                //僅增加物品數量
                var existingItem = InventoryManager.Instance.Items.Find(item => item.id == itemToAdd.id);
                existingItem.itemCounts[itemToAdd.itemName]++;
            }
            else
            {
                itemToAdd.itemCounts[itemToAdd.itemName] = 1; //撿起來的物品先初始化數量
                InventoryManager.Instance.AddItem(itemToAdd);
            }
            ChatManager.Instance.SystemMessage($"你拾取了{itemToAdd.itemName}。");
            InventoryManager.Instance.UpdateList();

            Destroy(gameObject);
        }
    }


    private void OnMouseDown()
    {
        Pickup();
    }
}
