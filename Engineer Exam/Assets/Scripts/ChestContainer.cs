using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestContainer : InventoryContainer
{
    private SphereCollider interactArea;
    protected override void StartFunctions()
    {
        base.StartFunctions();
        interactArea = GetComponent<SphereCollider>();
    }

    public override void Input()
    {
        Debug.Log("Here I am");
        
        ToggleContainer();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player._chestContainer = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player._chestContainer = null;
            if (inventoryUI.activeInHierarchy)
            {
                Input();
            }

        }
    }

}
