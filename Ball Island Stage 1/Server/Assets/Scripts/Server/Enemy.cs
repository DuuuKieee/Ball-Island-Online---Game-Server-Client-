using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int maxEnemies = 1;
    public static Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();
    private static int nextEnemyId = 1;
    private float dirX, dirY;
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector3 localScale;

    public int id;

    public Transform shootOrigin;


    public float health = 4;


    private void Start()
    {
        id = nextEnemyId;
        nextEnemyId++;
        enemies.Add(id, this);
        dirX = Random.Range(-1f, 1f);
        dirY = Random.Range(-1f, 1f);
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        ServerSend.SpawnEnemy(this);

    }

    private void FixedUpdate()
    {

         rb.velocity = new Vector2(dirX * moveSpeed, dirY * moveSpeed);
         ServerSend.EnemyPosition(this);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        dirX = Random.Range(-1f, 1f);
        dirY = Random.Range(-1f, 1f);
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            health--;
            if(health == 0)
            {
            string lastAttacker = player.username;
            DatabaseManager.instance.PointCounter(lastAttacker);
            enemies.Remove(id);
            Destroy(gameObject);
            }
            
            ServerSend.EnemyHealth(this);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        dirX = Random.Range(-1f, 1f);
        dirY = Random.Range(-1f, 1f);
    }


}

