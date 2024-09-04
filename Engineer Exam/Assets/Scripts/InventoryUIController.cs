using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    private InventoryContainer inventory;
    [SerializeField] private GameObject slotsParent;
    [SerializeField] private GameObject inventorySlot;

    [SerializeField] private List<InventorySlot> slots;

    private void Start()
    {
        inventory = GetComponentInParent<InventoryContainer>();
        SetInventorySlots();
        UIUpdate();
        
    }

    //Sets the amount of slots at the start to the amount listed in the Inventory container (Player or Chest) scripts
    private void SetInventorySlots()
    {
        for (int i = 0; i < inventory.GetInventorySlots(); i++)
        {
            Instantiate(inventorySlot, slotsParent.transform);
            
        }
        
        for (int i = 0; i < slotsParent.transform.childCount; i++)
        {
            //Debug.Log(slotsParent.transform.GetChild(i).name);
            slots.Add(slotsParent.transform.GetChild(i).GetComponent<InventorySlot>());
        }

        //Debug.Log(slots.Count);
    }

    //Update the UI for the Inventory
    public void UIUpdate()
    {
        int i = 0;
        foreach (InventorySlot slot in slots) 
        {
            foreach (Item item in inventory.GetItems())
            {
                if (item != null && item == slot.currentItem)
                {
                    slots[i].GetComponent<InventorySlot>().KeepSame(item);
                    return;
                }
                else if (item != null)
                {
                    slots[i].GetComponent<InventorySlot>().AddNewItem(item);
                    return;
                }
                i++;
            }
        }
        

    }

    //Return the slot with an item to add to its current count
    public InventorySlot GetItemSlot(Item item)
    {
        foreach(InventorySlot slot in slots)
        {
            if (item == slot.currentItem)
            {
                return slot;
            }
        }
        return null;
    }


    public void RemoveItem(Item item) //Used in the Inventory slot script to call the Inventory Container Script to completely remove an item
    {
        GetComponentInParent<InventoryContainer>().RemoveItem(item);
    }
}
