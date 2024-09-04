using StarterAssets;
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
        Item itemRef = Instantiate(item); //Instantiates a new item
        itemRef.gameObject.SetActive(false); //Sets its active state to false so the player cannot see it


        //Check each of the items to see if the item is alread in the inventory
        foreach (Item i in items)
        {
            //If the item being added is already in the inventory, increase its count in the slots by one
            if (item.itemID == i.itemID)
            {
                InventorySlot slot = inventoryUI.GetComponent<InventoryUIController>().GetItemSlot(i);
                slot.itemsInSlot += 1;
                inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
                //Debug.Log("Added to current");
                return;
            }
        }
        //otherwise, check to see if there are free slots in the inventory, if there are add the item to a new slot
        if (curItemCount < inventorySlots)
        {
            Debug.Log("Added Item");
            items.Add(itemRef);
            inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
            curItemCount++;
        }
        
    }

    //If the Item in the slot is completely gone (i.e. if there are 0 of a certain item in a slot) completely remove the item from the inventory
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
        curItemCount--;
    }

    public List<Item> GetItems() { return items; }
    public int GetInventorySlots() { return inventorySlots; }

}
