using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmojiChanger : MonoBehaviour, IPointerClickHandler
{
    public Image answerImage;
    public Sprite[] possibleAnswers; // Farkl� emoji g�rselleriniz

    private int currentAnswerIndex = 0;

    void Start()
    {
        if (possibleAnswers.Length > 0)
        {
            answerImage.sprite = possibleAnswers[currentAnswerIndex];
			
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeAnswer();
    }

    void ChangeAnswer()
    {
        currentAnswerIndex = (currentAnswerIndex + 1) % possibleAnswers.Length;
        answerImage.sprite = possibleAnswers[currentAnswerIndex];
    }
}
