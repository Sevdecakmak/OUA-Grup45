using UnityEngine;

public class Target : MonoBehaviour
{
    public float dropHeight; // Mücevherin bırakıldığı yükseklik
    private bool hasChecked = false; // Yüksekliği kontrol edip etmediğini belirten bayrak

    public float theDistance;
    public GameObject actionKey;
    public GameObject actionText;

    private void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (theDistance <= 2)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);
        }
        else
        {
            actionKey.SetActive(false);
            actionText.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (!hasChecked)
        {
            if (collision.gameObject.CompareTag("Ground")) // Yere çarptığında kontrol et
            {
                hasChecked = true;
                Debug.Log("Drop height: " + dropHeight);

                if (dropHeight >= 3f)
                {
                    Debug.Log("Destroying object: " + gameObject.name);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Jewel can be picked up again!");
                }
            }
            else
            {
                Debug.Log("Collision ignored, not with Ground.");
            }
        }
        else
        {
            Debug.Log("Collision ignored, already checked.");
        }
    }




}
