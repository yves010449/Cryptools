using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //private Item[] itemSlots;
    private List<Item> itemSlots;

    public Inventory()
    {
        //itemSlots = new Item[6];
        itemSlots = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1 });
        Debug.Log(itemSlots.Count);
    }

    public void AddItem(Item item)
    {
        itemSlots.Add(item);
        /*
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i] == null)
            {
                itemSlots[i] = item;
                break;
            }
        }
        */
    }

    public List<Item> GetItemSlots()
    {
        return itemSlots;
    }

}
