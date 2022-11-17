using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField] MouseFollower mouseFollower;

        public List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnItemActionRequested, OnStartDragging;

        //public event Action<ItemSO> OnItemCraft;

        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        //public void UpdateGameplayUI()
        //{
        //    for (int i = 0; i < 6; i++)
        //    {
        //        gameplayUI.UpdateHudSlot(listOfUIItems[i], i);
        //    }
        //}

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            //Debug.Log(index);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);

            //if (!gameObject.GetComponent<CraftingManager>().craftedItemDragged)
            //    currentlyDraggedItemIndex = index;

            //Debug.Log(index);
            //Debug.Log(currentlyDraggedItemIndex);

            //Debug.Log(index);
            //Debug.Log("C off");
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        }

        //private void HandleCraftItem(UIInventoryItem inventoryItemUI)
        //{
        //    //int index = listOfUIItems.IndexOf(inventoryItemUI);

        //    //if (!gameObject.GetComponent<CraftingManager>().craftedItemDragged)
        //    //    currentlyDraggedItemIndex = index;

        //    //Debug.Log(index);
        //    //Debug.Log(currentlyDraggedItemIndex);

        //    //Debug.Log(index);
        //    //Debug.Log("C off");
        //    //if (index == -1)
        //    //{
        //    //    return;
        //    //}
        //    OnItemActionRequested?.Invoke(currentlyDraggedItemIndex, index);
        //}

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            //Debug.Log("Returned!");
            //Debug.Log(index);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {

        }

        public void Show()
        {
            gameObject.transform.parent.gameObject.SetActive(true);
            gameObject.transform.SetSiblingIndex(gameObject.transform.parent.childCount - 2);
        }

        public void Hide()
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfUIItems)
            {
                item.ResetData();
            }
        }
    }
}