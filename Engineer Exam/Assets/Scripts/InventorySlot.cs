using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] public Item currentItem;
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button dropButton;
    [SerializeField] private Button transferButton;

    private InventoryUIController controller;
    public int itemsInSlot = 0;

    private void Start()
    {
        controller = transform.parent.parent.GetComponent<InventoryUIController>();

        currentItem = null;
        itemSprite.sprite = null;
        countText.text = itemsInSlot.ToString();
    }

    public void AddItemToSlot(Item item)
    {
        itemsInSlot++;
        countText.text = itemsInSlot.ToString();
    }

    public void KeepSame(Item item)
    {
        currentItem = item;
        itemSprite.sprite = item.itemImage;
        countText.text = itemsInSlot.ToString();
    }

    public void AddNewItem(Item item)
    {
        currentItem = item;
        itemSprite.sprite = item.itemImage;
        itemsInSlot = 1;
        countText.text = itemsInSlot.ToString();

    }

    public void RemoveItemFromSlot()
    {
        itemsInSlot--;

        if (itemsInSlot <= 0)
        {
            controller.RemoveItem(currentItem);
            currentItem = null;
            itemSprite.color = new Color(0,0,0,0);

        }
            
        countText.text = itemsInSlot.ToString();

    }

    public void ToggleDropButton()
    {
        if (itemsInSlot > 0)
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
    }

    bool canTransfer = false;
    public void ToggleTransferButton()
    {
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

}
