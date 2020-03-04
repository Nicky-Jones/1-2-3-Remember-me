using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {

    private bool correctCard = false;
    private bool clickedCard = false;
    LevelManager lManager;

    private void Start()
    {
        lManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();
    }
    /// <summary>
    /// tag the card to inform itself that it is one of the correct cards
    /// </summary>
    /// <param name="decision"></param>
    public void setCorrectCard(bool decision)
    {
        correctCard = decision;
    }
    /// <summary>
    /// checks to see if the player has selected this card
    /// </summary>
    private void setClickedCard()
    {
        clickedCard = true;
        lManager.setCurrentlySelectedCards();
    }
    /// <summary>
    /// returns true if the card has been clicked by the player
    /// </summary>
    /// <returns></returns>
    public bool GetClickedCard()
    {
        if (clickedCard == true)
            return true;
        else
            return false;
    }
}
