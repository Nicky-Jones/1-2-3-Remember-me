using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    GameObject[] cards;
    LevelManager lManager;
    [SerializeField]
    private int currentlySelectedCards = 0;
    private int cardClickable = 0;
    private int checkAnswerDelay = 0;
    private int correctAnswer = 0;
    List<int> numberGenerator = new List<int>();
    [SerializeField]
    private float delayDuration = 5.0f;
    private float countDown; 
    [SerializeField]
    private float endRoundDuration = 5.0f;
    private int roundProgression = 0;

    //TODO:
    // Add functionality to determin if the cards the player has selected are correct - completed but needs to be more flushed out by creating a display for success or failure.
    // if the cards are correct, advance to the next level, otherwise repeat same level with randomised cards - again - completed.
    // Add some kind of functionality to show the player which card they've currently clicked, at the moment its not immediately obvious after clicking the first one/two which ones you actually clicked
    // perhaps this could be returned to how it is now if a "hard" mode is ever designed.

    private void Start()
    {
        cards = GameObject.FindGameObjectsWithTag("Card");
        lManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();

        countDown = delayDuration;
        randomiseCards();
        StartCoroutine(roundDelay());
    }

    private void Update()
    {
        // checks to see if the number of cards selected 
        if (currentlySelectedCards == lManager.GetComponent<LevelManager>().getNumberOfAnswers() && roundProgression == 0)
        {
            disableClickableCards();
            checkAnswerDelay = 1;
            countDown -= Time.deltaTime;
            if (currentlySelectedCards != 3 && checkAnswerDelay == 1)
            {
                resetCountDown(delayDuration);
                return;
            }
            if(countDown < 0)
            {
                confirmAnswers();
                roundProgression = 1;
                resetCountDown(delayDuration);
            }
        }
        if(roundProgression == 1)
        {
            
            if (countDown == delayDuration)
            {
                StartCoroutine(displayCards(3));
            }
            countDown -= Time.deltaTime;
            if(countDown < 0)
            {
                changeLevel();
            }
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
        if (correctAnswer == lManager.getNumberOfAnswers())
        {
            lManager.setCurrentLevel();
        }
        startRound();
        Debug.Log("inside changeLevel function");
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
    /// Restarts the script back to default
    /// </summary>
    private void resetCardManager()
    {
        currentlySelectedCards = 0;
        cardClickable = 0;
        checkAnswerDelay = 0;
        correctAnswer = 0;
        resetCountDown(delayDuration);
        roundProgression = 0;
        foreach (GameObject card in cards)
        {
            card.GetComponent<CardScript>().resetCard();
        }
    }

    /// <summary>
    /// Randomises a number that isn't a duplicate
    /// </summary>
    /// <returns></returns>
    private int randomNumber()
    {
        int randNum = Random.Range(0, cards.Length);
        if(numberGenerator.Contains(randNum))
        {
            randNum = Random.Range(0, cards.Length);
        }
        else
            numberGenerator.Add(randNum);

        return randNum;
    }

    /// <summary>
    /// randomises the cards sprite
    /// </summary>
    public void randomiseCards()
    {
        int temp = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            if (temp <= 2)
            {
                int randNumber;
                randNumber = randomNumber();
                cards[randNumber].GetComponent<CardScript>().setCorrectCard(true);
                temp += 1;
            }
            if (temp >= 4)
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
