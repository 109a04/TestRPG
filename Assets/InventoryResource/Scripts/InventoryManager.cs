using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryUI;
    public static InventoryManager Instance;
    public StoreManager storeManager;
    public List<Item> Items = new List<Item>(); //�s���~���
    public BaseItemController[] InventoryItems; //����ಾ

    public Transform itemsParent; //��l��������
    public GameObject itemPrefab; //��l��prefab
    public int maxCapacity = 20;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InventoryUI.SetActive(false);
        storeManager.OpenInventoryEvent += OpenInventory; // �q�\
    }

    private void OpenInventory()
    {
        InventoryUI.SetActive(true); // �}�Ұө��ɶ��K���}�I�]UI
        storeManager.OpenInventoryEvent -= OpenInventory; // �����q�\�A�קK���ƥ��}
        storeManager.CloseInventoryEvent += CloseInventory; //�q�\�����I�]�ƥ�
    }

    private void CloseInventory() //�P�W�A�u�O����
    {
        InventoryUI.SetActive(false);
        storeManager.CloseInventoryEvent -= CloseInventory;
        storeManager.OpenInventoryEvent += OpenInventory; // �q�\�}�ҭI�]�ƥ�A�o�˥i�H�@���ʦa�}���I�]�Ӥ��QUpdate�j�[(�I�]���������������k�����}�ҫ��䳣�ٯ�ϥ�)
    }

    public void AddItem(Item newItem)
    {
        Items.Add(newItem);
    }


    public void RemoveItem(Item itemToRemove)
    {
        if (itemToRemove.itemCounts.ContainsKey(itemToRemove.itemName))
        {
            itemToRemove.itemCounts[itemToRemove.itemName]--;

            if (itemToRemove.itemCounts[itemToRemove.itemName] <= 0)
            {
                ChatManager.Instance.SystemMessage($"�I�]�̪�{itemToRemove.itemName}�Χ��F�C");
                Items.Remove(itemToRemove);
            }
        }

        UpdateList();
    }


    public void UpdateList()
    {
        // �M���즳����l
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        InventoryItems = new BaseItemController[Items.Count]; // ��l�� InventoryItems

        // �b UI ����Ҥƪ��~���بó]�m��T

        for (int i = 0; i < Items.Count; i++)
        {


            GameObject obj = Instantiate(itemPrefab, itemsParent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCount = obj.transform.Find("ItemIcon/ItemCount").GetComponent<Text>();

            itemIcon.sprite = Items[i].itemIcon;
            itemCount.text = Items[i].itemCounts[Items[i].itemName].ToString();

            // �]�m InventoryItemController �� item
            InventoryItems[i] = obj.GetComponent<BaseItemController>();
            InventoryItems[i].SetItem(Items[i]);

        }
    }

}
