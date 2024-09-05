using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : InventoryContainer
{

    protected override void StartFunctions()
    {
        ToggleContainer(); //Toggle the UI for the Player Inventory, this will make it appear at the beginning 
        
    }
    public override void Input()
    {
        ToggleContainer(); //Toggle with the button (i key)
    }
    


}
