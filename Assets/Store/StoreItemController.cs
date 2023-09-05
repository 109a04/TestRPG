using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemController : BaseItemController
{
    public Text itemPriceText;
    public GameObject QuantityPanel;
    public Text QuantityText;
    public Text ItemNameText;
    public Text CanNotAffordText;
    private bool CanNotAfford = false;
    private static GameObject activeQuantityUI; // 保存當前已經彈出的數量選擇界面
    private int currentQuantity = 1; //初始數量

    protected override void Start()
    {
        base.Start();

        QuantityPanel.gameObject.SetActive(false);
        CanNotAfford = false;
        itemPriceText = DescriptionPanel.transform.Find("ItemPrice").GetComponent<Text>(); //追加以下文字
        QuantityText = QuantityPanel.transform.Find("QuantityText").GetComponent<Text>();
        ItemNameText = QuantityPanel.transform.Find("ItemName").GetComponent<Text>();
        CanNotAffordText = QuantityPanel.transform.Find("CanNotAfford").GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update(); //前面用BaseItemController的東西
        if (isMouseOverItem)
        {
            DescriptionPanel.transform.position = new Vector3(500, 280, 0);
            itemPriceText.text = $"價格: {thisItem.itemPrice}";
        }
        ToggleHint();
    }

    public void SelectItem()
    {
        currentQuantity = 1;
        QuantityText.text = $"{currentQuantity}";
        ItemNameText.text = $"{thisItem.itemName}: ";

        // 如果當前有已經彈出的數量選擇界面，先隱藏它
        if (activeQuantityUI != null && activeQuantityUI.activeSelf)
        {
            activeQuantityUI.SetActive(false);
        }

        // 將當前的數量選擇界面設為已彈出
        activeQuantityUI = QuantityPanel;

        QuantityPanel.transform.position = new Vector3(1000, 525, 0);
        QuantityPanel.gameObject.SetActive(true);
    }

    public void IncrementQuantity()
    {
        currentQuantity++; // 增加數量
        UpdateQuantityUI(); // 更新數量顯示
    }

    public void DecrementQuantity()
    {
        if (currentQuantity > 1) // 確保數量不小於 1
        {
            currentQuantity--; // 減少數量
            UpdateQuantityUI(); // 更新數量顯示
        }
    }

    private void UpdateQuantityUI()
    {
        // 更新數量的 UI 顯示
        QuantityText.text = QuantityText.text = $"{currentQuantity}";
    }

    public void BuyItem() //按下確認鍵購買
    {
        int totalPrice = thisItem.itemPrice * currentQuantity; // 計算總價格
        if (Player.Instance.money >= totalPrice)
        {
            CanNotAfford = false;
            Player.Instance.money -= totalPrice; //付錢


            //檢查背包中是否已存在相同物品
            Item existingItem = InventoryManager.Instance.Items.Find(item => item.itemName == thisItem.itemName);

            if (existingItem != null)
            {
                // 如果存在，僅增加物品數量
                existingItem.itemCounts[thisItem.itemName] += currentQuantity;
            }
            else
            {
                // 如果不存在，創建新的 Item 
                Item purchasedItem = new Item //然後通通複製過去
                {
                    id = thisItem.id,
                    itemName = thisItem.itemName,
                    itemIcon = thisItem.itemIcon,
                    itemPrice = thisItem.itemPrice,
                    itemValue = thisItem.itemValue,
                    itemDescript = thisItem.itemDescript,
                    itemType = thisItem.itemType,
                    itemCounts = new Dictionary<string, int> { { thisItem.itemName, currentQuantity } }
                };

                // 添加到玩家的背包
                InventoryManager.Instance.AddItem(purchasedItem);
            }

            InventoryManager.Instance.UpdateList();//更新背包
            currentQuantity = 1;
            UpdateQuantityUI();
        }

        else
        {
            CanNotAfford = true;
        }
    }


    public void ToggleHint()
    {
        CanNotAffordText.gameObject.SetActive(CanNotAfford);
    }
}
