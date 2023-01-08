using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField]List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defualtAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    [SerializeField] Slider progressBar;
    public bool isComplete;
    
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar = FindObjectOfType<Slider>();
        progressBar.maxValue = questions.Count ;
        progressBar.value = 0;
        //Debug.Log(isComplete);
    }

     void LateUpdate()
    {
        //timerImage.fillAmount = timer.fillFraction;

    }
   void Update()
   {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
            isComplete = true;
            return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;

        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            
            
            
            SetButtonState(false);
            DisplayAnswer(6);
        }
   }
    public void OnAnswerSelected (int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false); 
        timer.CancelTimer();
        scoreText.text = "Score:" + scoreKeeper.CalculateScore() + "%";
        
    }
    void DisplayAnswer(int index)
    {
        
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
            

        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string answerIndex = currentQuestion.Getanswer(correctAnswerIndex);
            questionText.text = "Incorrect! The Correct Answer Is:" + answerIndex;
            Image buttonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            //Invoke("SetDefaultButtonSprites", 5f);

        }
    }

    public void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementQuestionsSeen();
            progressBar.value ++;
        }

        
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }

    public void DisplayQuestion()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = currentQuestion.Getanswer(i);
        }
        questionText.text = currentQuestion.GetQuestion();
    }
    void SetButtonState(bool state)
    {
        for(int i=0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defualtAnswerSprite;
        }
    }
    
}
 