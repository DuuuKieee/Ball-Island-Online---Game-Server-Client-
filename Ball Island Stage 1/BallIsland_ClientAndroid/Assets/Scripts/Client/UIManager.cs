using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject startMenu;
    [SerializeField] public InputField usernameField, passwordField,ipfield, portfield, chatField;
    [SerializeField] public GameObject MainCanvas,leaderBoard,MenuButton,serverMenu, clientObj, menuPanel, ingameMenu; 
    [SerializeField] public Text LoginFailedNoti, broadCastField;
    [SerializeField] public Text ChangeMenuStatusBtn, ActionBtn;
    public string username;
    public AudioSource mainTheme;

    private GameObject player;
    public bool isLoginPage = true;
    
    

    void Start()
    {
        //databaseaccess = GameObject.FindGameObjectWithTag("Database").GetComponent<DatabaseManager>();
        mainTheme.Play();

    }
    void FixedUpdate()
    {

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    /// <summary>Attempts to connect to the server.</summary>
    public void ConnectToServer()
    {
        serverMenu.SetActive(false);
        Destroy(mainTheme);
        clientObj.GetComponent<Client>().ip = ipfield.text;
        clientObj.GetComponent<Client>().port = int.Parse(portfield.text);
        menuPanel.SetActive(true);
        ingameMenu.SetActive(true);

        Client.instance.ConnectToServer();
    }
    public void ServerMenu()
    {
        MainCanvas.SetActive(false);
        serverMenu.SetActive(true);
        DatabaseManager.instance.ServerManager();
    }


    public void BacktoMenu()
    {
        Application.Quit();
    }

    public void HomepageProcess()
    {
        username = usernameField.text + "(Guest)";
        ServerMenu();

    }

    public void SendMessageChat()
    {
        //string _username = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerManager>().username;
        if(!string.IsNullOrEmpty(chatField.text))
        {
            ClientSend.MessageChat(username +": "+ chatField.text);
            chatField.text = "";
            
        }

    }


    public void DisplayNoti(string message, bool success)
    {
        string tColor = success ? "green" : "red";
        LoginFailedNoti.text = "<color="+tColor+">" + message + "</color>";
    }

   
}