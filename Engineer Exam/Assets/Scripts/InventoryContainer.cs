using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryContainer : MonoBehaviour
{
    [Header("Container Variables")]
    [SerializeField] protected int inventorySlots = 12; //Max slots for this particular inventory, Overall Max is 12
    [SerializeField] protected GameObject inventoryUI; //Refrence to the UI object
    private bool isOn = false; //If the UI is active in the scene
    protected ThirdPersonController player; //Ref to the player

    [SerializeField] protected List<Item> items = new List<Item>(); //The List of items that will be in the inventory

    [SerializeField] private int curItemCount = 0; //Current amount of items the player has

    [SerializeField] protected InventoryContainer otherContainer;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>(); //Find the Player

        if (inventoryUI == null )
        {
            Debug.LogError("Missing Inventory Object on: " + name);
        }

        if (!isOn)
        {
            inventoryUI.SetActive(false);
        }

        items.Capacity = inventorySlots; //Sets the capacity of the items list to the amount of slots 
        StartFunctions(); //Run any other stuff at the end of the start functions (Only for inherited classes) 
    }

    protected virtual void StartFunctions()
    {

    }

    //Handle Input (Inherited Classes) 
    public virtual void Input()
    {
        
    }

    //Turns on/off the UI and locks/unlocks the mouse cursor
    protected void ToggleContainer()
    {
        if (inventoryUI.activeInHierarchy)
        {
            //Debug.Log("OFF");
            inventoryUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            //Debug.Log("ON");
            //inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
            inventoryUI.SetActive(true);
            inventoryUI.GetComponent<InventoryUIController>().UIUpdate();

            Cursor.lockState = CursorLockMode.None;
        }
    }

    
    //Adds Item to the list
    public void AddItem(Item item)
    {
        Debug.Log("Beggining to Add Item");

        if (item == null ) { return; }
        
        Item itemRef = Instantiate(item); //Instantiates a new item
        itemRef.gameObject.SetActive(false); //Sets its active state to false so the player cannot see it

        if(items.Count == 0)
        {
            Debug.Log("Added Item");
            items.Add(itemRef);
            inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
            curItemCount++;
            return;
        } 

        foreach (Item i in items)
        {
            InventorySlot slot = inventoryUI?.GetComponent<InventoryUIController>()?.GetItemSlot(i);
            //Check for null references before accessing the slot
            if (slot == null)
            {
                Debug.LogError("Slot is null or inventory UI is missing!");
                continue;
            }

            //If item is already in inventory and there's room in the slot
            if (item.itemID == i.itemID && slot.itemsInSlot < slot.maxInSlot)
            {
                slot.itemsInSlot += 1;
                inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
                return;
            }
            //If item is in inventory but slot is full, move to the next slot
            else if (item.itemID == i.itemID && slot.itemsInSlot == slot.maxInSlot)
            {
                Debug.Log("Max Items Reached");
                bool slotFound = false;

                //Loop through available slots to find an empty one
                foreach (InventorySlot availableSlot in inventoryUI.GetComponent<InventoryUIController>().slots)
                {
                    if (availableSlot.itemsInSlot < availableSlot.maxInSlot)
                    {
                        Debug.Log("Move to new slot");
                        availableSlot.currentItem = item; // Assuming you're adding the same item
                        availableSlot.itemsInSlot += 1;
                        inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
                        curItemCount++;
                        slotFound = true;
                        break;
                    }
                }

                if (!slotFound)
                {
                    Debug.Log("No available slots");
                }
                return;
            }
        }

        //If the item is new, find an empty slot to place the item
        if (curItemCount < inventoryUI.GetComponent<InventoryUIController>().slots.Count)
        {
            foreach (InventorySlot emptySlot in inventoryUI.GetComponent<InventoryUIController>().slots)
            {
                if (emptySlot.itemsInSlot == 0)
                {
                    Debug.Log("Added new item to empty slot");
                    emptySlot.currentItem = item;
                    emptySlot.itemsInSlot += 1;
                    items.Add(item);
                    inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
                    curItemCount++;
                    return;
                }
            }
            Debug.Log("Inventory full, cannot add item");
        }

    }

    //If the Item in the slot is completely gone (i.e. if there are 0 of a certain item in a slot) completely remove the item from the inventory
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
        curItemCount--;
    }

    private void Update()
    {
        if ( otherContainer == null)
        {
            foreach (InventorySlot slot in inventoryUI.GetComponent<InventoryUIController>().slots)
            {
                slot.ToggleTransferBool(false);
            }
        }
    }



    public List<Item> GetItems() { return items; }
    public int GetInventorySlots() { return inventorySlots; }


    public void GetOtherContainer(InventoryContainer chestContainer)
    {
        otherContainer = chestContainer;
        foreach (InventorySlot slot in inventoryUI.GetComponent<InventoryUIController>().slots)
        {
            slot.ToggleTransferBool(true);
        }
        foreach (InventorySlot slot in otherContainer.inventoryUI.GetComponent<InventoryUIController>().slots)
        {
            slot.ToggleTransferBool(true);
        }
    }

    public void DisableTransfer()
    {
        foreach (InventorySlot slot in inventoryUI.GetComponent<InventoryUIController>().slots)
        {
            slot.ToggleTransferBool(false);
        }
    }

    public void TransferItem(Item item)
    {
        if (otherContainer == this)
        {
            Debug.LogError("Houston we have a problem");
            return;
        }

        foreach (InventorySlot slot in inventoryUI.GetComponent<InventoryUIController>().slots)
        {
            if (item == slot.currentItem)
            {
                Debug.Log("Transfer: " + otherContainer.name);
                otherContainer.AddItem(item);
                Debug.Log(otherContainer.GetItems().Count);
            }
        }

    }

}
