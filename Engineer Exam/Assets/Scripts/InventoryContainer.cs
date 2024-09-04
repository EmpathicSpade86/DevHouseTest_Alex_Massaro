using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryContainer : MonoBehaviour
{
    [Header("Container Variables")]
    [SerializeField] protected int inventorySlots = 12; //Max slots for this particular inventory, Overall Max is 12
    [SerializeField] private GameObject inventoryUI; //Refrence to the UI object
    private bool isOn = false;
    private ThirdPersonController player;

    protected List<Item> items = new List<Item>(); //The List of items that will be in the inventory

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

    }

    public virtual void Input()
    {
        Debug.Log("Toggle");
        ToggleContainer();
    }

    protected void ToggleContainer()
    {
        if (inventoryUI.activeInHierarchy)
        {
            Debug.Log("OFF");
            inventoryUI.SetActive(false);
        }
        else
        {
            Debug.Log("ON");
            inventoryUI.SetActive(true);
        }
    }

    public int GetInventorySlots() { return inventorySlots; }

    public void AddItem(Item item)
    {
        items.Add(item);
        inventoryUI.GetComponent<InventoryUIController>().UIUpdate(); 
    }

    public void RemoveItem()
    {

    }

    public List<Item> GetItems() { return items; }
    

}
