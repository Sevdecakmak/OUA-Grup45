using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Camera cam;
    private bool isSelected;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update() 
    {
      
        if(isSelected)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }

        
    }

    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0))
        {
            isSelected = true;
        }
    }
}
