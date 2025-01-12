using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles launching a specific game build.
/// </summary>
public class LauncherIcon : MonoBehaviour
{
    [SerializeField]
    private Button launchBtn;
    [SerializeField]
    private string launchPath;

    //Auto add button functionality
    private void Awake()
    {
        if (launchBtn == null)
        {
            launchBtn = GetComponent<Button>();
        }
        
        launchBtn.onClick.AddListener(LaunchGame);
    }

    private void OnDestroy()
    {
        launchBtn.onClick.RemoveListener(LaunchGame);
    }

    /// <summary>
    /// Called on set up from BBL.
    /// </summary>
    /// <param name="path"></param>
    public void SetupIcon(string path)
    {
        launchPath = path;
    }

    /// <summary>
    /// Called on User selection. 
    /// </summary>
    public void LaunchGame()
    {
        Process.Start(launchPath);
    }
}
