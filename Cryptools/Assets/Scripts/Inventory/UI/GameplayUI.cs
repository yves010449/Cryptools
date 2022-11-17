using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    Transform hud;
    
    public UIInventoryItem slotPrefab;

    public int selectedIndexUI = 0;

    List<UIInventoryItem> inventoryItems = new List<UIInventoryItem>();

    //void Start()
    //{
    //    PrepareHud();
    //}

    public void InitializeHud()
    {
        //hud = this.gameObject.transform.Find("GameplayInventory").transform;

        for (int i = 0; i < 6; i++)
        {
            inventoryItems.Add(Instantiate(slotPrefab, hud));
            Destroy(inventoryItems[i].GetComponent<EventTrigger>());
        }
    }

    public void UpdateHudSlot(Sprite sprite, int quantity, int index)
    {
        if (index >= 6)
            return;
        inventoryItems[index].SetData(sprite, quantity);
    }

    void Update()
    {
        
    }
    internal void ResetAllItems()
    {
        if (inventoryItems.Count > 0)
        {
            foreach (var item in inventoryItems)
            {
                item.ResetData();
            }
        }
    }

    public void SelectItem(int selectedIndex)
    {
        foreach (UIInventoryItem item in inventoryItems)
        {
            item.Deselect();
        }

        selectedIndexUI = selectedIndex;

        Debug.Log(selectedIndex);

        inventoryItems[selectedIndex].Select();
    }

}
