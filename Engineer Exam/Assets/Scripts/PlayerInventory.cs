using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : InventoryContainer
{
    protected override void StartFunctions()
    {
        ToggleContainer();
        
    }
    public override void Input()
    {
        ToggleContainer();
    }

}
