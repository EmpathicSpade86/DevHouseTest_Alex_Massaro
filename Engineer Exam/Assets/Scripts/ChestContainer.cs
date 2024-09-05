using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestContainer : InventoryContainer
{
    private SphereCollider interactArea; //The Area in which the player needs to be in order to interact with the chest
    protected override void StartFunctions()
    {
        base.StartFunctions();
        interactArea = GetComponent<SphereCollider>(); //Get the Reference to the Sphere Area
    }

    public override void Input()
    {
        //Debug.Log("Here I am");
        
        ToggleContainer(); //Use the E key to interact with the chests

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //Check the trigger area to make sure the player is in it
        {
            player._chestContainer = this; //Sets the current chest to this
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Remove the current chest so the player cannot interact with it
        if (other.gameObject.tag == "Player")
        {
            player._chestContainer = null; 
            if (inventoryUI.activeInHierarchy) //If the Player moves away from the chest while it is open, this will close it
            {
                Input();
            }

        }
    }

    //Make it so you can't use the drop button in the Chest
    private void DisableDropButton()
    {
        foreach(InventorySlot slot in inventoryUI.GetComponent<InventoryUIController>().slots)
        {
            slot.DisableDropButton();
        }

    }

}
