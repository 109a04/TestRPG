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
    public List<Item> Items = new List<Item>(); //存物品資料
    public BaseItemController[] InventoryItems; //資料轉移

    public Transform itemsParent; //格子的父物件
    public GameObject itemPrefab; //格子的prefab
    public int maxCapacity = 20;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InventoryUI.SetActive(false);
        storeManager.OpenInventoryEvent += OpenInventory; // 訂閱
    }

    private void OpenInventory()
    {
        InventoryUI.SetActive(true); // 開啟商店時順便打開背包UI
        storeManager.OpenInventoryEvent -= OpenInventory; // 取消訂閱，避免重複打開
        storeManager.CloseInventoryEvent += CloseInventory; //訂閱關閉背包事件
    }

    private void CloseInventory() //同上，只是關閉
    {
        InventoryUI.SetActive(false);
        storeManager.CloseInventoryEvent -= CloseInventory;
        storeManager.OpenInventoryEvent += OpenInventory; // 訂閱開啟背包事件，這樣可以一次性地開閉背包而不被Update綁架(背包本身的關閉按鍵跟右側的開啟按鍵都還能使用)
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
                ChatManager.Instance.SystemMessage($"背包裡的{itemToRemove.itemName}用完了。");
                Items.Remove(itemToRemove);
            }
        }

        UpdateList();
    }


    public void UpdateList()
    {
        // 清除原有的格子
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        InventoryItems = new BaseItemController[Items.Count]; // 初始化 InventoryItems

        // 在 UI 中實例化物品項目並設置資訊

        for (int i = 0; i < Items.Count; i++)
        {


            GameObject obj = Instantiate(itemPrefab, itemsParent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCount = obj.transform.Find("ItemIcon/ItemCount").GetComponent<Text>();

            itemIcon.sprite = Items[i].itemIcon;
            itemCount.text = Items[i].itemCounts[Items[i].itemName].ToString();

            // 設置 InventoryItemController 的 item
            InventoryItems[i] = obj.GetComponent<BaseItemController>();
            InventoryItems[i].SetItem(Items[i]);

        }
    }

}
