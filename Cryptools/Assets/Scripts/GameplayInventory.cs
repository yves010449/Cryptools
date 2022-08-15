using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayInventory : MonoBehaviour
{
    Inventory inventory;
    Transform itemSlotContainer;
    Transform itemInSlot;

    public void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemInSlot = itemSlotContainer.Find("Item");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        float x = 0;
        float itemSlotCellSize = 60f;
        foreach(Item item in inventory.GetItemSlots())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemInSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
            Image image = itemSlotRectTransform.GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
        }
    }

}
