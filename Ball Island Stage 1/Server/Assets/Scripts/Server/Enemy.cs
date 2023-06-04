using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int maxEnemies = 4;
    public static Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();
    private static int nextEnemyId = 1;
    private float dirX, dirY;
    public float moveSpeed;
    public Rigidbody2D rb;

    //private bool facingRight = false;
    private Vector3 localScale;

    public int id;
    public Player target;

    public Transform shootOrigin;

    public float detectionRange = 30f;
    public float health = 2;
    private DatabaseManager databaseaccess;


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

    private bool CanSeeTarget()
    {
        if (target == null)
        {
            return false;
        }
        if (Physics.Raycast(shootOrigin.position, target.transform.position - transform.position, out RaycastHit _hit, detectionRange))
        {
            if (_hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }


        return false;
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


public enum EnemyState
{
    move,
    attack
}