using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
 [SerializeField] float timeToCompleteQuestions = 30f;
 [SerializeField] float timeToShowCorrectAnswer = 10f;
 public bool loadNextQuestion;
 public float fillFraction =10f;
 public bool isAnsweringQuestion;
  public float timerValue;

   
    void Update()
    {
        UpdateTimer();
        //Debug.Log(isAnsweringQuestion);
    }
    public void CancelTimer()
    {
        timerValue = 0;
    }
    
    public void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if(isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestions;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else 
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestions;
                loadNextQuestion = true;
            }
        }

 

       // Debug.Log(timerValue + ":" + fillFraction + ":" );
    }
    
}
