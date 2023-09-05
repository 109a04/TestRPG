using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : BaseItemController, IPointerEnterHandler, IPointerExitHandler
{
    public float screenHeightThreshold = 0.7f; // �ù����ת��H��
    public float screenWidthThreshold = 0.8f; // �ù��e�ת��H��

    protected override void Update()
    {
        base.Update(); //�e����BaseItemController���F��
        Vector3 mouseScreenPos = Input.mousePosition;

        // �N�ƹ��ù��y���ഫ���@�ɮy��
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));

        // �N�@�ɮy���ഫ���ù��y�СA�u�� x �b��
        float mouseScreenX = Camera.main.WorldToScreenPoint(mouseWorldPos).x;

        // �N�@�ɮy���ഫ���ù��y�СA�u�� y �b��
        float mouseScreenY = Camera.main.WorldToScreenPoint(mouseWorldPos).y;

        // �P�_�ƹ��O�_���ù����Y�B���W
        bool isAboveThreshold = mouseScreenY > Screen.height * screenHeightThreshold;

        // �P�_�ƹ��O�_���ù����k��
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
            //ChatManager.Instance.SystemMessage($"<color=#F5EC3D>�ϥιD��G{thisItem.itemName}</color>\n");
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
