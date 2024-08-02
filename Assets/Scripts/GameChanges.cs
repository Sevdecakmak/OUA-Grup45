using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChanges : MonoBehaviour
{
    // Oyun içerisinde değiştirilecek sahneler için kapının önüne bir collider konulacak ve trigger aktif olacak. Player oraya geldiği zaman Canvasta oraya sabitlenmiş bir E - Interact yazısı belirecek. 
    // Oyuncu E tuşuna bastığı zaman bu script çalışacak ve belirtilen sahneye geçiş yapılacak.
    public string changeSceneName;
    public GameObject interactButton;
    void Start()
    {
        interactButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            interactButton.SetActive(true);
        }
    }

    // Bu metod ise interact butonuna basıldığında çalışacak.
    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(changeSceneName);
    }
}
