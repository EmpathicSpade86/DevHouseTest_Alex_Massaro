using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public string itemID = "";
    public Sprite itemImage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInventory inventory = other.gameObject.GetComponentInChildren<PlayerInventory>();
            inventory.AddItem(this);
            Debug.Log("Collided With Player");
            Destroy(gameObject);
        }
    }
}
