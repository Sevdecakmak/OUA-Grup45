using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    public List<GameObject> flames;

    public void SetFlamesActive(bool isActive)
    {
        foreach (GameObject flame in flames)
        {
            flame.SetActive(isActive);
        }
    }
}
