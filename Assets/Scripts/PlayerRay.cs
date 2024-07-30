using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    public static float distanceFromTarget; // uzaklığı hesaplayacak
    public float toTarget; //gördüğüm her nesneye olan uzaklığı


    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit))
        {
            toTarget = hit.distance;
            distanceFromTarget = toTarget;
        }
    }
}
