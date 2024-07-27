using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBlockerController : MonoBehaviour
{
    public List<GameObject> transitionBlockers;

    public void SetBlockersActive(bool isActive)
    {
        foreach (GameObject blocker in transitionBlockers)
        {
            blocker.SetActive(isActive);
        }
    }
}
