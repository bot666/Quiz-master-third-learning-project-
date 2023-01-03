using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defualtAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    int correctAnswerIndex;
    
    bool Done = false;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < answerButtons.Length; i++)
            {
                TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        
                buttonText.text = question.Getanswer(i);
            }
            questionText.text = question.GetQuestion();

    }
    public void OnAnswerSelected (int index)
    {
        
        if (index == question.GetCorrectAnswerIndex() && Done == false)
        {
            questionText.text = "Correct!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            Done = true;
            
        }
        else if (Done == false)
        {
            correctAnswerIndex = question.GetCorrectAnswerIndex();
            string answerIndex = question.Getanswer(correctAnswerIndex);
            questionText.text = "Incorrect! The Correct Answer Is:" +  answerIndex;
            Image buttonImage = answerButtons[question.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            Done = true;
        }
        

    }


}
 