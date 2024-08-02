using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Awake() {
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate() {
        transform.LookAt(transform.position + mainCameraTransform.forward);
    }
}
