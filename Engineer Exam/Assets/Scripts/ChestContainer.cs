using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChestContainer : MonoBehaviour
{
    [Header("Container Variables")]
    [SerializeField] protected int inventorySlots = 15;
    [SerializeField] private GameObject inventoryUI;
    private bool isOn = false;
    private ThirdPersonController player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        if (!isOn)
        {
            inventoryUI.SetActive(false);
        }
    }

    public virtual void Input()
    {
        Debug.Log("Toggle");
        ToggleContainer();
    }

    protected void ToggleContainer()
    {
        if (inventoryUI.activeInHierarchy)
        {
            Debug.Log("OFF");
            inventoryUI.SetActive(false);
        }
        else
        {
            Debug.Log("ON");
            inventoryUI.SetActive(true);
        }
    }

}
