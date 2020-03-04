using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite cardFront;
    [SerializeField]
    private Sprite cardBlank;
    GameObject[] cards;
    LevelManager lManager;


    // TODO: When the available number of answers are clicked deactive the click ability of all remaining cards
    // Add a timer for how long the player has to memorise the correct cards
    // Turn randomise card function & clickable/declickable & return to default into seperate classes that are children of the CardManager class.


   
    //would it be better to randomise the cards and show them during the randomisation, or randomise them and then create a timed function to show the correct cards????? - ask Liam his opinion



    private void Start()
    {
        cards = GameObject.FindGameObjectsWithTag("Card");
        lManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();
    }
    /// <summary>
    /// randomises the cards sprite
    /// </summary>
    public void randomiseCards()
    {
        int temp = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            int rand = Random.Range(0, 1);
            if (rand == 1)
            {
                if (temp == lManager.getNumberOfAnswers())
                    return;
                cards[i].GetComponent<Image>().sprite = cardFront;
                cards[i].GetComponent<CardScript>().setCorrectCard(true);
                temp = 1;
            }
            else
            {
                cards[i].GetComponent<Image>().sprite = cardBlank;
            }
        }
    }
    /// <summary>
    /// set the cards sprite back to default
    /// </summary>
    public void setToDefault()
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<Image>().sprite = cardBack;
        }
    }
}
