using UnityEngine;
using UnityEngine.SceneManagement;

public class JewelMainHall : MonoBehaviour
{
    // Bu script'i `jewel` objesine ekle
    void OnTriggerEnter(Collider other)
    {
        // Eğer oyuncu (veya belirli bir tag'e sahip başka bir nesne) `jewel` objesine çarparsa
        if (other.CompareTag("Player"))
        {
            // 'mainHall' sahnesine geri dön
            SceneManager.LoadScene("Main_Hall");
        }
    }
}
