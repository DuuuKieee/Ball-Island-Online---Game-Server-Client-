using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    MongoClient client = new MongoClient("mongodb+srv://DuuuKieee:899767147@loginserver.hqnkiia.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    private UIManager UI;
    void Start()
    {
        database = client.GetDatabase("UserDB");
        collection = database.GetCollection<BsonDocument>("UserCollection");
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }

    // Update is called once per frame
    public async void HomepageManager(string Username, string Password)
    {
        string user_ = Username;
        var filter = Builders<BsonDocument>.Filter.Eq("username", user_);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        if(user == null)
        {
            if (UI.isLoginPage == true)
            {
                UI.DisplayNoti("Invalid Username or Password.", false);
            }
            else
            {
                if (Password.Length >= 8 && Password.Length <= 15)
                {
                    var salt_ = DateTime.Now.ToString();
                    string pass_ = HashPassword(Password + salt_);
                    var document = BsonDocument.Parse($"{{ username: \"{user_}\", password: \"{pass_}\", salt: \"{salt_}\" }}");
                    await collection.InsertOneAsync(document);
                    UI.DisplayNoti("Congratulations, your account has been successfully created.", true);
                }
                else
                {
                    UI.DisplayNoti("Your password must be at least 8 characters.", false);
                } 
                    
            }
        }
        else
        {
            if(UI.isLoginPage == true)
            {
                var salt_ = user["salt"].AsString;
                string pass_ = HashPassword(Password + salt_);
                var savedPassword = user["password"].AsString;
                if (savedPassword.Equals(pass_, StringComparison.OrdinalIgnoreCase))
                {
                    UI.ConnectToServer();
                    Debug.Log("Da chay");
                }
                else
                {
                    UI.DisplayNoti("Invalid Username or Password.", false);
                }
            }
            else
            {
                UI.DisplayNoti("This username is already being used.", false);
            }    
        }
    }

    
    string HashPassword(string password)
    {
        SHA256 hash = SHA256.Create();

        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hashedpassword = hash.ComputeHash(passwordBytes);

        return BitConverter.ToString(hashedpassword).Replace("-", "");
    }


}
