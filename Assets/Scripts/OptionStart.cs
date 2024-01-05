using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionStart : MonoBehaviour
{
    public GameObject mainMenu;
    private Resolution[] resolutions;

    public TMPro.TMP_Dropdown resolutionDropdown;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<String> options = new List<String>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }
        
        resolutionDropdown.AddOptions(options);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
