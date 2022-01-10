using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[BepInPlugin("org.Crafterbot.Muck.houseEscape", "House Escape", "1.0.0")]
[HarmonyPatch(typeof(PlayerMovement))]
[HarmonyPatch("Update", MethodType.Normal)]
public class main : BaseUnityPlugin
{
    //one offs
    public bool oneOff = false;
    public bool oneoff1 = false;

    //game generated
    public bool playerInGame = false;
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            //assetLoading.DeloadAssetBundle();
            oneoff1 = false;
        }
        else
        {
            if (assetLoading.house == null && GameObject.Find("Player").active == true && active.Value == true)
            {
                Debug.Log("Player in game");
                assetLoading.LoadHouse();
            }
        }
    }
    private void OnGUI()
    {
        /*
        if(announcements.toggleAnnouncements)-
        {
            GUI.Window(432, announcements.rect, announcements.window, "Announcements - " + patcher.modTitle);
        } else
        {
            GUI.Window(432, new Rect(0 , 0, 0,0), PlaceHolder, "");
        }
        */
        if (errorLog.errorLogEnable && errorlogEnabled.Value)
        {
            GUI.Window(363, new Rect(Screen.width - 500, Screen.height - 200, 500, 200), errorLog.errorLogWindow1, "Error log - " + patcher.modTitle + " - " + patcher.modVersion);
        }
        else
        {
            GUI.Window(363763, new Rect(0, 0, 0, 0), PlaceHolder, "");
        }
    }
    private void PlaceHolder(int windowID)
    {

    }
    //bools 
    public static ConfigEntry<bool> testDot;
    public static ConfigEntry<bool> active;
    public ConfigEntry<bool> errorlogEnabled;
    //floats
    public static ConfigEntry<float> hieghtMult;
    private void Awake()
    {
        //announcements.run();
        crafterbotsFolderCheck.crafterbotsFolder.run();
        ConfigFile configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "Safe House.cfg"), true);
        active = configFile.Bind<bool>("Enabled", "This value will determine wether the mod will be enabled", true);
        errorlogEnabled = configFile.Bind<bool>("Error log enabled", "This will determine whether if the error log is enabled or disabled.", true);
        hieghtMult = configFile.Bind<float>("Height multiplier", "This number will be multiplied by the set height to determine how high the structure will be.", 1);
        testDot = configFile.Bind<bool>("Testing dot", "This will make a dot that spawns at the end of the raycast to land the house.(only for developers or error testing)", false);
    }
}
/*
 * for a later version
public class triggerMain : MonoBehaviour
{
    public static bool infHP = false;
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Object Entered the trigger");
    }
    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Object Exited the trigger");
    }
}
*/