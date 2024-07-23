using UnityEngine;
using UnityEngine.UI;

public class answer_checker : MonoBehaviour
{
    [System.Serializable]
    public struct Question
    {
        public Image firstEmoji;
        public Image secondEmoji;
        public Image answerImage;
        public Image crossImageTik;
        public Image crossImageCarpi;

        public Sprite correctAnswer;
        public Sprite image1;
        public Sprite image2;
    }

    public Question[] questions; // Sorular
    public Button checkButton;

    void Start()
    {
        checkButton.onClick.AddListener(CheckAnswers);
        foreach (var question in questions)
        {
            question.crossImageTik.gameObject.SetActive(false); // Başlangıçta çarpı işaretlerini gizle
            question.crossImageCarpi.gameObject.SetActive(false);
        }
    }

    void CheckAnswers()
    {
        bool allCorrect = true;
        foreach (var question in questions)
        {
            if (question.answerImage.sprite == question.correctAnswer)
            {
                question.crossImageTik.gameObject.SetActive(true); // Cevap doğruysa çarpı işaretini gizle
                question.crossImageCarpi.gameObject.SetActive(false);
            }
            else
            {
                question.crossImageCarpi.gameObject.SetActive(true); // Cevap yanlışsa çarpı işaretini göster
                question.crossImageTik.gameObject.SetActive(false);
                allCorrect = false;
            }
        }

        if (allCorrect)
            Debug.Log("Hepsi Doğru!");
        else
            Debug.Log("Hata var!");
    }
}
