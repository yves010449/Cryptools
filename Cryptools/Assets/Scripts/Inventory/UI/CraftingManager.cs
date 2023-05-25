using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    List<UIInventoryItem> craftableItems = new List<UIInventoryItem>();
    List<int> indices = new List<int>();

    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemSO> craftableGameItems;

    public ItemSO craftedItem;

    public bool isReadyToCraft = false;

    public event Action<ItemSO> OnCraftedItem;

    public void Start()
    {
        ResetCraftableItems();
        InitializeCraftableItems();
        UpdateCraftableItems();
        itemPrefab.OnItemClicked += HandleCraft;
        itemPrefab.OnItemEndDrag += HandleEndDrag;
        //itemPrefab.OnItemDroppedOn += HandleCraft;
    }

    public void UpdateCraftableItems()
    {
        Dictionary<int, InventoryItem> inventoryItems = inventoryData.GetCurrentInventoryState();

        foreach (ItemSO item in craftableGameItems)
        {
            for (int i = 0; i < item.CraftingItems.Count; i++)
            {
                foreach (KeyValuePair<int, InventoryItem> element in inventoryItems)
                {
                    if (item.CraftingItems[i].Name == element.Value.item.Name && item.CraftingAmount[i] <= element.Value.quantity)
                    {
                        item.CraftingAvailability[i] = true;
                        if (item.CraftingAvailability.Contains(false))
                            break;
                        item.IsCraftable = true;
                        //if (craftedItem != null)
                        //{
                        //    craftedItem.IsCraftable = true;
                        //}
                        //if (craftedItem != null)
                        //{
                        //    if (craftedItem.IsCraftable == false)
                        //    {
                        //        itemPrefab.ResetData();
                        //        craftedItem = null;
                        //    }
                        //}
                    }
                }
            }
        }
    }

    public void ResetCraftableItems()
    {
        foreach (ItemSO item in craftableGameItems)
        {
            for (int i = 0; i < item.CraftingItems.Count; i++)
            {
                item.CraftingAvailability[i] = false;
            }
            item.IsCraftable = false;
        }
        foreach (Transform item in contentPanel)
        {
            //Debug.Log("Object Removed!");
            GameObject.Destroy(item.gameObject);
            craftableItems = new List<UIInventoryItem>();
            indices = new List<int>();
        }
        //if (craftedItem != null)
        //{
        //    if (craftedItem.IsCraftable == false)
        //    {
        //        itemPrefab.ResetData();
        //        craftedItem = null;
        //    }
        //}
        //if (craftableItems.Count == 0)
        //{
        //    itemPrefab.ResetData();
        //    craftedItem = null;
        //}
    }

    public void InitializeCraftableItems()
    {
        UpdateCraftableItems();
        for (int i = 0; i < craftableGameItems.Count; i++)
        {
            if (craftableGameItems[i].IsCraftable)
            {
                //Debug.Log("Object Made!");
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.SetData(craftableGameItems[i].ItemImage, 0);
                uiItem.craftableUI = true;
                uiItem.OnItemClicked += HandleItemSelection;
                craftableItems.Add(uiItem);
                indices.Add(i);
            }
        }
        if (craftedItem != null)
        {
            if (craftedItem.IsCraftable == false)
            {
                itemPrefab.ResetData();
                craftedItem = null;
            }
        }
        //if (craftableItems.Count == 0)
        //{
        //    itemPrefab.ResetData();
        //    craftedItem = null;
        //}
        //foreach (ItemSO item in craftableGameItems)
        //{
        //    //Debug.Log(item.IsCraftable);

        //}
    }

    public void CraftItem()
    {

    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = craftableItems.IndexOf(inventoryItemUI);
        itemPrefab.SetData(craftableGameItems[indices[index]].ItemImage, 1);
        craftedItem = craftableGameItems[indices[index]];
        //craftedItem.IsCraftable = true;
        isReadyToCraft = true;
    }

    private void HandleCraft(UIInventoryItem inventoryItemUI)
    {
        //if (craftedItem == null)
        //{
        //    Debug.Log("No crafted Item");
        //    return;
        //}

        Debug.Log("Crafted Item Clicked!");
        OnCraftedItem?.Invoke(craftedItem);
        //gameObject.GetComponent<UIInventoryPage>().CreateDraggedItem(craftedItem.ItemImage, 1);

        //craftedItemDragged = true;
        //Debug.Log("C on");

    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        //craftedItemDragged = false;
        //Debug.Log("C on");
        //int index = gameObject.GetComponent<UIInventoryPage>().listOfUIItems.IndexOf(inventoryItemUI);
        ////Debug.Log(index+"pp");
        //if (index == -1)
        //{
        //    return;
        //}
        //OnAddCraftedItem?.Invoke(index);
    }

}
