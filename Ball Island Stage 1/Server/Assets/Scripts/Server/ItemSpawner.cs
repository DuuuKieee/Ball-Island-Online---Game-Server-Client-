using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static Dictionary<int, ItemSpawner> spawners = new Dictionary<int, ItemSpawner>();
    private static int nextSpawnerId = 1;

    public int spawnerId;
    public bool hasItem = false;
    private DatabaseManager databaseaccess;

    private void Start()
    {
        hasItem = false;
        spawnerId = nextSpawnerId;
        nextSpawnerId++;
        spawners.Add(spawnerId, this);
        StartCoroutine(SpawnItem());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && hasItem == true)
        {
            Player _player = other.GetComponent<Player>();
            
            ItemPickedUp(_player.id, _player.username);
            
        }

        
    }

    private IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(Random.Range(20f, 35f));

        hasItem = true;
        ServerSend.ItemSpawned(spawnerId);
    }

    private void ItemPickedUp(int _byPlayer, string username)
    {
        hasItem = false;
        ServerSend.ItemPickedUp(spawnerId, _byPlayer);
        DatabaseManager.instance.PointCounter(username);
        StartCoroutine(SpawnItem());
    }
}