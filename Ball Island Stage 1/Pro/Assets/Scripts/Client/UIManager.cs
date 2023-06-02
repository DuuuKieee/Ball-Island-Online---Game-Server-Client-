using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject startMenu;
    [SerializeField] public InputField usernameField, passwordField,ipfield, portfield;
    [SerializeField] public GameObject MainCanvas,leaderBoard,MenuButton,serverMenu, clientObj; 
    [SerializeField] public Text LoginFailedNoti;
    [SerializeField] public Text ChangeMenuStatusBtn, ActionBtn;
    public string username;
    public AudioSource mainTheme;
    public Client client;

    
    private DatabaseManager databaseaccess;
    public bool isLoginPage = true;
    

    void Start()
    {
        databaseaccess = GameObject.FindGameObjectWithTag("Database").GetComponent<DatabaseManager>();
        mainTheme.Play();
        //client = GameObject.FindGameObjectWithTag("Client").GetComponent<Client>();

    }
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            leaderBoard.SetActive(true);
            databaseaccess.GenerateLeaderBoardGlobal();
        }
        else
        {
            leaderBoard.SetActive(false);
        }
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

        Client.instance.ConnectToServer();
    }
    public void ServerMenu()
    {
        MainCanvas.SetActive(false);
        serverMenu.SetActive(true);

    }

    public void BacktoMenu()
    {
        Application.Quit();
    }

    public void HomepageProcess()
    {
        databaseaccess.HomepageManager(usernameField.text, passwordField.text);
        username = usernameField.text;

    }


    public void ChangeMenuStatus()
    {
        if (isLoginPage)
        {
            ChangeMenuStatusBtn.text = "Back to Login";
            ActionBtn.text = "Sign Up";
        }
        else
        {
            ChangeMenuStatusBtn.text = "Create new account";
            ActionBtn.text = "Sign In";
        }
        isLoginPage = !isLoginPage;
    }
  
  
   
    public void DisplayNoti(string message, bool success)
    {
        string tColor = success ? "green" : "red";
        LoginFailedNoti.text = "<color="+tColor+">" + message + "</color>";
    }

   
}