using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Metin göstermek için TextMeshPro bileşeni
    public string[] sentences;  // Diyalog metinlerini içeren dizi
    private int index = 0;  // Şu anki diyalog cümlesinin indeksini tutar
    public float typingSpeed = 0.05f;  // Metnin harf harf yazılma hızı

    private bool isTyping = false;  // Metin yazılırken başka bir işlemi engellemek için
    private bool isReady = false;  // Diyalog ilerlemeye hazır mı?

    public GameObject megaStar;  // Belirli bir olayda etkinleştirilecek oyun nesnesi
    public GameObject primogem;  // Belirli bir olayda etkinleştirilecek oyun nesnesi

    private bool firstAttack = false;  // İlk saldırı tetikleyicisi
    private bool secondAttack = true;  // İkinci saldırı tetikleyicisi

    private void Start()
    {
        StartCoroutine(TypeSentence());  // İlk cümleyi yazdır
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isReady && !isTyping)  // Z tuşuna basıldığında ve metin yazılmıyorken
        {
            if (!firstAttack)  // İlk saldırı gerçekleştirilmediyse
            {
                firstAttack = true;
                megaStar.SetActive(true);  // MegaStar objesini etkinleştir
            }
            else if (secondAttack)  // İkinci saldırı gerçekleştirilmediyse
            {
                secondAttack = false;
                primogem.SetActive(true);  // Primogem objesini etkinleştir
            }
            else
            {
                NextSentence();  // Diyalogları ilerlet
            }
        }
    }

    IEnumerator TypeSentence()
    {
        isTyping = true;  // Metin yazılmaya başlandı
        dialogueText.text = "";  // Mevcut metni temizle

        foreach (char letter in sentences[index].ToCharArray())  // Metni harf harf ekrana yazdır
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;  // Metin yazma işlemi bitti
        isReady = true;  // Yeni diyalog için hazır
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)  // Eğer daha fazla cümle varsa
        {
            index++;  // Sonraki cümleye geç
            StartCoroutine(TypeSentence());  // Sonraki cümleyi yaz
        }
        else
        {
            dialogueText.text = "";  // Diyaloglar bittiğinde metni temizle
            isReady = false;
        }
    }
}
