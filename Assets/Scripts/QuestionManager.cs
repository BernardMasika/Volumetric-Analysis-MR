using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _answerUIQn1;
    [SerializeField] private TMP_Text _answerUIQn2;
    [SerializeField] private TMP_Text _answerUIQn3;

    public void CheckTheAnswer(string ans)
    {
        if (ans == "A")
        {
            if (_answerUIQn1.IsActive())
            {
                _answerUIQn1.text = "Correct Answer.!!";
                _answerUIQn1.color = Color.blue;
            } 
            if (_answerUIQn2.IsActive())
            {
                _answerUIQn2.text = "Correct Answer.!!";
                _answerUIQn2.color = Color.blue;
            }
            if (_answerUIQn3.IsActive())
            {
                _answerUIQn3.text = "Correct Answer.!!";
                _answerUIQn3.color = Color.blue;
            }
           
            
        }
        else
        {
            if (_answerUIQn1.IsActive())
            {
                _answerUIQn1.text = " NOT Correct";
                _answerUIQn1.color = Color.red;
            } 
            if (_answerUIQn2.IsActive())
            {
                _answerUIQn2.text = " NOT Correct";
                _answerUIQn2.color = Color.red;
            }
            if (_answerUIQn3.IsActive())
            {
                _answerUIQn3.text = " NOT Correct";
                _answerUIQn3.color = Color.red;
            }
        }
    }
  
}
