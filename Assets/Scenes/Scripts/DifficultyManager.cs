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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void levelDifficultyOne()
    {
        numOfCorrectAnswers = levelOneDifficulty;
    }
    public void levelDifficultyTwo()
    {
        numOfCorrectAnswers = levelTwoDifficulty;
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
