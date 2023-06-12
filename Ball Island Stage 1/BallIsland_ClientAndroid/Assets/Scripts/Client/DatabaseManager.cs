using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    // Start is called before the first frame update
    MongoClient client = new MongoClient("mongodb+srv://DuuuKieee:899767147@loginserver.hqnkiia.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection, collection2;
    private UIManager UI;
    [SerializeField] public Text leaderBoard,serverBoard;
    
    void Start()
    {
        database = client.GetDatabase("UserDB");
        collection = database.GetCollection<BsonDocument>("UserCollection");
        collection2 = database.GetCollection<BsonDocument>("ServerCollection");
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
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

    // Update is called once per frame
    // public async void HomepageManager(string Username, string Password)
    // {
    //     string user_ = Username;
    //     var filter = Builders<BsonDocument>.Filter.Eq("username", user_);
    //     var user = await collection.Find(filter).FirstOrDefaultAsync();
    //     var update = Builders<BsonDocument>.Update.Set("status", "online");
    //     if(user == null)
    //     {
    //         if (UI.isLoginPage == true)
    //         {
    //             UI.DisplayNoti("Invalid Username or Password.", false);
    //         }
    //         else
    //         {
    //             if (Password.Length >= 8 && Password.Length <= 15)
    //             {
    //                 var salt_ = DateTime.Now.ToString();
    //                 string pass_ = HashPassword(Password + salt_);
    //                 var document = BsonDocument.Parse($"{{ username: \"{user_}\", password: \"{pass_}\", salt: \"{salt_}\",isOnline: \"{"false"}\",Point: {0},Death: {0} }}");
    //                 await collection.InsertOneAsync(document);
    //                 UI.DisplayNoti("Congratulations, your account has been successfully created.", true);
    //             }
    //             else
    //             {
    //                 UI.DisplayNoti("Your password must be at least 8 characters.", false);
    //             } 
                    
    //         }
    //     }
    //     else
    //     {
    //         if(UI.isLoginPage == true)
    //         {
    //             var salt_ = user["salt"].AsString;
    //             string pass_ = HashPassword(Password + salt_);
    //             var savedPassword = user["password"].AsString;
    //             var status = user["isOnline"].AsString;
    //             if (savedPassword.Equals(pass_, StringComparison.OrdinalIgnoreCase))
    //             {
    //                 if(status == "false")
    //                 {
    //                 UI.ServerMenu();

    //                 Debug.Log("Da chay");
    //                 }
    //                 else
    //                 {
    //                     UI.DisplayNoti("Your account has been logged in on another device.", false);
    //                 }
    //             }
    //             else
    //             {
    //                 UI.DisplayNoti("Invalid Username or Password.", false);
    //             }
    //         }
    //         else
    //         {
    //             UI.DisplayNoti("This username is already being used.", false);
    //         }    
    //     }
    // }

    public void GenerateLeaderBoardGlobal()
    {
        var filter = Builders<BsonDocument>.Filter.Empty;
        var sort = Builders<BsonDocument>.Sort.Descending("Point");
        var limit = 5;
        var topPlayers = collection.Find(filter).Sort(sort).Limit(limit).ToList();
        var sb = new StringBuilder();
        foreach (var player in topPlayers)
        {       
         var name = player.GetValue("username").AsString;
         var point = player.GetValue("Point").AsInt32;
         var death = player.GetValue("Death").AsInt32;
         float PD;
         if(death == 0)
         {
            PD = point;
         }
         else if (point == 0)
         {
            PD = 0;
         }
         else
         {PD = PD = (float)point / (float)death;}
         sb.AppendLine($"Username: {name}");
         sb.AppendLine($"Point: {point} Death: {death} P/D: {PD.ToString("F2")}");


        }
        leaderBoard.text = sb.ToString();

    }   
    public void ServerManager()
    {
        var filter = Builders<BsonDocument>.Filter.Eq("status", "online");
        var servers = collection2.Find(filter).ToList();
        var sb = new StringBuilder();
        foreach (var player in servers)
        {       
         var ip = player.GetValue("IP").AsString;
         var port = player.GetValue("port").AsString;
         sb.AppendLine($"IP: {ip}");
         sb.AppendLine($"Port: {port}");
        }
        serverBoard.text = sb.ToString();
    }

    string HashPassword(string password)
    {
        SHA256 hash = SHA256.Create();

        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hashedpassword = hash.ComputeHash(passwordBytes);

        return BitConverter.ToString(hashedpassword).Replace("-", "");
    }


}