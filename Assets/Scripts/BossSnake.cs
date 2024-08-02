using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli kütüphane

public class BossSnake : MonoBehaviour
{
    [Header("Boss Snake HP")]
    public float HP = 100f; // Yılanın canı
    public float currentHp;
    public float damage = 10f; // Yılanın verdiği hasar
    public float damageTaken = 5f; // Yılanın aldığı hasar
    private HealthBar healthBarCanvas;
    public Transform player; // Karga karakteri

    [Header("Boss Snake Behavior")]
    public float detectionRadius = 10f; // Yılanın kargayı fark edeceği mesafe
    public float attackRadius = 3f; // Yılanın saldırıya geçeceği mesafe
    public float tauntDelay = 5f; // Yılanın taunt yapmadan önce bekleyeceği süre
    private Animator animator;
    private bool isTaunting = false;
    public bool isSnakeAttacking;
    private object butterrr;

    [Header("Scene Management")]
    public int nextSceneIndex; // Yüklenecek sonraki sahnenin indeksi

    private void Start()
    {
        animator = GetComponent<Animator>();
        healthBarCanvas = GetComponentInChildren<HealthBar>();
        currentHp = HP;

        StartCoroutine(IdleBehavior());
    }

    private void Update()
    {
        // Karga'nın pozisyonuna göre mesafe hesapla
        float distance = Vector3.Distance(transform.position, player.position);

        // Karga'nın yaklaştığını algıla ve animasyonları çalıştır
        if (distance <= detectionRadius && distance > attackRadius)
        {
            // Kargaya doğru bak
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Y ekseni sabit kalsın
            transform.rotation = Quaternion.LookRotation(direction);

            // GetUp animasyonunu çalıştır
            animator.SetTrigger("GetUp");

            // Taunt animasyonu için bekle
            if (!isTaunting)
            {
                isTaunting = true;
                StartCoroutine(TauntBehavior());
            }
        }
        else if (distance <= attackRadius)
        {
            // Saldırı animasyonu
            isSnakeAttacking = true;
            animator.SetBool("isSnakeAttacking", true);
            //animator.SetTrigger("Attack");
        }

        if (distance > attackRadius)
        {
            isSnakeAttacking = false;
            animator.SetBool("isSnakeAttacking", false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        CrowLocomotion crowController = other.gameObject.GetComponent<CrowLocomotion>();
        if (crowController != null && crowController.isAttacking)
        {
            healthBarCanvas.TakeDamage(damageTaken);            // 5 hasar alıyor.
            currentHp = healthBarCanvas.health;
            if (currentHp <= 0)
            {
                Die();
            }
        }
        else if (crowController != null && isSnakeAttacking)
        {
            crowController.crowHealthBar.TakeDamage(damage);        // 10 hasar kargaya veriyor.
            crowController.currenctCrowHp = crowController.crowHealthBar.health;
            if (crowController.currenctCrowHp <= 0)
            {
                crowController.Die();
            }
        }
    }

    private IEnumerator IdleBehavior()
    {
        // IdleGround animasyonu oynat
        animator.SetBool("isIdleGround", true);
        yield return null;
    }

    private IEnumerator TauntBehavior()
    {
        // IdleTaunt animasyonunu çalıştırmadan önce bekle
        yield return new WaitForSeconds(tauntDelay);
        animator.SetTrigger("Taunt");
        isTaunting = false;
    }

    public void Die()
    {
        Debug.Log("Boss Snake is dead!");
        animator.SetTrigger("Die");

        // Ölüm animasyonu bittikten sonra sahneyi değiştirmek için bir coroutine başlat
        StartCoroutine(ChangeSceneAfterDeath());
    }

    private IEnumerator ChangeSceneAfterDeath()
    {
        // Ölüm animasyonunun süresini bekle (örneğin 2 saniye)
        yield return new WaitForSeconds(2f);

        // Yeni sahneyi yükle (sahne indeksini belirtin)
        SceneManager.LoadScene(nextSceneIndex);
    }
}
