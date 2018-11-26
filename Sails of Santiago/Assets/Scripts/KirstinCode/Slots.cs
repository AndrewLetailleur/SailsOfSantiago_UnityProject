using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

private bool hovered;
public bool empty;

public GameObject item;
public Texture itemIcon;


	// Use this for initialization
	void Start () {
		hovered = false;
        	}

    // Update is called once per frame
    void Update()
    {
        if (item)
        {
            empty = false;

            itemIcon = item.GetComponent<Item>().icon;
            this.GetComponent<RawImage>().texture = itemIcon;
        }

       
    }
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		hovered = true;
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
		hovered = false;
	}
}
