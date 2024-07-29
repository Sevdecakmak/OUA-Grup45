using UnityEngine;
using UnityEngine.UI;
public class AnswerChecker : MonoBehaviour
{
    public int mission_counter;

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
            question.crossImageTik.gameObject.SetActive(false); // Baþlangýçta çarpý iþaretlerini gizle
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
                question.crossImageTik.gameObject.SetActive(true); // Cevap doðruysa çarpý iþaretini gizle
                question.crossImageCarpi.gameObject.SetActive(false);
            }
            else
            {
                question.crossImageCarpi.gameObject.SetActive(true); // Cevap yanlýþsa çarpý iþaretini göster
                question.crossImageTik.gameObject.SetActive(false);
                allCorrect = false;
            }
        }

        if (allCorrect)
        {
            Debug.Log("Hepsi Doðru!");
            mission_counter++;
        }

        else
            Debug.Log("Hata var!");
    }
}
