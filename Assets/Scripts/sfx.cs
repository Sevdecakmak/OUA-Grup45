using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundEffect : MonoBehaviour
{
    public Button button; // Butonunuzu buraya atayın
    public AudioSource audioSource; // AudioSource bileşenini buraya atayın

    void Start()
    {
        // Butona tıklama olayını dinleyin
        button.onClick.AddListener(PlaySoundEffect);
    }

    void PlaySoundEffect()
    {
        // Ses efektini çal
        audioSource.Play();
    }
}
