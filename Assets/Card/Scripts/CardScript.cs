using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardScript : MonoBehaviour {

    private bool correctCard = false;
    private bool clickedCard = false;
    CardManager cManager;
    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite cardFront;
    [SerializeField]
    private Sprite cardBlank;

    private void Start()
    {
        cManager = GameObject.Find("SceneManager").GetComponent<CardManager>();
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
            this.GetComponent<Image>().sprite = cardFront;
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
