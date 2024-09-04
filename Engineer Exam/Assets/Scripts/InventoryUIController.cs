using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    private InventoryContainer inventory;
    [SerializeField] private GameObject slotsParent;
    [SerializeField] private InventorySlot inventorySlot;

    GameObject[] slots;

    private void Start()
    {
        inventory = GetComponentInParent<InventoryContainer>();
        SetInventorySlots();
    }

    private void SetInventorySlots()
    {
        for (int i = 0; i < inventory.GetInventorySlots(); i++)
        {
            InventorySlot slot = Instantiate(inventorySlot, slotsParent.transform);

        }
    }

    public void UIUpdate()
    {
        foreach (Item item in inventory.GetItems())
        {
            if (item != null)
            {
                Debug.Log(item.ToString());
            }
        }

    }
}
