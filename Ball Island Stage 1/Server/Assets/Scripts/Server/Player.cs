using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public int id;
    public string username;

    public bool isPressMoveKey, isDashing, isCanConotrol;
    [SerializeField] float timeDashing, dashRecoil = 4, nextDash = 0;
      public ParticleSystem obtainEff, dushEffect;

    private float moveSpeed = 700f / Constants.TICKS_PER_SEC, dashSpeed = 700f / Constants.TICKS_PER_SEC, dashStopSpeed = 500f / Constants.TICKS_PER_SEC;
    public float bounceForce;
    private bool[] inputs;
    public bool isDrown;
    public Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isCanConotrol = true;

    }


    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        

        inputs = new bool[5];
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (inputs[2])
        {
            _inputDirection.x -= 1;
        }
        if (inputs[3])
        {
            _inputDirection.x += 1;
        }

        Move(_inputDirection);
    }
     Vector3 enterWaterPos;


    /// <summary>Calculates the player's desired movement direction and moves him.</summary>
    /// <param name="_inputDirection"></param>
    private void Move(Vector2 _inputDirection)
    {

        if (isCanConotrol && isDrown == false)
        {
        rb.AddForce(new Vector2(_inputDirection.x * moveSpeed, _inputDirection.y * moveSpeed));
        }
       
        ServerSend.PlayerPosition(this);
        
    }


     private void OnTriggerEnter2D(Collider2D collision)
    {
    
       
        if (collision.gameObject.tag == "Water")
        {
            enterWaterPos = transform.position - new Vector3(Mathf.Clamp(rb.velocity.x, 0, 1), Mathf.Clamp(rb.velocity.y, 0, 1), 0);
        }
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (collision.transform.position-transform.position).normalized * bounceForce;
        }

    }
    Vector3 spawnpos = new Vector3 (0, 0, 0);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            print("hit water");
            Vector3 landPos = transform.position + new Vector3(collision.contacts[0].normal.x, collision.contacts[0].normal.y, 0);
            if (collision.contacts[0].normal.x > 0.9 || collision.contacts[0].normal.x < -0.9)
                transform.Translate(new Vector2(-collision.contacts[0].normal.x, 0));
            if (collision.contacts[0].normal.y > 0.9)
                transform.Translate(new Vector2(0, -collision.contacts[0].normal.y));
            if (collision.contacts[0].normal.y < -0.9)
                transform.Translate(new Vector2(0, -collision.contacts[0].normal.y * 0.3f));
            //transform.Translate(new Vector2(-collision.contacts[0].normal.x*1.2f, -collision.contacts[0].normal.y*1.2f));

            rb.velocity = Vector2.zero;
            StartCoroutine(Respawn(5, spawnpos));
        }

        isDashing = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            print("hit water");
            Vector3 landPos = enterWaterPos;

            rb.velocity = Vector2.zero;
            isDrown = true;

            StartCoroutine(Respawn(5, spawnpos));
        }
    }
    IEnumerator Respawn(float sec, Vector3 landPos)
    {
        yield return new WaitForSeconds(sec);
        transform.position = spawnpos;
        isDrown = false;


        //Mot cai giong ham Hurt() danh rieng cho viec roi xuong nuoc

        print("Player Hurt");
    }
    
    

   

    /// <summary>Updates the player input with newly received input.</summary>
    /// <param name="_inputs">The new key inputs.</param>
    /// <param name="_rotation">The new rotation.</param>
    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        transform.rotation = _rotation;
    }
}