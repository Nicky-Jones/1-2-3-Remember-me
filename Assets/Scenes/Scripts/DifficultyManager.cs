using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private int numOfCorrectAnswers;
    [SerializeField]
    private int levelOneDifficulty = 3;
    [SerializeField]
    private int levelTwoDifficulty = 5;
    [SerializeField]
    private int levelThreedifficulty = 3;

    private int currentLevelDifficulty;

    /// <summary>
    /// updates the game data required for level 1 difficulty
    /// </summary>
    public void levelDifficultyOne()
    {
        numOfCorrectAnswers = levelOneDifficulty;
        currentLevelDifficulty = 1;
    }

    /// <summary>
    /// updates the game data required for level 2 difficulty
    /// </summary>
    public void levelDifficultyTwo()
    {
        numOfCorrectAnswers = levelTwoDifficulty;
        currentLevelDifficulty = 2;
    }

    /// <summary>
    /// updates the game data required for level 3 difficulty
    /// </summary>
    public void levelDifficultyThree()
    {
        numOfCorrectAnswers = levelThreedifficulty;
        currentLevelDifficulty = 3;
    }

    /// <summary>
    /// returns the current difficulty of the game
    /// </summary>
    /// <returns></returns>
    public int getLevelDifficulty()
    {
        return currentLevelDifficulty;
    }

    /// <summary>
    /// returns the number of correct answers
    /// </summary>
    /// <returns></returns>
    public int getNumberOfAnswers()
    {
        return numOfCorrectAnswers;
    }

}
