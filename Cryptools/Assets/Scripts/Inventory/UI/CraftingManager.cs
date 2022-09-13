using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftingManager : MonoBehaviour
{
    private List<UIInventoryItem> craftableItems = new List<UIInventoryItem>();
    
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemSO> gameItems;

    public List<string> craftingItemsRecipe;
    public List<int> craftingAmountRecipe;

    public List<string> totalCraftingItemsRecipe;
    public List<int> totalCraftingAmountRecipe;

    public void IdentifyCarriedItems()
    {
        totalCraftingItemsRecipe = new List<string>();
        totalCraftingAmountRecipe = new List<int>();

        Dictionary<int, InventoryItem> inventoryItems = inventoryData.GetCurrentInventoryState();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            if (totalCraftingItemsRecipe[i] == inventoryItems[i].item.Name)
            {
                totalCraftingAmountRecipe[i] += inventoryItems[i].quantity;
                continue;
            }
            totalCraftingItemsRecipe.Add(inventoryItems[i].item.Name);
            totalCraftingAmountRecipe.Add(inventoryItems[i].quantity);
        }

    }
    
    private void UpdateCraftableItems()
    {
        foreach (ItemSO item in gameItems)
        {
            List<ItemSO> recipe = item.CraftingItems;
            List<int> recipeAmount = item.CraftingAmount;

            UIInventoryItem craftableItem = new UIInventoryItem();
            for (int i = 0; i < recipe.Count; i++)
            {
                for (int j = 0; j < totalCraftingItemsRecipe.Count; j++)
                {
                    if (recipe[i].Name == totalCraftingItemsRecipe[j] && recipeAmount[i] <= totalCraftingAmountRecipe[j])
                    {
                        craftableItem.SetData(item.ItemImage, 0);
                        craftableItems.Add(craftableItem);
                    }
                }
                
            }
        }
    }

    private void InitializeCraftableItems()
    {
        for (int i = 0; i < craftableItems.Count; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
        }
    }
}
