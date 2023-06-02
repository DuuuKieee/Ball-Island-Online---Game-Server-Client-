using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MongoDB.Driver.WriteConcern;


public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    MongoClient client = new MongoClient("mongodb+srv://DuuuKieee:899767147@loginserver.hqnkiia.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection, servercollection;
    void Start()
    {
        database = client.GetDatabase("UserDB");
        collection = database.GetCollection<BsonDocument>("UserCollection");
        servercollection = database.GetCollection<BsonDocument>("ServerCollection");
    }

    // Update is called once per frame
    public async void PointCounter(string Username)
    {
        string user_ = Username;
        var filter = Builders<BsonDocument>.Filter.Eq("username", user_);
        var update = Builders<BsonDocument>.Update.Inc("Point", 1);
        await collection.UpdateOneAsync(filter, update);
    }
    public async void DeadCounter(string Username)
    {
        string user_ = Username;
        var filter = Builders<BsonDocument>.Filter.Eq("username", user_);
        var update = Builders<BsonDocument>.Update.Inc("Death", 1);

        await collection.UpdateOneAsync(filter, update);

    }
    public async void SetStatus(string status, string playerName)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("username", playerName);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        if(user != null)
        {
        var update = Builders<BsonDocument>.Update.Set("isOnline", status);
        await collection.UpdateOneAsync(filter, update);
        }
        
    }

}
