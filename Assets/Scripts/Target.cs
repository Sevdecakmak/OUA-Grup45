using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // Mücevher zemine çarptığında yapılacak işlemler
            Debug.Log("Jewel dropped successfully!");
        }
    }
}
