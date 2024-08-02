using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowHealth : MonoBehaviour
{
    public HealthBar crowHealthBar;
    public float healingAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kuşun canı artacak
            crowHealthBar.Heal(healingAmount);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kuşun canı artacak
            crowHealthBar.Heal(healingAmount);
        }
    }
}