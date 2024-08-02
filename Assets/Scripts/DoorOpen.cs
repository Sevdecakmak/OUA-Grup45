using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;
    public GameObject actionText;
    public GameObject archGate;

    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (theDistance <= 2)
        {
            //mouse un üstündeyken
            actionKey.SetActive(true);
            actionText.SetActive(true);
            
        }
        else
        {
            actionKey.SetActive(false);
            actionText.SetActive(false);
        }

        if (Input.GetButton("Action"))
        {
            if (theDistance <= 2)
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false; //var olmaz
                actionKey.SetActive(true);
                actionText.SetActive(true);
                archGate.GetComponent<Animation>().Play("ArchGateAnimation");
            }
        }
    }

    private void OnMouseExit()
    {   //üstünde değilken
        actionKey.SetActive(false);
        actionText.SetActive(false);
        
    }
}
