using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private int numOfCorrectAnswers = 3;
    private int currentLevel = 1;
    private int currentlySelectedCards = 0;



    // TODO: check to see when player has clicked multiple answers
    // add a levelup function
    // add a change difficulty function based of level ranges

    void Start()
    {

    }

    void Update()
    {

    }
    /// <summary>
    /// returns the number of correct answers
    /// </summary>
    /// <returns></returns>
    public int getNumberOfAnswers()
    {
        return numOfCorrectAnswers;
    }
    /// <summary>
    /// update how many cards the player has selected
    /// </summary>
    /// <param name="value"></param>
    public void setCurrentlySelectedCards(int value = 1)
    {
        currentlySelectedCards += value;
    }

}
