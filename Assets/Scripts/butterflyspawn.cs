using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Kelebekleri içeren GameObject
    public GameObject butterflys;

    // Yılanın can puanı
    public int snakeHealth = 100;

    // Update metodu dövüşü simüle eder
    void Update()
    {
        // Örnek: Yılanın canını azalt
        // Bu kısmı kendi dövüş mekaniklerinize göre düzenleyebilirsiniz
        if (snakeHealth > 0)
        {
            // Yılanın canı her frame'de 1 azalıyor (örnek)
            snakeHealth--;
        }
        else
        {
            // Yılan öldü, kelebekleri aktif et
            ActivateButterflys();
        }
    }

    // Kelebekleri aktif eden fonksiyon
    private void ActivateButterflys()
    {
        if (butterflys != null)
        {
            butterflys.SetActive(true);
        }
    }
}
