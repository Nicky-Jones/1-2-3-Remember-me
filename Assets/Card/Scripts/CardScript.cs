﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardScript : MonoBehaviour {

    [SerializeField]
    private bool correctCard = false;
    private bool clickedCard = false;
    private bool heartCard = false;
    CardController cManager;
    DifficultyManager dManager;
    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite cardDiamond;
    [SerializeField]
    private Sprite cardHeart;
    [SerializeField]
    private Sprite cardBlank;

    private void Start()
    {
        cManager = GameObject.Find("SceneManager").GetComponent<CardController>();
        dManager = GameObject.Find("SceneManager").GetComponent<DifficultyManager>();
    }

    /// <summary>
    /// if the Decision = 1 the card is correct but if the decision = 2 I want the card to display a heart version to make things more complicated for the player to remember
    /// this function can now be further modified if a user wants to add more cards to trip players up in the future.
    /// </summary>
    /// <param name="decision"> if decision = 1 the card is correct if decision = 2 the card becomes a heart card</param>
    public void setCard(int decision)
    {
        if (decision == 1)
            correctCard = true;
        else if (decision == 2)
            heartCard = true;
        else
            Debug.Log("Error in SetCard function - CardScript");
    }

    /// <summary>
    /// tags the card to confirm the player has either clicked or unclicked this card
    /// </summary>
    public void setClickedCard()
    {
        if (clickedCard == true)
        {
            clickedCard = false;
            cManager.setCurrentlySelectedCards(-1);
            cManager.enableClickableCards();
            return;
        }
        clickedCard = true;
        cManager.setCurrentlySelectedCards();
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

    /// <summary>
    /// returns true if the card is the correct one
    /// </summary>
    /// <returns></returns>
    public bool getCorrectCard()
    {
        if (correctCard == true)
            return true;
        else
            return false;
    }

    /// <summary>
    /// resets the card back to default settings
    /// </summary>
    public void resetCard()
    {
        defaultImage();
        enableClick();
        correctCard = false;
        clickedCard = false;
    }

    /// <summary>
    /// change the image of the card depending on if the card is a correct one or not
    /// </summary>
    public void changeImage()
    {
        if(correctCard == true)
        {
            this.GetComponent<Image>().sprite = cardDiamond;
        }
        else if(heartCard == true)
        {
            this.GetComponent<Image>().sprite = cardHeart;
        }
        else
        {
            this.GetComponent<Image>().sprite = cardBack;
        }
    }

    /// <summary>
    /// Changes the card back to default sprite
    /// </summary>
    public void defaultImage()
    {
        this.GetComponent<Image>().sprite = cardBack;
    }

    /// <summary>
    /// Disables the Clickable function of the card
    /// </summary>
    public void disableClick()
    {
        this.GetComponent<Button>().interactable = false;
    }

    /// <summary>
    /// Enables the clickable function of the card
    /// </summary>
    public void enableClick()
    {
        this.GetComponent<Button>().interactable = true;
    }

}
