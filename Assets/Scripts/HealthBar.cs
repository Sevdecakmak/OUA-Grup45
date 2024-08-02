using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100;
    public float health;
    private float lerpSpeed = 0.05f;
    void Start()
    {
        health = maxHealth;
        
    }
    void Update()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        // if(Input.GetKeyDown(KeyCode.N))     // Buraya gerekli hasar azaltma logici gelecek.
        // {
        //     TakeDamage(10);
        // }
        if(healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public void Heal(float healAmount)
    {
        health += healAmount;
    }
}
