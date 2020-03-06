using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    private int numOfCorrectAnswers = 3;
    private int currentLevel = 1;
    TextMeshProUGUI levelText;

    // TODO:
    // add a levelup function - a rudimentry level system is currently in place, all it does at the moment is change the text of the level text object.
    // add a change difficulty function based of level ranges

    void Start()
    {
        levelText = GameObject.Find("LevelNumberText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

    }

    /// <summary>
    /// Changes the current level text by 1
    /// </summary>
    public void setCurrentLevel()
    {
        currentLevel += 1;
        levelText.text = currentLevel.ToString();
    }

    /// <summary>
    /// returns the number of correct answers
    /// </summary>
    /// <returns></returns>
    public int getNumberOfAnswers()
    {
        return numOfCorrectAnswers;
    }

}
