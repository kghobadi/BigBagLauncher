using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using IconExtraction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles launching a specific game build.
/// </summary>
public class LauncherIcon : MonoBehaviour
{
    [SerializeField]
    private Button launchBtn;

    [SerializeField] private TMP_Text gameNameText;
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
    /// <param name="gameName"></param>
    /// <param name="icon"></param>
    public void SetupIcon(string path, string gameName, Icon icon = null)
    {
        launchPath = path;
        string gameNameNoEnding = gameName.Remove(gameName.Length - 4);
        gameNameText.text = gameNameNoEnding;
        //Load icon as sprite 
        if (icon != null)
        {
           SetSpriteFromIcon(icon);
        }
    }

    /// <summary>
    /// Overrides sprite icon. 
    /// </summary>
    /// <param name="sprite"></param>
    public void ManualSpriteOverride(Sprite sprite)
    {
        if (sprite != null)
        {
            launchBtn.image.sprite = sprite;
            //launchBtn.image.rectTransform.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
        }
    }

    void SetSpriteFromIcon(Icon icon)
    {
        Texture2D iconTexture = Shell.GetTextureFromIcon(icon);
        launchBtn.image.sprite = Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height),
            new Vector2(0, 0));
    }

    /// <summary>
    /// Called on User selection. 
    /// </summary>
    public void LaunchGame()
    {
        Process.Start(launchPath);
    }
}
