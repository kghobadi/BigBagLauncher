using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using IconExtraction;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

/// <summary>
/// This component will be the main manager for the Big Bag Launcher
/// </summary>
public class BigBagLauncher : MonoBehaviour
{
    //TODO 
    //Must
    //Data management of internal/external folders for launching Unity projects. 
    //Internal builds folder saved to Application.dataPath + this name 
    [SerializeField] private string buildsFolder = "/../Builds";
    private string[] launchFiles;//grabbed from build folders
    //TODO may not need this bool if just set up editor script 
    [Tooltip("Check this if you want to copy a lot of builds at once from a given external folder.")]
    [SerializeField] private bool useExternalFolder;
    //External builds folder paths are copied to and saved to Application.dataPath + buildsFolder
    [SerializeField] private string[] externalFolderPaths; 
    //Ability to set the folders you want to point to when launching. 
    //In order to build this and have it work, we would still need to include the game files.
    //But could have it target paths you enter for variables to copy builds from 
    
    //Simple menu for display / selecting the number of games 
    [SerializeField] private GameObject launchIconPrefab;
    //Should this be a scroll view to account for any number of files to launch from? 
    [SerializeField] private ScrollRect launcherMenu;
    //runtime list of launch icons generated for the menu 
    private List<GameObject> launchIcons = new List<GameObject>();

    [Tooltip("This array should match the expected builds for output. Will automatically use them, or try to pull from .exe icons.")]
    [SerializeField] private Sprite[] launchIconSprites;
    
    //Should
    //For images contained in the launcher
    //These can just be assets since most users of this software will just be developers
    //Folder/Directory vars for where to load any 
    //People can replace the files
    
    //Could
    //Get the icons from the Unity .exes 
    
    //Nice 
    //Might be helpful to be able to read/unpack zip folders to help compress builds 
    //Editor script that helps with External retrieval process 
    
    // Start is called before the first frame update
    void Start()
    {
        SetupLauncherMenu();
    }
    
    void SetupLauncherMenu()
    {
        //Fetch all build folders from the directory 
        string[] buildDirectories = Directory.GetDirectories(Application.dataPath + buildsFolder);
        //string[] buildZips = Directory.GetFiles(Application.dataPath + buildsFolder); may not need this
        launchFiles = new string [buildDirectories.Length];
        //can assume there is one build file per folder
        for(int i = 0; i < buildDirectories.Length; i++)
        {
            //Get all build files in a given directory
            string[] buildFiles = Directory.GetFiles(buildDirectories[i]);
            
            //loop through build files
            for (int f = 0; f < buildFiles.Length; f++)
            {
                //For windows
                //find all Unity .exe files within build folder
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
                if (buildFiles[f].EndsWith(".exe") && !buildFiles[f].Contains("CrashHandler"))
                {
                    launchFiles[i] = buildFiles[f];
                    CreateLaunchIcon(launchFiles[i], i);
                }
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                //For Mac 
                //find all Unity .app folders/files? in build folder  
                if (buildFiles[f].EndsWith(".app"))
                {
                    launchFiles[i] = buildFiles[f];
                    CreateLaunchIcon(launchFiles[i], i);
                }
#endif
            }
        }
        

        //check if use external

        //Generate icons for all filePaths targeted 

        //Eventually, will associate the filePaths with a component on the Icon prefab
        //It will handle being selected? 
        //Could handle it here too -_-

    }

    /// <summary>
    /// Creates instance of launch Icon with provided path
    /// </summary>
    /// <param name="path"></param>
    /// <param name="index"></param>
    void CreateLaunchIcon(string path, int index)
    {
        GameObject launchIcon = Instantiate(launchIconPrefab, launcherMenu.content.transform);
        //get launch icon script
        LauncherIcon launcherIcon = launchIcon.GetComponent<LauncherIcon>();
        //get filename
        string fileName = Path.GetFileName(path);
        //need to supply filename and icon as well 
        Icon icon = Shell.IconFromFilePath(path);
        //give it selection capability 
        launcherIcon.SetupIcon(path, fileName, icon);
        //Manual sprite override 
        if (launchIconSprites.Length - 1 >= index && launchIconSprites[index])
        {
            launcherIcon.ManualSpriteOverride(launchIconSprites[index]);
        }
    }

    void FindAndCopyExternalBuilds()
    {
        
    }
}
