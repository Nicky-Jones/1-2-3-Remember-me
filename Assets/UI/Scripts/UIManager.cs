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
    [HeaderAttribute("Banner GameObjects")]
    [Tooltip("Success Banner")]
    [SerializeField]
    private GameObject levelPassBanner;
    [Tooltip("Failed Banner")]
    [SerializeField]
    private GameObject levelFailBanner;
    [Space(20)]
    [HeaderAttribute("ParticleSystems")]
    [Tooltip("Array of ParticleSystems")]
    [SerializeField]
    private GameObject[] ParticleSystems;
    [Space(20)]
    [HeaderAttribute("Completion")]
    [Tooltip("CompletionUIMenu")]
    [SerializeField]
    private GameObject completedScene;

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
    /// Pass true if you want the level pass banner to be actived, pass false if you want it to be deactived
    /// </summary>
    /// <param name="value"> if value = 1 set banner true if value = 0 set banner to false</param>
    public void setLevelPassBanner(bool value)
    {
        if (value == true)
            levelPassBanner.SetActive(true);
        else if (value == false)
        {
            //levelPassBanner.GetComponentInChildren<PlayAnimation>().resetPlayParticleSystem();
            foreach(GameObject particle in ParticleSystems)
            {
                particle.GetComponent<PlayAnimation>().resetPlayParticleSystem();
            }
            levelPassBanner.SetActive(false);
        }
        else
            Debug.Log("Error: SetLevelPassBanner not receiving correct value - UIManager Script");        
    }

    public void completedGame()
    {
        completedScene.SetActive(true);
    }

    /// <summary>
    /// pass true if you want the fail banner to be actived, pass false if you want the fail banner to be deactived
    /// </summary>
    /// <param name="value"></param>
    public void setLevelFailBanner(bool value)
    {
        if (value == true)
            levelFailBanner.SetActive(true);

        else if (value == false)
            levelFailBanner.SetActive(false);
        else
            Debug.Log("Error: SetLevelFailBanner not receiving correct value - UIManager Script");
    }
}
