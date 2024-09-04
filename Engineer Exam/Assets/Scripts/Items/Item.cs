using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    //Base Item Class
    public string itemID = ""; //ID to check to see if the item is already in the player's inventory or chest inventory
    public Sprite itemImage; //Sprite image for the item
    private void OnTriggerEnter(Collider other)
    {
        //Handle collision with the player
        if (other.gameObject.tag == "Player")
        {
            PlayerInventory inventory = other.gameObject.GetComponentInChildren<PlayerInventory>();
            inventory.AddItem(this);
            Debug.Log("Collided With Player");
            Destroy(gameObject);
        }
    }
}
