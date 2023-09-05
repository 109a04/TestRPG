using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : BaseItemController, IPointerEnterHandler, IPointerExitHandler
{
    public float screenHeightThreshold = 0.7f; // 螢幕高度的閾值
    public float screenWidthThreshold = 0.8f; // 螢幕寬度的閾值

    protected override void Update()
    {
        base.Update(); //前面用BaseItemController的東西
        Vector3 mouseScreenPos = Input.mousePosition;

        // 將滑鼠螢幕座標轉換成世界座標
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));

        // 將世界座標轉換成螢幕座標，只取 x 軸值
        float mouseScreenX = Camera.main.WorldToScreenPoint(mouseWorldPos).x;

        // 將世界座標轉換成螢幕座標，只取 y 軸值
        float mouseScreenY = Camera.main.WorldToScreenPoint(mouseWorldPos).y;

        // 判斷滑鼠是否位於螢幕的某處之上
        bool isAboveThreshold = mouseScreenY > Screen.height * screenHeightThreshold;

        // 判斷滑鼠是否位於螢幕的右側
        bool isOnRightSide = mouseScreenX > Screen.width * screenWidthThreshold;

        if (isMouseOverItem)
        {
            if (isAboveThreshold)
            {
                if (!isOnRightSide)
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(250, -200, 0);
                }
                else
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(-250, -200, 0);
                }
            }
            else
            {
                if (!isOnRightSide)
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(250, 100, 0);
                }
                else
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(-250, 100, 0);
                }

            }
        }

    }

    public void UseItem()
    {
        if (thisItem.itemType != Item.ItemType.Weapon)
        {
            //ChatManager.Instance.SystemMessage($"<color=#F5EC3D>使用道具：{thisItem.itemName}</color>\n");
        }

        switch (thisItem.itemType)
        {
            case Item.ItemType.Fruit:
                Player.Instance.IncreaseHealth(thisItem.itemValue);
                break;
            case Item.ItemType.Vegetable:
                Player.Instance.IncreaseExp(thisItem.itemValue);
                break;
            case Item.ItemType.Starch:
                Player.Instance.IncreaseMp(thisItem.itemValue);
                break;

        }

        RemoveItem();
    }

    public void RemoveItem()
    {
        if (thisItem.itemType != Item.ItemType.Weapon)
        {
            InventoryManager.Instance.RemoveItem(thisItem);
        }


        if (thisItem.itemCounts[thisItem.itemName] == 0)
        {
            Destroy(gameObject);
        }

    }


}
