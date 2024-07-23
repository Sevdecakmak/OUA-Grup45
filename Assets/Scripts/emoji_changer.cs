using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class emoji_changer : MonoBehaviour, IPointerClickHandler
{
    public Image answerImage;
    public Sprite[] possibleAnswers; // Farklı emoji görselleriniz

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
