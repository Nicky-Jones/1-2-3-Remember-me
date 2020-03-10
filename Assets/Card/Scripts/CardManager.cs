﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    GameObject[] cards;
    LevelManager lManager;
    DifficultyManager dManager;
    [SerializeField]
    private int currentlySelectedCards = 0;
    private int cardClickable = 0;
    private int checkAnswerDelay = 0;
    private int correctAnswer = 0;
    [SerializeField]
    private float delayDuration = 5.0f;
    [SerializeField]
    private float countDown = 5.0f; 
    [SerializeField]
    private float endRoundDuration = 5.0f;
    private int roundProgression = 0;
    private int canCountDown = 0;
    private int updateSwitch = 0;

    //TODO:
    // Add functionality to determin if the cards the player has selected are correct - completed but needs to be more flushed out by creating a display for success or failure.
    // if the cards are correct, advance to the next level, otherwise repeat same level with randomised cards - again - completed.
    // Add some kind of functionality to show the player which card they've currently clicked, at the moment its not immediately obvious after clicking the first one/two which ones you actually clicked
    // perhaps this could be returned to how it is now if a "hard" mode is ever designed.
    // add another difficulty that implements a new type of card they have to avoid clicking, e.g. 1 of hearts as it closely resembles the 1 of diamonds.

    private void Start()
    {
        cards = GameObject.FindGameObjectsWithTag("Card");
        lManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();
        dManager = GameObject.Find("SceneManager").GetComponent<DifficultyManager>();

        countDown = delayDuration;
        randomiseCards();
        StartCoroutine(roundDelay());
    }

    private void Update()
    {
        if (currentlySelectedCards == dManager.getNumberOfAnswers() && updateSwitch == 0 && checkAnswerDelay == 0)
        {
            disableClickableCards();
            checkAnswerDelay = 1;
            canCountDown = 1;
        }
        else if(currentlySelectedCards != dManager.getNumberOfAnswers() && checkAnswerDelay == 1)
        {
            resetCountDown(delayDuration);
            canCountDown = 0;
        }
        if(countDown < 0.0f && updateSwitch == 0)
        {
            resetCountDown(delayDuration);
            confirmAnswers();
            updateSwitch = 1;
        }
        if(updateSwitch == 1 && roundProgression == 0)
        {
            Debug.Log("updateSwitch = 1");
            roundProgression = 1;
            StartCoroutine(displayCards(3));
            canCountDown = 1;
        }
        if(countDown < 0.0f && roundProgression == 1)
        {
            resetCountDown(delayDuration);
            changeLevel();
        }
        if(canCountDown == 1 && countDown > 0.0f)
        {
            countDown -= Time.unscaledDeltaTime;
        }
    }

    /// <summary>
    /// Restarts the script back to default
    /// </summary>
    private void resetCardManager()
    {
        currentlySelectedCards = 0;
        cardClickable = 0;
        checkAnswerDelay = 0;
        updateSwitch = 0;
        canCountDown = 0;
        correctAnswer = 0;
        resetCountDown(delayDuration);
        roundProgression = 0;
        foreach (GameObject card in cards)
        {
            card.GetComponent<CardScript>().resetCard();
        }
    }

    /// <summary>
    /// Start a new round
    /// </summary>
    private void startRound()
    {
        resetCardManager();
        randomiseCards();
        StartCoroutine(roundDelay());
    }

    /// <summary>
    /// A function that checks to see if the cards the player clicked are the correct ones
    /// updates the level manager if they are correct and restarts the round
    /// or if they aren't correct just restart the round.
    /// </summary>
    private void confirmAnswers()
    {
        foreach (GameObject card in cards)
        {
            if(card.GetComponent<CardScript>().GetClickedCard() == true && card.GetComponent<CardScript>().getCorrectCard() == true)
            {
                correctAnswer += 1;
            }
        }
    }
    /// <summary>
    /// Calls the function in Level manager to change the level text if the player got all the correct answers, otherwise it simply resets.
    /// </summary>
    private void changeLevel()
    {
        Debug.Log("inside changeLevel function");
        if (correctAnswer == dManager.getNumberOfAnswers())
        {
            Debug.Log("inside correctAnswer Function");
            lManager.setCurrentLevel();
        }
        startRound();
        Debug.Log("inside end of changeLevel function");
    }

    /// <summary>
    /// a delay function to display the cards to the player for a determined time
    /// </summary>
    /// <param name="delay">The delay in how long the player can view what the function does</param>
    /// <returns>returns nothing after a set delay</returns>
    IEnumerator displayCards(int delay = 1)
    {
        Debug.Log("inside displayCards Function");
        foreach (GameObject card in cards)
        {
            card.GetComponent<CardScript>().changeImage();
        }


        yield return new WaitForSeconds(delay);

        foreach (GameObject card in cards)
        {
            card.GetComponent<CardScript>().defaultImage();
        }
    }

    /// <summary>
    /// Randomises a number that isn't a duplicate
    /// </summary>
    /// <returns></returns>
    private int randomNumber(List<int> number)
    {
        int randNum = Random.Range(0, cards.Length);
        int temp = 0;
        while (temp == 0)
        {
            if (number.Contains(randNum) == true)
            {
                randNum = Random.Range(0, cards.Length);
            }
            else
                temp = 1;
        }
        number.Add(randNum);
        return randNum;
    }

    /// <summary>
    /// randomises the cards sprite
    /// </summary>
    public void randomiseCards()
    {
        List<int> numberGenerator = new List<int>();
        int temp = 1;
        for (int i = 0; i < cards.Length; i++)
        {
            if (temp <= dManager.getNumberOfAnswers())
            {
                int randNumber;
                randNumber = randomNumber(numberGenerator);
                cards[randNumber].GetComponent<CardScript>().setCorrectCard(true);
                temp += 1;
            }
            else
                return;
        }
    }

    /// <summary>
    /// initial round delay for the player to prepare themselves
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator roundDelay(int delay = 2)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(displayCards(2));
    }

    /// <summary>
    /// update how many cards the player has selected
    /// </summary>
    /// <param name="value"></param>
    public void setCurrentlySelectedCards(int value = 1)
    {
        currentlySelectedCards += value;
    }

    /// <summary>
    /// Disables all cards from being clickable except for the ones the player has already clicked
    /// </summary>
    private void disableClickableCards()
    {
        foreach(GameObject card in cards)
        {
            if (card.GetComponent<CardScript>().GetClickedCard() == false)
            {
                card.GetComponent<CardScript>().disableClick();
            }
        }
        cardClickable = 1;
    }

    /// <summary>
    /// if the player Unclicks a card after all availably correct cards have been clicked it returns the clickability of all other available cards
    /// </summary>
    public void enableClickableCards()
    {
        if (cardClickable == 1)
        {
            foreach (GameObject card in cards)
            {
                if (card.GetComponent<CardScript>().GetClickedCard() == false)
                {
                    card.GetComponent<CardScript>().enableClick();
                }
            }
            cardClickable = 0;
        }
    }

    /// <summary>
    /// resets the countdown variable based off a value.
    /// </summary>
    /// <param name="reset"></param>
    private void resetCountDown(float reset)
    {
        countDown = reset;
    }
}