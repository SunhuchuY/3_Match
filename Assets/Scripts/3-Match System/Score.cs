using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Fever _fever;

    [SerializeField]
    private TMP_Text _scoreText;

    private int _score = 0;

    [SerializeField]
    public int score 
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            _scoreText.text = $"{value}";
        }

    }


    private void Awake()
    {
        score = 0;    
    }

    public void AttributeScore(int attributeAmount)
    {
        score += attributeAmount;
        _fever.AttributeFever(attributeAmount);
    }
}
