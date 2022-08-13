using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayInventory : MonoBehaviour
{
    Inventory inventory;
    Transform itemSlotContainer;
    Transform itemTemplate;

    public void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemTemplate = itemSlotContainer.Find("ItemTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        int x = 0;
        float itemSlotCellSize = 60f;
        foreach(Item item in inventory.GetItemSlots())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
            x++;
        }
    }

}
