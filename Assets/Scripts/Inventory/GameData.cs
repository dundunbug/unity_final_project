using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public GameLevel Level = GameLevel.Easy;
    public GameLevel LevelPassed = GameLevel.NothingYet;
    public Inventory inventory;
    public player player;
    public string Name;
    public GameSave gameSave;
    public GameSave LoadedData;
    public UsedSave usedSave;
    public int strength;
    public int speed;
    public int vitality;
   // public Owned_Item items;

    public string[] SaveFileName;
    //[SerializeField] public bool[] usedSave;
    public int FileLimit = 3;
    public int FileNum;
    public int origin_FileNum;
    public int targetNum;
    public bool InSave = false;
    public bool Restart = true;
   
    /*Game timer*/
    public int PlayTime = 0;
    public int min = 0;

    public Scene scene;
    public bool startCount = false;

    public GameObject panel_Score;
  //  public static bool ins = false;
    public static GameData ins;
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard,
        NothingYet,
    }
    void Awake()
    {

        if (ins == null)
        {

            ins = this;
            GameObject.DontDestroyOnLoad(gameObject);

        }
        else if (ins != this)
        {

            DestroyImmediate(gameObject);
        }
    }
    
  
    // Start is called before the first frame update
    private void Start()
    {
        Restart = true;
        LoadGameSaveRecord();
    }
    
    public void LoadGameSaveRecord()
    {
        SaveFileName = new string[5] { "Save1", "Save2", "Save3", "Save4", "Save5" };
        /*處理現在是save幾，從來沒存過就新增一個usedSave*/
        string str = PlayerPrefs.GetString("usedSave");
        if (str != null && str.Length > 0)
        {
            usedSave = JsonUtility.FromJson<UsedSave>(str);
            Debug.Log("Not A New Game");
        }
        else
        {
            usedSave = new UsedSave();
            usedSave.usedSave = new bool[5];
            FileNum = 0;
            Debug.Log("Create NewGame");

            string jsonUsedSave = JsonUtility.ToJson(usedSave);
            PlayerPrefs.SetString("usedSave", jsonUsedSave);
        }
    }

    public void LoadGame()
    {
        Debug.Log(targetNum);
        string str = PlayerPrefs.GetString(SaveFileName[targetNum]);
        if (str != null && str.Length > 0)
        {
            InSave = true;
            LoadedData = JsonUtility.FromJson<GameSave>(str);
            if (LoadedData != null)
            {
                Debug.Log("DataLoaded");
                origin_FileNum = targetNum;
            }
            Restart = false;
            SceneManager.LoadScene("Full_Cave");
        }
        else
        {
            Debug.Log("Nothing to Load");
        }

        
    }

    public void SaveGame(int currentFile)
    {
        /*GetData*/

        /*assign*/
        gameSave = new GameSave();
        // gameSave.checkpoint = 
        gameSave.Level = Level;
        gameSave.inventory = inventory;
        gameSave.strength = strength;
        gameSave.speed = speed;
        gameSave.vitality = vitality;
        //gameSave.playTime = 
        gameSave.PlayerName = Name;
        //gameSave.rank = 
        /*items*/
        SaveInventory();


        /*Save UsedSaveFile*/

        if (currentFile == 0 || !InSave) //"currentFile" for save opcode, 0 for new save, 1 for save; "InSave" for if ever loaded or already in a save, either should create a new Savefile 
        { 
            for(int i = 0; i < FileLimit; i++) 
            {
                if (!usedSave.usedSave[i])
                {
                    usedSave.usedSave[i] = true;
                    FileNum = i;
                    Debug.Log("SaveFile[" + i + "]");
                    if (!InSave)origin_FileNum = FileNum;//if never load and not in an existed save, make it the origin_FileNum
                    break;
                }
            }

        }

        else
        {
            FileNum = origin_FileNum;
            Debug.Log("SaveFile[CurrentBranch]");
        }
        
        InSave = true;//set bool to note already in a save, so the following option "Save" would use the origin FileNum

        /*Json*/
        string json = JsonUtility.ToJson(gameSave);
        
        PlayerPrefs.SetString(SaveFileName[FileNum], json);
        

        string jsonUsedSave = JsonUtility.ToJson(usedSave);
        PlayerPrefs.SetString("usedSave", jsonUsedSave);
    }
    
    public void DeleteGameSave()
    {
        if (!usedSave.usedSave[targetNum])
        {
            Debug.Log("Nothing To Delete");
            return;
        }
        Debug.Log("DeleteFile:" + SaveFileName[targetNum]);
        usedSave.usedSave[targetNum] = false;
        PlayerPrefs.DeleteKey(SaveFileName[targetNum]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for(int i = 0; i < FileLimit; i++)
            {
                targetNum = i;
                DeleteGameSave();
            }
            

            Debug.Log("DeleteGameSave");
        }

        /*還原成沒開過的遊戲*/
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("DeleteUsedSave");
            PlayerPrefs.DeleteKey("usedSave");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            panel_Score = GameObject.Find("Canvas (2)").transform.GetChild(1).gameObject;
            if(panel_Score.activeSelf==false)
                panel_Score.SetActive(true);
            else
                panel_Score.SetActive(false);
        }

        /*計時遊戲時間*/
        if (SceneManager.GetActiveScene().name == "Full_Cave" && !startCount)
        {
            InvokeRepeating("Timer", 1, 1);
            startCount = true;
        }

        /*每次重新回到MainMenu都要重新load一次目前有的save array*/
        if (SceneManager.GetActiveScene().name == "MainMenu" && !Restart)
        {
            usedSave=null; 
            LoadGameSaveRecord();
            Restart = true;
        }

    }

    public void Timer()
    {
        PlayTime++;
        if (PlayTime == 60)
        {
            min++;
            PlayTime = 0;
        }
    }

    public void SaveInventory()
    {
        foreach (Item item in inventory.GetList())
        {
            // Debug.Log(item.Num);
            switch (item.itemType)
            {
                default:
                case Item.ItemType.Bomb_L:
                    {
                        // items.Bomb_L = item.Num;
                        gameSave.items[0] = item.Num;
                    }
                    break;
                case Item.ItemType.Bomb_S:
                    {
                        //items.Bomb_S = item.Num;
                        gameSave.items[1] = item.Num;
                    }
                    break;
                case Item.ItemType.Bomb_Timer:
                    {
                        // items.Bomb_Timer = item.Num;
                        gameSave.items[2] = item.Num;
                    }
                    break;
                case Item.ItemType.Teddy:
                    {
                        //items.Teddy = item.Num;
                        gameSave.items[3] = item.Num;
                    }
                    break;
                case Item.ItemType.TransferGate:
                    {
                        //items.TransferGate = item.Num;
                        gameSave.items[4] = item.Num;
                    }
                    break;
                case Item.ItemType.Lego:
                    {
                        //items.Lego = item.Num;
                        gameSave.items[5] = item.Num;
                    }
                    break;
                case Item.ItemType.CardBoard:
                    {
                        //items.CardBoard = item.Num;
                        gameSave.items[6] = item.Num;
                    }
                    break;
                case Item.ItemType.Bottle:
                    {
                        //items.Bottle = item.Num;
                        gameSave.items[7] = item.Num;
                    }
                    break;
                case Item.ItemType.Carton:
                    {
                        //items.Carton = item.Num;
                        gameSave.items[8] = item.Num;
                    }
                    break;
                case Item.ItemType.Pillow:
                    {
                        //items.Pillow = item.Num;
                        gameSave.items[9] = item.Num;
                    }
                    break;
                case Item.ItemType.DroppedItem:
                    {
                        //items.DroppedItem = item.Num;
                        gameSave.items[10] = item.Num;
                    }
                    break;
            }

        }

    }
}

