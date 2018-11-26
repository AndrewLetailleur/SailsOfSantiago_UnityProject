using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject inventory;
    public GameObject slotHolder;
    private bool inventoryEnabled;

    private int slots;
    private Transform[] slot;

    private GameObject itemPickedUp;



    public void Start()
    {
        slots = slotHolder.transform.childCount;
        slot = new Transform[slots];
        DetectInventorySlots();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;

        }
        if (inventoryEnabled)
            inventory.SetActive(true);
        else
            inventory.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        print("Collison! Oh No!");
        if(other.tag == "Item")
        {
            AddItem(other.gameObject);
        }
    }

    public void AddItem(GameObject item)
    {
        for (int i = 0; i < slots; i++)
        {
            if(slot[i].GetComponent<Slots>().empty)
            {
                slot[i].GetComponent<Slots>().item = itemPickedUp;
                slot[i].GetComponent<Slots>().itemIcon = itemPickedUp.GetComponent<Item>().icon;
            }
        }
    }

    public void DetectInventorySlots()
	{
		for(int i = 0; i < slots; i++)
		{
			slot[i] = slotHolder.transform.GetChild(i);
			print(slot[i].name);
		}
}
}
