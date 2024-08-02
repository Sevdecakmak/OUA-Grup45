using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class StartSceneCameraController : MonoBehaviour
{
    [SerializeField]
    private float duration;

    public void LookAt(Transform transform1)
    {
        transform.DOLookAt(transform1.position, duration);
    }
}
