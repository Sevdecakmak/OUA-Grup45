using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float flySpeed = 5f;
    public float attackDistance = 2f;
    public Transform target; // Saldırılacak hedef
    private bool isFlying = false;
    private bool isAttacking = false;
  
    void Update()
    {
        // Yürüme
        if (Input.GetKey(KeyCode.W))
        {
            Walk();
        }

        // Uçma
        if (Input.GetKey(KeyCode.Space))
        {
            Fly();
        }

        // Saldırma
        if (target != null && Vector3.Distance(transform.position, target.position) < attackDistance)
        {
            Attack();
        }

       Debug.Log($"Walking: {!isFlying && !isAttacking}, Flying: {isFlying}, Attacking: {isAttacking}");
    }

    void Walk()
    {
        isFlying = false;
        isAttacking = false;
        transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
    }

    void Fly()
    {
        isFlying = true;
        isAttacking = false;
        transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
    }

    void Attack()
    {
        isAttacking = true;
        isFlying = false;
        // Saldırı davranışı burada eklenebilir (örneğin hasar verme)
    }
}
