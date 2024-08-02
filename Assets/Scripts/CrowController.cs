using UnityEngine;
using System.Collections;

public class CrowController : MonoBehaviour
{
    public Transform jewel; // Mücevher objesi
    public GameObject stone; // Stone objesi
    private bool isHolding = false; // Başlangıçta mücevheri tutmuyor
    public float dropHeight; // Mücevherin bırakıldığı yükseklik
    public float destroyDelay = 2f; // Animasyon süresi kadar bekleme süresi
    private bool isCrash = false; // Taş animasyonunu tetiklemek için kullanılacak boolean değişkeni
    private Animator stoneAnimator; // Stone objesinin Animator bileşeni

    void Start()
    {
        // Stone objesinin Animator bileşenini al
        if (stone != null)
        {
            stoneAnimator = stone.GetComponent<Animator>();
        }
    }

    void Update()
    {
        // Kargayı yatayda ve dikeyde hareket ettirmek için
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveUpDown = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            moveUpDown = 1f; // Yukarı çık
        }
        else if (Input.GetKey(KeyCode.C))
        {
            moveUpDown = -1f; // Aşağı in
        }

        Vector3 movement = new Vector3(moveHorizontal, moveUpDown, moveVertical);
        transform.Translate(movement * Time.deltaTime * 5f); // Hareket hızı

        // Mücevheri tutma ve bırakma
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L tuşuna basıldı");
            if (isHolding)
            {
                DropJewel();
            }
            else
            {
                PickupJewel();
            }
        }

        // Taş animasyonunu isCrash'e göre oynat
        if (isCrash && stoneAnimator != null)
        {
            // Animasyon klibini oynat
            stoneAnimator.Play("StoneAnimation"); // StoneAnimation: Animator'daki animasyon klip ismi
            isCrash = false; // Animasyonun oynatıldığını belirtmek için
            StartCoroutine(DestroyJewelAfterAnimation()); // Animasyon bitiminden sonra yok et
        }
    }

    void DropJewel()
    {
        isHolding = false;
        dropHeight = transform.position.y; // Bırakılan yüksekliği kaydet
        jewel.SetParent(null); // Mücevheri kargadan ayır
        Rigidbody jewelRigidbody = jewel.GetComponent<Rigidbody>();

        if (jewelRigidbody != null)
        {
            jewelRigidbody.isKinematic = false; // Mücevherin fiziksel etkilerini aç
            jewelRigidbody.useGravity = true; // Eğer gerekli ise, yer çekimini aktif et
        }

        // Mücevherin yüksekliğini Target bileşenine ata
        Target target = jewel.GetComponent<Target>();
        if (target != null)
        {
            target.dropHeight = dropHeight;
            Debug.Log("Drop height set to: " + target.dropHeight); // Hata ayıklama
        }

        // Mücevheri bırakıldıktan sonra yükseklik kontrolü yap
        if (dropHeight >= 3f)
        {
            Debug.Log("Triggering stone animation and delaying jewel destruction.");
            isCrash = true; // Taş animasyonunu tetiklemek için
        }
        else
        {
            Debug.Log("Jewel can be picked up again!");
        }
    }

    void PickupJewel()
    {
        if (jewel != null && Vector3.Distance(transform.position, jewel.position) < 3f)
        {
            isHolding = true;
            jewel.SetParent(transform);
            jewel.localPosition = new Vector3(0, -1, 0); // Mücevheri karganın altına yerleştir
            Rigidbody jewelRigidbody = jewel.GetComponent<Rigidbody>();
            if (jewelRigidbody != null)
            {
                jewelRigidbody.isKinematic = true; // Mücevherin fiziksel etkilerini kapat
            }

            // Drop height'ı doğru bir şekilde ayarla
            Target target = jewel.GetComponent<Target>();
            if (target != null)
            {
                target.dropHeight = dropHeight;
                Debug.Log("Drop height set to: " + target.dropHeight); // Hata ayıklama
            }
        }
        else
        {
            Debug.Log("Mücevher çok uzakta");
        }
    }

    private IEnumerator DestroyJewelAfterAnimation()
    {
        yield return new WaitForSeconds(destroyDelay); // Animasyon süresi kadar bekle
        if (jewel != null)
        {
            Destroy(jewel.gameObject); // Mücevheri yok et
        }
    }
}