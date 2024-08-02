using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;
    public GameObject actionText;
    public GameObject door;
    private Animator doorAnimator;
    private bool gate = false;

    public MissionManager missionManager; // MissionManager referansı
    public GameObject finishDoorText; // Görevler tamamlanmadığında gösterilecek metin

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }

    private void OnMouseOver()
    {

        theDistance = PlayerRay.distanceFromTarget;
        actionKey.SetActive(false);
        actionText.SetActive(false);
        finishDoorText.SetActive(false);

      if (theDistance <= 2)
        {
          if (missionManager.AreAllMissionsCompleted())
           {
                Debug.Log("görevler tamamlanmış ve 2 den küçük mesafe");   
                actionKey.SetActive(true);
                actionText.SetActive(true);
                finishDoorText.gameObject.SetActive(false); // Görevler tamamlanmışsa uyarı metnini gizle

                if (Input.GetKeyDown(KeyCode.E))
                {
                    gate = !gate;
                    doorAnimator.SetBool("isOpen", gate);
                    this.gameObject.GetComponent<BoxCollider>().enabled = !gate;
                    actionKey.SetActive(false);
                    actionText.SetActive(false);
                    finishDoorText.gameObject.SetActive(false);
                }
           }
       
            else
            {
                actionKey.SetActive(false);
                actionText.SetActive(false);
                finishDoorText.gameObject.SetActive(true);
            }
        }
        else
        {
            // Görevler tamamlanmamışsa uyarı metnini göster
            finishDoorText.gameObject.SetActive(false);
            actionKey.SetActive(false);
            actionText.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
        finishDoorText.SetActive(false);
    }

}
