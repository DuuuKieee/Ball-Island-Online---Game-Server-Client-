using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using System;
using System.Collections;
using System.Collections.Generic;
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
        string pass_ = Password;
        var document = BsonDocument.Parse($"{{ username: \"{user_}\", password: \"{pass_}\" }}");

        await collection.InsertOneAsync(document);
    }

    public async void Login(string Username, string Password)
    {
        string user_ = Username;
        string pass_ = Password;
        // Tạo bộ lọc để tìm người dùng có tên đăng nhập tương ứng
        var filter = Builders<BsonDocument>.Filter.Eq("username", user_);

        var user = await collection.Find(filter).FirstOrDefaultAsync();

    // Kiểm tra xem người dùng có tồn tại hay không
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

}
