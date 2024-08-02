using UnityEngine;

public class Target : MonoBehaviour
{
    public float dropHeight; // Mücevherin bırakıldığı yükseklik
    private bool hasChecked = false; // Yüksekliği kontrol edip etmediğini belirten bayrak

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasChecked && collision.gameObject.CompareTag("Ground")) // Yere çarptığında kontrol et
        {
            hasChecked = true;

            if (dropHeight >= 3f)
            {
                // Mücevher belirtilen yüksekliğin üzerindeyse yok et
                Destroy(gameObject); // gameObject'in kendisini yok ediyoruz
                Debug.Log("Jewel destroyed due to high drop!");
            }
            else
            {
                // Mücevher belirtilen yüksekliğin altında ise, tekrar alınabilir
                Debug.Log("Jewel can be picked up again!");
            }
        }
    }
}
