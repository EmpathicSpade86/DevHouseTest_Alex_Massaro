using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] public Item currentItem;
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button dropButton;
    [SerializeField] private Button transferButton;
    private bool disableDropButton = false; //Used for chests, so they can't drop anything

    private InventoryUIController controller;
    public int itemsInSlot = 0; //The Counter for how many items are currently in the slot

    private void Start()
    {
        controller = transform.parent.parent.GetComponent<InventoryUIController>(); //For the Remove Item funciton

        currentItem = null;
        itemSprite.sprite = null;
        countText.text = itemsInSlot.ToString();
    }


    //public void AddItemToSlot(Item item)
    //{
    //    itemsInSlot++;
    //    countText.text = itemsInSlot.ToString();
    //}

    //Don't increase or decrease the count
    public void KeepSame(Item item)
    {
        currentItem = item;
        itemSprite.sprite = item.itemImage;
        countText.text = itemsInSlot.ToString();
    }

    //Add A completely new Item to the inventory
    public void AddNewItem(Item item)
    {
        currentItem = item;
        itemSprite.sprite = item.itemImage;
        itemsInSlot = 1;
        countText.text = itemsInSlot.ToString();

    }

    //Remove an item from the slot, if there are none left, completely remove it from the list
    public void RemoveItemFromSlot()
    {
        itemsInSlot--;

        if (itemsInSlot <= 0)
        {
            controller.RemoveItem(currentItem);
            currentItem = null;
        }
        else
        {

        }

        if (itemsInSlot <= 0) 
        {
            ToggleButtons();
            itemsInSlot = 0; 
        }
            
        countText.text = itemsInSlot.ToString();

    }

    public void ToggleButtons()
    {
        if (itemsInSlot > 0 && !disableDropButton)
        {
            if (dropButton.gameObject.activeInHierarchy)
            {
                dropButton.gameObject.SetActive(false);
            }
            else
            {
                dropButton.gameObject.SetActive(true);
            }
        }
        else
        {
            dropButton.gameObject.SetActive(false);
        }
        if (itemsInSlot > 0 && canTransfer)
        {
            if (transferButton.gameObject.activeInHierarchy)
            {
                transferButton.gameObject.SetActive(false);
            }
            else
            {
                transferButton.gameObject.SetActive(true);
            }
        }

    }

    //Transfer Button Behavior
    bool canTransfer = false;

    //Called by the transfer button
    public void OnTransfer()
    {
        controller.TransferFromSlot(currentItem);
        RemoveItemFromSlot();
    }

    public void DisableDropButton()
    {
        disableDropButton = true;
    }

    public void ToggleTransferBool(bool intake)
    {
        canTransfer = intake;
    }

    private void Update()
    {
        transferButton.gameObject.SetActive(canTransfer);

        if (itemsInSlot <= 0)
        {
            itemSprite.color = new Color(0, 0, 0, 0);

        }
        else
        {
            itemSprite.color = new Color(255, 255, 255, 255);

        }
    }

}
