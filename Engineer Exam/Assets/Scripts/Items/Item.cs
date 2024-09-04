using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public Sprite itemImage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInventory inventory = collision.gameObject.GetComponentInChildren<PlayerInventory>(); 
            inventory.AddItem(this);
        }
    }
}
