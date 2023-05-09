using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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
    public async void Register(string Username, string Password)
    {

        string user_ = Username;
        var filter = Builders<BsonDocument>.Filter.Eq("username", user_);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        if (user == null)   
        { 
        var salt_ = DateTime.Now.ToString();
        string pass_ = HashPassword(Password + salt_);
        var document = BsonDocument.Parse($"{{ username: \"{user_}\", password: \"{pass_}\", salt: \"{salt_}\" }}");
        await collection.InsertOneAsync(document);
        }
        else
        {
            Debug.Log("da ton tai");
        }    
    }

    public async void Login(string Username, string Password)
    {
        string user_ = Username;
        var filter = Builders<BsonDocument>.Filter.Eq("username", user_);

        var user = await collection.Find(filter).FirstOrDefaultAsync();
        var salt_ = user["salt"].AsString;
        string pass_ = HashPassword(Password + salt_);

        if (user != null)
        {
        // Lấy mật khẩu đã lưu trữ trong cơ sở dữ liệu
        var savedPassword = user["password"].AsString;
        // So sánh mật khẩu được cung cấp bởi người dùng với mật khẩu đã lưu trữ trong cơ sở dữ liệu
        if (savedPassword.Equals(pass_, StringComparison.OrdinalIgnoreCase))

        {
                UI.ConnectToServer();
                Debug.Log("Thanh cong");
        }
        else
        {
            Debug.Log("Incorrect password");
        }
    }
    else
    {
        Debug.Log("User not found");
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
