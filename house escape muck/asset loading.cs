using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
public class assetLoading : MonoBehaviour
{
    //file stuff
    public static string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string filePath = crafterbotsFolderCheck.crafterbotsFolder.FileDirect + "\\house";
    public static string fileToDownload = "http://versioncheckers.crafterbot.com/house";

    //house on ground
    public static bool houseOnGround = false;
    public static Ray rayCheckHouseOnGround = new Ray();

    //load house
    private static Vector3 playerSpawn = new Vector3();


    //asset stuff
    public static AssetBundle assetbundle = null;
    public static GameObject house = null;
    public static GameObject houseAssetbundleLocation = null;

    public static void LoadHouse()
    {
        try
        {
            Debug.Log("loading house");
            try
            {
                assetbundle = AssetBundle.LoadFromFile(crafterbotsFolderCheck.crafterbotsFolder.FileDirect + "\\house");
                houseAssetbundleLocation = assetbundle.LoadAsset<GameObject>("house");
            } catch
            {
                Debug.LogError("Loading asset(assetbundle, houseAssetbundleLocation), failed " + patcher.modTitle + " - " + patcher.discordServer);

            }
            house = GameObject.Instantiate<GameObject>(houseAssetbundleLocation);
            System.Random random = new System.Random();
            int rnd = random.Next(1, 359);

            playerSpawn = PlayerMovement.Instance.GetRb().position;
            house.transform.position = new Vector3(rnd, 1000, rnd);
            PlayerMovement.Instance.GetRb().position = house.transform.position;

            house.transform.localScale = new Vector3(7.564249f, 7.564249f, 7.564249f);
            //house.AddComponent<triggerMain>();

            BeginLanding();
        }
        catch
        {
            try
            {
                if (!File.Exists(crafterbotsFolderCheck.crafterbotsFolder.FileDirect + "\\house"))
                {
                    Debug.LogError("Asset not found ");
                    DownloadAsset();
                }
                else
                {
                    try
                    {
                        assetbundle = AssetBundle.LoadFromFile(crafterbotsFolderCheck.crafterbotsFolder.FileDirect + "\\house");
                    }
                    catch
                    {
                        errorLog.errorLogEnable = true;
                        errorLog.errorText = "Unknown error - this issue has something to do with the asset file not being found or not being able to download it: ";
                        Debug.LogError("Unknown error: " + patcher.discordServer + " -- " + patcher.modTitle);
                    }
                }
            } catch
            {
                Debug.Log("Total failure " + patcher.modTitle + " " + patcher.discordServer);
            }
        }
    }
    public static void DeloadAssetBundle()
    {
        house = null;
        houseAssetbundleLocation = null;
    }
    private static void retry()
    {
        DeloadAssetBundle();
        LoadHouse();
    }
    private static void BeginLanding()
    {
        rayCheckHouseOnGround = new Ray(house.transform.position, Vector3.down);
        Physics.Raycast(rayCheckHouseOnGround, 10000);
        RaycastHit hit;
        if (main.testDot.Value)
        {
            test = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            test.transform.localScale = new Vector3(5, 5, 5);
        }
        if (Physics.Raycast(rayCheckHouseOnGround, out hit))
        {
            if (main.testDot.Value)
            {
                test.transform.position = hit.point;
            }
            house.transform.position = hit.point + new Vector3(0, 10 * main.hieghtMult.Value, 0);
        }
        else
        {
            errorLog.errorLogEnable = true;
            errorLog.errorText = "House landing failed, raycasting error, please report it on my discord server or github page";
            Debug.LogError("House landing failed, raycasting error " + patcher.modTitle);
        }
        PlayerMovement.Instance.GetRb().position = playerSpawn;
    }
    public static GameObject test = null;
    private static WebClient web = new WebClient();
    public static void DownloadAsset()
    {
        try
        {
            Debug.Log("Beginning download");
            try
            {
                File.Delete(fileLocation);
            }
            catch
            {
                //nothing bad is gonna happen, it will be ok, do not cry you big baby ---- Baby shark, doo doo doo doo doo doo Baby shark, doo doo doo doo doo doo Baby shark, doo doo doo doo doo doo Baby shark! Mommy shark, doo doo doo doo doo doo Mommy shark, doo doo doo doo doo doo Mommy shark, doo doo doo doo doo doo Mommy shark! exp-player-logo Read More Daddy shark, doo doo doo doo doo doo Daddy shark, doo doo doo doo doo doo Daddy shark, doo doo doo doo doo doo Daddy shark! Grandma shark, doo doo doo doo doo doo Grandma shark, doo doo doo doo doo doo Grandma shark, doo doo doo doo doo doo Grandma shark! Grandpa shark, doo doo doo doo doo doo Grandpa shark, doo doo doo doo doo doo Grandpa shark, doo doo doo doo doo doo Grandpa shark! Let’s go hunt, doo doo doo doo doo doo Let’s go hunt, doo doo doo doo doo doo Let’s go hunt, doo doo doo doo doo doo Let’s go hunt! - https://www.youtube.com/watch?v=XqZsoesa55w&t=1s tell me on discord if you found this
            }
            web.DownloadFile(fileToDownload, filePath);
            Debug.Log("Download completed");
        }
        catch
        {
            errorLog.errorLogEnable = true;
            errorLog.errorText = "Unable to download asset, make sure you are connected to the internet or contact me on discord:";
            Debug.LogError("Unable to download asset, make sure you are connected to the internet or contact me on discord: " + patcher.discordServer);
        }
    }
}

