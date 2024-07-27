using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchelaOpen : MonoBehaviour
{

    public float theDistance;
    public GameObject actionKey;
    public GameObject actionText;
    public GameObject Torchela;
    
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.TryGetComponent(out IInteractable interactableObject))
                {
                    interactableObject.Interact();
                }
            }
        }
    }

    private void OnMouseExit()
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
    }
}
