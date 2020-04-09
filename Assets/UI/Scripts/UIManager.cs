using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText;
    private DifficultyManager dManager;
    [SerializeField]
    private GameObject levelPassBanner;
    [SerializeField]
    private GameObject levelFailBanner;

    private void Awake()
    {
        levelText = GameObject.Find("LevelNumberText").GetComponent<TextMeshProUGUI>();
    }
    /// <summary>
    /// Sets the current level to string and updates the LevelText onscreen
    /// </summary>
    /// <param name="level"></param>
    public void setLevelText(int level)
    {
        levelText.text = level.ToString();
    }
    /// <summary>
    /// if value = 1 set banner true if value = 0 set banner to false
    /// </summary>
    /// <param name="value"> if value = 1 set banner true if value = 0 set banner to false</param>
    public void setLevelPassBanner(int value)
    {
        if (value == 1)
            levelPassBanner.SetActive(true);
        else if (value == 0)
        {
            levelPassBanner.GetComponentInChildren<PlayAnimation>().resetPlayParticleSystem();
            levelPassBanner.SetActive(false);
        }
        else
            Debug.Log("Error: SetLevelPassBanner not receiving correct value - UIManager Script");        
    }

    /// <summary>
    /// if value = 1 set banner true if value = 0 set banner to false
    /// </summary>
    /// <param name="value"></param>
    public void setLevelFailBanner(int value)
    {
        if (value == 1)
            levelFailBanner.SetActive(true);

        else if (value == 0)
            levelFailBanner.SetActive(false);
        else
            Debug.Log("Error: SetLevelFailBanner not receiving correct value - UIManager Script");
    }
}
