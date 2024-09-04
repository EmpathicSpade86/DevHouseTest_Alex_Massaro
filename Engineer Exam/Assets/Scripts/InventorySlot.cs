using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Item currentItem;
    private Sprite itemSprite; 
    private void Start()
    {
        currentItem = null;
        itemSprite = null;
    }

    public void AddItemToSlot(Item item)
    {
        currentItem = item;
        itemSprite = item.itemImage;
    }

    public void RemoveItemFromSlot()
    {
        currentItem = null;
        itemSprite = null;
    }

}
