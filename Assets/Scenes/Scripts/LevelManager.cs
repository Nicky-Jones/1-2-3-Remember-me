using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int currentLevel = 1;
    UIManager uiManager;
    DifficultyManager dManager;

    // TODO:

    private void Awake()
    {
        dManager = GameObject.Find("SceneManager").GetComponent<DifficultyManager>();
        uiManager = GameObject.Find("SceneManager").GetComponent<UIManager>();
        startRound();
    }

    private void Start()
    {

        uiManager.setLevelText(currentLevel);
    }

    private void startRound()
    {
        if (currentLevel >= 20)
            dManager.levelDifficultyThree();
        else if (currentLevel >= 10 && currentLevel < 20)
            dManager.levelDifficultyTwo();
        else
            dManager.levelDifficultyOne();
    }
    /// <summary>
    /// Changes the current level text by 1
    /// </summary>
    public void setCurrentLevel()
    {
        currentLevel += 1;
        uiManager.setLevelText(currentLevel);
        if (currentLevel == 10)
            dManager.levelDifficultyTwo();
        else if (currentLevel == 20)
            dManager.levelDifficultyThree();
    }
}
