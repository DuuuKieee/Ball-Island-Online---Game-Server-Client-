using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject startMenu, registerMenu;
    [SerializeField] public InputField usernameField, passwordField, userregField, passwordregField;
    private DatabaseManager databaseaccess;

    private void Start()
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
        startMenu.SetActive(false);
        usernameField.interactable = false;
        passwordField.interactable = false;
        Client.instance.ConnectToServer();
    }
    public void Register()
    {
        databaseaccess.Register(userregField.text, passwordregField.text);
    }
    public void Login()
    {
        databaseaccess.Login(usernameField.text, passwordField.text);
    }

    public void RegMenu()
    {
        startMenu.SetActive(false);
        registerMenu.SetActive(true);
    }
    public void LogMenu()
    {
        startMenu.SetActive(true);
        registerMenu.SetActive(false);
    }    
    public void LoginFailed()
    {
        Debug.Log("Sai ten");
    }
    public void Registerfail()
    {
        Debug.Log("Sai ten");
    }
}
