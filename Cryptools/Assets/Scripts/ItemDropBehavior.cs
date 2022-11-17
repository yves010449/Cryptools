using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropBehavior : MonoBehaviour
{
    [SerializeField]
    Item itemDrop;

    [SerializeField]
    ItemSO itemType;

    float spread = 2f;
    float hitpoints;

    private void Start()
    {
        SetDrop();
    }

    public void DropItem()
    {
        for (int i = 0; i < Random.Range(8, 15); i++)
        {
            Vector2 pos = this.transform.position;
            pos.x += spread * Random.value - spread / 2;
            pos.y += spread * Random.value - spread / 2;
            Instantiate(itemDrop, pos, Quaternion.identity).transform.SetParent(this.transform.parent);
        }
        
    }

    public void SetDrop()
    {
        itemDrop.SetData(itemType);
    }

    public ItemSO GetDrop()
    {
        return itemType;
    }

    public void BreakItem()
    {
        Destroy(gameObject);
    }
}
