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

    private void Awake()
    {
        levelText = GameObject.Find("LevelNumberText").GetComponent<TextMeshProUGUI>();
        dManager = GameObject.Find("SceneManager").GetComponent<DifficultyManager>();
        startRound();
    }


    private void startRound()
    {
        if (currentLevel >= 20)
            dManager.levelDifficultyThree();
        else if (currentLevel >= 10 && currentLevel < 20)
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
        if (currentLevel >= 10 && currentLevel < 20)
            dManager.levelDifficultyTwo();
        else if (currentLevel >= 20)
            dManager.levelDifficultyThree();
        else
            Debug.Log("Error: SetCurrentLevel Function - LevelManager Script");
    }
}
