using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private CraftingManager craftingUI;

        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField]
        private GameplayUI gameplayUI;

        [SerializeField]
        private UIToolsPage toolsUI;

        [SerializeField]
        private AudioSource click;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        public void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            toolsUI.SetTools();
            gameplayUI.SelectItem(0);
            //inventoryUI.gameplayUI.PrepareHud();
            //inventoryUI.UpdateGameplayUI();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            gameplayUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                gameplayUI.UpdateHudSlot(item.Value.item.ItemImage, item.Value.quantity, item.Key);
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            gameplayUI.InitializeHud();
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            craftingUI.OnCraftedItem += HandleCraftItem;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject);
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;

            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            //if (craftingUI.craftedItemDragged)
            //{
            //    inventoryData.AddItem(craftingUI.craftedItem, 1);
            //}
            //else
                inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleCraftItem(ItemSO craftedItem)
        {
            //if (craftingUI.craftedItemDragged)
            //{

            //int totalCraftingAmmount = 0;

            //for (int i = 0; i < craftingUI.craftedItem.CraftingAmount.Count; i++)
            //{
            //    totalCraftingAmmount += craftingUI.craftedItem.CraftingAmount[i];
            //}
            //Debug.Log(totalCraftingAmmount);

            //for (int i = 0; i < totalCraftingAmmount; i++)
            //{
            //    foreach (var item in inventoryData.GetCurrentInventoryState())
            //    {
            //        if (item.Value.item.Name == craftingUI.craftedItem.Name)
            //        {
            //            inventoryData.RemoveItem(item.Key, 1);
            //            break;
            //        }
            //    }
            //}

            for (int i = 0; i < craftingUI.craftedItem.CraftingItems.Count; i++)
            {
                for (int j = 0; j < craftingUI.craftedItem.CraftingAmount[i]; j++)
                {
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        if (item.Value.item.Name == craftingUI.craftedItem.CraftingItems[i].Name)
                        {
                            inventoryData.RemoveItem(item.Key, 1);
                            break;
                        }
                    }
                }
            }

            inventoryData.AddItem(craftingUI.craftedItem, 1);

            craftingUI.ResetCraftableItems();
            craftingUI.InitializeCraftableItems();
            ////}
            ////else
            //inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        [SerializeField]
        Item item;

        public void DropItem(int index)
        {
            if (inventoryData.GetItemAt(index).IsEmpty)
                return;

            float spread = 2f;

            Vector2 pos = this.transform.position;
            pos.x += spread * Random.value - spread / 2;
            pos.y += spread * Random.value - spread / 2;

            item.SetData(inventoryData.GetItemAt(index).item);

            Instantiate(item, pos, Quaternion.identity);

            inventoryData.RemoveItem(index, 1);
        }

        int selectedIndex = 0;
        public void Update()
        {
            //if (Input.anyKeyDown)
            //{
            //    click.Play();
            //}

            if (Input.GetKeyDown(KeyCode.I))
            {
                click.Play();
                //inventoryUI.UpdateGameplayUI();
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        gameplayUI.UpdateHudSlot(item.Value.item.ItemImage, item.Value.quantity, item.Key);
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                    craftingUI.InitializeCraftableItems();
                    craftingUI.UpdateCraftableItems();
                }
                else
                {
                    craftingUI.ResetCraftableItems();
                    inventoryUI.Hide();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                click.Play();
                DropItem(gameplayUI.selectedIndexUI);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                toolsUI.SelectTool(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                toolsUI.SelectTool(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                toolsUI.SelectTool(2);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                gameplayUI.SelectItem(0);
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                gameplayUI.SelectItem(1);
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                gameplayUI.SelectItem(2);
            else if (Input.GetKeyDown(KeyCode.Alpha7))
                gameplayUI.SelectItem(3);
            else if (Input.GetKeyDown(KeyCode.Alpha8))
                gameplayUI.SelectItem(4);
            else if (Input.GetKeyDown(KeyCode.Alpha9))
                gameplayUI.SelectItem(5);
        }
 
        public int GetSelectedIndex()
        {
            return toolsUI.selectedToolIndex;
        }
    }
}