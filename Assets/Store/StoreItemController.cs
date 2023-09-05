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
    private static GameObject activeQuantityUI; // �O�s��e�w�g�u�X���ƶq��ܬɭ�
    private int currentQuantity = 1; //��l�ƶq

    protected override void Start()
    {
        base.Start();

        QuantityPanel.gameObject.SetActive(false);
        CanNotAfford = false;
        itemPriceText = DescriptionPanel.transform.Find("ItemPrice").GetComponent<Text>(); //�l�[�H�U��r
        QuantityText = QuantityPanel.transform.Find("QuantityText").GetComponent<Text>();
        ItemNameText = QuantityPanel.transform.Find("ItemName").GetComponent<Text>();
        CanNotAffordText = QuantityPanel.transform.Find("CanNotAfford").GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update(); //�e����BaseItemController���F��
        if (isMouseOverItem)
        {
            DescriptionPanel.transform.position = new Vector3(500, 280, 0);
            itemPriceText.text = $"����: {thisItem.itemPrice}";
        }
        ToggleHint();
    }

    public void SelectItem()
    {
        currentQuantity = 1;
        QuantityText.text = $"{currentQuantity}";
        ItemNameText.text = $"{thisItem.itemName}: ";

        // �p�G��e���w�g�u�X���ƶq��ܬɭ��A�����å�
        if (activeQuantityUI != null && activeQuantityUI.activeSelf)
        {
            activeQuantityUI.SetActive(false);
        }

        // �N��e���ƶq��ܬɭ��]���w�u�X
        activeQuantityUI = QuantityPanel;

        QuantityPanel.transform.position = new Vector3(1000, 525, 0);
        QuantityPanel.gameObject.SetActive(true);
    }

    public void IncrementQuantity()
    {
        currentQuantity++; // �W�[�ƶq
        UpdateQuantityUI(); // ��s�ƶq���
    }

    public void DecrementQuantity()
    {
        if (currentQuantity > 1) // �T�O�ƶq���p�� 1
        {
            currentQuantity--; // ��ּƶq
            UpdateQuantityUI(); // ��s�ƶq���
        }
    }

    private void UpdateQuantityUI()
    {
        // ��s�ƶq�� UI ���
        QuantityText.text = QuantityText.text = $"{currentQuantity}";
    }

    public void BuyItem() //���U�T�{���ʶR
    {
        int totalPrice = thisItem.itemPrice * currentQuantity; // �p���`����
        if (Player.Instance.money >= totalPrice)
        {
            CanNotAfford = false;
            Player.Instance.money -= totalPrice; //�I��


            //�ˬd�I�]���O�_�w�s�b�ۦP���~
            Item existingItem = InventoryManager.Instance.Items.Find(item => item.itemName == thisItem.itemName);

            if (existingItem != null)
            {
                // �p�G�s�b�A�ȼW�[���~�ƶq
                existingItem.itemCounts[thisItem.itemName] += currentQuantity;
            }
            else
            {
                // �p�G���s�b�A�Ыطs�� Item 
                Item purchasedItem = new Item //�M��q�q�ƻs�L�h
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

                // �K�[�쪱�a���I�]
                InventoryManager.Instance.AddItem(purchasedItem);
            }

            InventoryManager.Instance.UpdateList();//��s�I�]
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
