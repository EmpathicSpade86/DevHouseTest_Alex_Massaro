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

    //public void AddToExistingItem(Item item)
    //{
    //    foreach(InventorySlot slot in slots)
    //    {
    //        if(item == slot.currentItem)
    //        {
    //            Debug.Log("Added to Existing Item");
    //            slot.itemsInSlot += 1/2;
    //            UIUpdate();
    //            return;
    //        }
    //    }
    //}

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


    public void RemoveItem(Item item)
    {
        GetComponentInParent<InventoryContainer>().RemoveItem(item);
    }
}
