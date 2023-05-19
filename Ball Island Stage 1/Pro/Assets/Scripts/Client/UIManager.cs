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
    [SerializeField] public InputField usernameField, passwordField;
    [SerializeField] public GameObject MainCanvas; 
    [SerializeField] public Text LoginFailedNoti;
    [SerializeField] public Text ChangeMenuStatusBtn, ActionBtn;
    public bool menuAnim = false;
    private DatabaseManager databaseaccess;
    public bool isLoginPage = true;
    

    void Start()
    {
        databaseaccess = GameObject.FindGameObjectWithTag("Database").GetComponent<DatabaseManager>();

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
        //playerCamera.SetActive(false);
        MainCanvas.SetActive(false);

        Client.instance.ConnectToServer();
    }

    public void HomepageProcess()
    {
        databaseaccess.HomepageManager(usernameField.text, passwordField.text);

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
