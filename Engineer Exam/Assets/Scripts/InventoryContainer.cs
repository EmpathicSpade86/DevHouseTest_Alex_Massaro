using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryContainer : MonoBehaviour
{
    [Header("Container Variables")]
    [SerializeField] protected int inventorySlots = 12; //Max slots for this particular inventory, Overall Max is 12
    [SerializeField] protected GameObject inventoryUI; //Refrence to the UI object
    private bool isOn = false;
    protected ThirdPersonController player;

    [SerializeField] protected List<Item> items = new List<Item>(); //The List of items that will be in the inventory

    [SerializeField] private int curItemCount = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();

        if (inventoryUI == null )
        {
            Debug.LogError("Missing Inventory Object on: " + name);
        }

        if (!isOn)
        {
            inventoryUI.SetActive(false);
        }

        items.Capacity = inventorySlots;
        StartFunctions();
    }

    protected virtual void StartFunctions()
    {

    }

    public virtual void Input()
    {
        //Debug.Log("Toggle");
        
    }

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

    

    public void AddItem(Item item)
    {
        Item itemRef = Instantiate(item);
        itemRef.gameObject.SetActive(false);
        //Check each of the items to see if the item is alread in the inventory
        foreach (Item i in items)
        {
            if (item == i)
            {
                inventoryUI.GetComponent<InventoryUIController>().AddToExistingItem(itemRef);
                return;
            }
        }
        
        if (curItemCount < inventorySlots)
        {
            Debug.Log("Added Item");
            items.Add(itemRef);
            inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
            curItemCount++;
        }
        
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        inventoryUI.GetComponent<InventoryUIController>().UIUpdate();
        curItemCount--;
    }

    public List<Item> GetItems() { return items; }
    public int GetInventorySlots() { return inventorySlots; }

}
