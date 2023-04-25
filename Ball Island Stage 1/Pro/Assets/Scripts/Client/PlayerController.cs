using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject ballSpriteObj, afterImageObj;
    public bool isPressMoveKey, isDashing, isCanConotrol = true, isJumping, isHurting, isCanBeHurted, isConfuse, isDrown, isDie, isGoal;
    Animator animSprite, anim;
    Rigidbody2D rb;
    float xdir, ydir;
    float dashRecoil;
    [SerializeField] float timeDashing;
    public ParticleSystem obtainEff, dushEffect;
    void Start()
    {

        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }
    void Awake()
    {
        animSprite = ballSpriteObj.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        SendInputToServer();

        xdir = Input.GetAxis("Horizontal");
        ydir = Input.GetAxis("Vertical");
        if(isDrown == false)
        {
        animSprite.speed = 1;
        animSprite.SetFloat("xSpeed", xdir);
        animSprite.SetFloat("ySpeed", ydir);
        }


    }
   // void Walking()
  //  {
   //  if (Time.timeScale != 0)
      //  {
      //  animSprite.speed = 1;
       // animSprite.SetFloat("xSpeed", xdir);
       // animSprite.SetFloat("ySpeed", ydir);
      //  }
   // }


     Vector3 enterWaterPos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
          
            //transform.Translate(new Vector2(-collision.contacts[0].normal.x*1.2f, -collision.contacts[0].normal.y*1.2f));
            isDrown = true;
            anim.Play("Drown");
            animSprite.speed = 1;
            animSprite.Play("Drown");
            StartCoroutine(LenBo(5));

        }

        isDashing = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {   isDrown = true; 
            anim.Play("Drown");
            animSprite.speed = 1;
            animSprite.Play("Drown");
            StartCoroutine(LenBo(5));
        }
    }
    IEnumerator LenBo(float sec)
    {
        yield return new WaitForSeconds(sec);
        isDrown = false;

        //Mot cai giong ham Hurt() danh rieng cho viec roi xuong nuoc
    }



    /// <summary>Sends player input to the server.</summary>
    private void SendInputToServer()
    {

        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
        };
        ClientSend.PlayerMovement(_inputs);
    }
}
