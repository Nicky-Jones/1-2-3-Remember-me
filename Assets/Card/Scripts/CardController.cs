using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardController : MonoBehaviour
{

    GameObject[] cards;
    LevelManager lManager;
    DifficultyManager dManager;
    UIManager uiManager;
    private int currentlySelectedCards = 0;
    private int cardClickable = 0;
    private int checkAnswerDelay = 0;
    private int correctAnswer = 0;
    [HeaderAttribute("Adjustable values")]
    [Tooltip("Value for how long you want the player to have before his choices are locked in")]
    [SerializeField]
    private float delayDuration = 5.0f;
    [Tooltip("Changable value for adjusting the length at which the delay of checking the cards for the banner animation")]
    [SerializeField]
    private int endRoundDuration = 5;
    private float countDown = 5.0f;
    private int roundProgression = 0;
    private int canCountDown = 0;
    private int updateSwitch = 0;
    private AdsManager adManager;
    private bool pauseCoroutine;


    private void Start()
    {
        cards = GameObject.FindGameObjectsWithTag("Card");
        lManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();
        dManager = GameObject.Find("SceneManager").GetComponent<DifficultyManager>();
        uiManager = GameObject.Find("SceneManager").GetComponent<UIManager>();
        adManager = (AdsManager)FindObjectOfType(typeof(AdsManager));
        countDown = delayDuration;
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
            checkAnswerDelay = 0;
        }
        if(countDown < 0.0f && updateSwitch == 0)
        {
            resetCountDown(endRoundDuration);
            confirmAnswers();
            updateSwitch = 1;
        }
        if(updateSwitch == 1 && roundProgression == 0)
        {
            roundProgression = 1;
            StartCoroutine(displayCards(endRoundDuration, 1));
            canCountDown = 1;
        }
        if(canCountDown == 1 && countDown > 0.0f)
            countDown -= Time.unscaledDeltaTime;
    }

    /// <summary>
    /// Inital GameStart function
    /// </summary>
    public void startGame()
    {
        if (lManager.getCurrentLevel() <= 30 && lManager.getCurrentLevel() > 0)
        {
            randomiseCards();
            StartCoroutine(roundDelay());
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
        if (lManager.getCurrentLevel() <= 30)
        { 
            randomiseCards();
            StartCoroutine(roundDelay());
        }
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
        if (correctAnswer == dManager.getNumberOfAnswers())
        {
            lManager.setCurrentLevel();
        }
        startRound();
    }

    /// <summary>
    /// a delay function to display the cards to the player for a determined time
    /// </summary>
    /// <param name="delay">The delay in how long the player can view what the function does</param>
    /// <returns>returns nothing after a set delay</returns>
    IEnumerator displayCards(int delay = 1, int endOfLevel = 0)
    {
        if (adManager.adStarted == true)
            pauseCoroutine = true;
        else if (adManager.adStarted == false)
            pauseCoroutine = false;
        foreach (GameObject card in cards)
        {
            card.GetComponent<CardScript>().changeImage();
            if (endOfLevel == 1)
                card.GetComponent<CardScript>().removePanel();
        }

        if (endOfLevel == 1)
        {
            if (lManager.getCurrentLevel() <= 30)
            {
                if (correctAnswer == dManager.getNumberOfAnswers())
                {
                    uiManager.setLevelPassBanner(true);
                }
                else
                    uiManager.setLevelFailBanner(true);
            }
            else
            {
                uiManager.completedGame();
            }
            endOfLevel = 2;
        }

        if (adManager.adStarted != true)
        {
            yield return new WaitForSeconds(delay);

            foreach (GameObject card in cards)
                card.GetComponent<CardScript>().defaultImage();

            if (endOfLevel == 2)
            {
                uiManager.setLevelPassBanner(false);
                uiManager.setLevelFailBanner(false);
            }
            if(roundProgression == 1)
            {
                changeLevel();
            }
        }
        else
            yield return null;
        

    }

    /// <summary>
    /// Randomises a number that isn't a duplicate
    /// </summary>
    /// <returns></returns>
    private int randomNumberGenerator(List<int> number)
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
        List<int> numberGeneratorList = new List<int>();
        int temp = 1;
        for (int i = 0; i < cards.Length; i++)
        {
            if (temp <= dManager.getNumberOfAnswers())
            {
                int randNumber;
                randNumber = randomNumberGenerator(numberGeneratorList);
                cards[randNumber].GetComponent<CardScript>().setCard(1);
                if (dManager.getLevelDifficulty() == 3)
                {
                    randNumber = randomNumberGenerator(numberGeneratorList);
                    cards[randNumber].GetComponent<CardScript>().setCard(2);
                }
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