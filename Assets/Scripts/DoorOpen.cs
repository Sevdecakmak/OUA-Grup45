using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;
    public GameObject actionText;
    public GameObject door;
    private Animator doorAnimator;
    private bool gate = false; 

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }

    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (theDistance <= 2)
        {
            
            actionKey.SetActive(true);
            actionText.SetActive(true);
        }
        else
        {
            actionKey.SetActive(false);
            actionText.SetActive(false);
        }

        
        if (Input.GetKeyDown(KeyCode.E) && theDistance <= 2)
        {
            gate = !gate; 
            doorAnimator.SetBool("isOpen", gate); 
            this.gameObject.GetComponent<BoxCollider>().enabled = !gate;
            actionKey.SetActive(false);
            actionText.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
    }
}
