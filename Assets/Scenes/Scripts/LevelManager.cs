using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int currentLevel = 1;
    TextMeshProUGUI levelText;
    DifficultyManager dManager;

    // TODO:
    // add a levelup function - a rudimentry level system is currently in place, all it does at the moment is change the text of the level text object.
    // add a change difficulty function based of level ranges

    private void Awake()
    {
        levelText = GameObject.Find("LevelNumberText").GetComponent<TextMeshProUGUI>();
        dManager = GameObject.Find("SceneManager").GetComponent<DifficultyManager>();
        startRound();
    }
    void Start()
    {
        
    }

    void Update()
    {

    }
    private void startRound()
    {
        if (currentLevel >= 10 && currentLevel < 20)
            dManager.levelDifficultyTwo();
        else
            dManager.levelDifficultyOne();
        levelText.text = currentLevel.ToString();
    }
    /// <summary>
    /// Changes the current level text by 1
    /// </summary>
    public void setCurrentLevel()
    {
        currentLevel += 1;
        levelText.text = currentLevel.ToString();
        if(currentLevel == 10)
        {
            dManager.levelDifficultyTwo();
        }
    }
}
