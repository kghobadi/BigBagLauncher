using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component will be the main manager for the Big Bag Launcher
/// </summary>
public class BigBagLauncher : MonoBehaviour
{
    //TODO 
    //Must
    //Data management of internal/external folders for launching Unity projects. 
    //Internal builds folder saved to Application.dataPath + this name 
    [SerializeField] private string buildsFolder; 
    //TODO may not need this bool if just set up editor script 
    [Tooltip("Check this if you want to copy a lot of builds at once from a given external folder.")]
    [SerializeField] private bool useExternalFolder;
    //External builds folder paths are copied to and saved to Application.dataPath + buildsFolder
    [SerializeField] private string[] externalFolderPaths; 
    //Ability to set the folders you want to point to when launching. 
    //In order to build this and have it work, we would still need to include the game files.
    //But could have it target paths you enter for variables to copy builds from 
    
    //Simple menu for display / selecting the number of games 
    [SerializeField] private GameObject selectableIconPrefab;
    //Should this be a scroll view to account for any number of files to launch from? 
    [SerializeField] private ScrollRect launcherMenu;
    //runtime list of launch icons generated for the menu 
    private List<GameObject> launchIcons = new List<GameObject>();
    
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
        
    }


    void SetupLauncherMenu()
    {
        //For windows
        //find all Unity .exe files within build folder
        
        //For Mac 
        //find all Unity .app folders/files? in build folder  
        
        //check if use external
        
        //Generate icons for all filePaths targeted 
        
        //Eventually, will associate the filePaths with a component on the Icon prefab
        //It will handle being selected? 
        //Could handle it here too -_-
        
    }

    void FindAndCopyExternalBuilds()
    {
        
    }
}
