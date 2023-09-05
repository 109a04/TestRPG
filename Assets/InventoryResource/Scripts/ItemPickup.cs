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
        //�ˬd�I�]�O�_�w��
        if (InventoryManager.Instance.Items.Count >= InventoryManager.Instance.maxCapacity)
        {
            Debug.Log("�I�]�w��!");
        }
        else //�p�G�S���~�i�H��
        {
            //�ˬd�O�_�w�s�b�ۦP�D��
            Item isExist = InventoryManager.Instance.Items.Find(item => item.id == itemToAdd.id);

            if (isExist != null)
            {
                //�ȼW�[���~�ƶq
                var existingItem = InventoryManager.Instance.Items.Find(item => item.id == itemToAdd.id);
                existingItem.itemCounts[itemToAdd.itemName]++;
            }
            else
            {
                itemToAdd.itemCounts[itemToAdd.itemName] = 1; //�߰_�Ӫ����~����l�Ƽƶq
                InventoryManager.Instance.AddItem(itemToAdd);
            }
            ChatManager.Instance.SystemMessage($"�A�B���F{itemToAdd.itemName}�C");
            InventoryManager.Instance.UpdateList();

            Destroy(gameObject);
        }
    }


    private void OnMouseDown()
    {
        Pickup();
    }
}
