using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftingManager : MonoBehaviour
{
    List<UIInventoryItem> craftableItems = new List<UIInventoryItem>();

    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemSO> craftableGameItems;

    public void Start()
    {
        ResetCraftableItems();
        InitializeCraftableItems();
        UpdateCraftableItems();
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
            Debug.Log("Object Removed!");
            GameObject.Destroy(item.gameObject);
            craftableItems = new List<UIInventoryItem>();
        }
    }

    public void InitializeCraftableItems()
    {
        UpdateCraftableItems();
        foreach (ItemSO item in craftableGameItems)
        {
            Debug.Log(item.IsCraftable);
            if (item.IsCraftable)
            {
                Debug.Log("Object Made!");
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.SetData(item.ItemImage, 0);
                uiItem.craftableUI = true;
                uiItem.OnItemClicked += HandleItemSelection;
                craftableItems.Add(uiItem);
            }
        }
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        Debug.Log("Craftable Item Clicked!");
    }
}
