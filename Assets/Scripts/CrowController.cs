using UnityEngine;

public class CrowController : MonoBehaviour
{
    public Transform jewel; // Mücevher objesi
    public float moveSpeed = 2f; // Hareket hızı
    public float verticalMoveSpeed = 3f; // Yukarı ve aşağı hareket hızı
    private bool isHolding = false; // Başlangıçta mücevheri tutmuyor

    void Update()
    {

        // Kargayı yatayda ve dikeyde hareket ettirmek için
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Yukarı ve aşağı hareket
        float moveUp = 0f;
        if (Input.GetKey(KeyCode.Space))
        {
            moveUp = verticalMoveSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            moveUp = -verticalMoveSpeed;
        }

        Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Mücevheri tutma ve bırakma
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHolding)
            {
                DropJewel();
            }
            else
            {
                PickupJewel();
            }
        }

        // Mücevheri tutarken mücevherin konumunu güncelle
        if (isHolding)
        {
            jewel.position = transform.position + new Vector3(0, -1, 0); // Mücevheri kuşun altına yerleştir
        }
    }

    void DropJewel()
    {
        isHolding = false;
        jewel.SetParent(null);
        jewel.GetComponent<Rigidbody>().isKinematic = false; // Mücevherin fiziksel etkilerini aç
    }

    void PickupJewel()
    {
        if (jewel != null && Vector3.Distance(transform.position, jewel.position) < 1f)
        {
            isHolding = true;
            jewel.SetParent(transform);
            jewel.localPosition = new Vector3(0, -1, 0); // Mücevheri karganın altına yerleştir
            jewel.GetComponent<Rigidbody>().isKinematic = true; // Mücevherin fiziksel etkilerini kapat
        }
    }
}
