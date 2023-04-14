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
        animSprite = ballSpriteObj.GetComponent<Animator>();

        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        dashRecoil += Time.deltaTime;

        SendInputToServer();

        dashRecoil +=  Time.deltaTime;
        xdir = Input.GetAxis("Horizontal");
        ydir = Input.GetAxis("Vertical");
        if (xdir != 0 || ydir != 0) isPressMoveKey = true;
        else isPressMoveKey = false;
        if (isCanConotrol && isDrown == false) Walking();
        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false /*&& isCanConotrol*/ && isPressMoveKey && dashRecoil >= 1 && isDrown == false)
        {

            isDashing = true;

            Instantiate(dushEffect, transform.position, Quaternion.identity);

            StartCoroutine(AppearAfterImage(0.05f));
            StartCoroutine(StopDashing(timeDashing));
            dashRecoil = 0;
        }
        if (isDashing) Dash();


    }
    void Walking()
    {
     if (Time.timeScale != 0)
        {
        animSprite.speed = 1;
        animSprite.SetFloat("xSpeed", xdir);
        animSprite.SetFloat("ySpeed", ydir);
        }
    }

    void Dash()
    {
        isCanConotrol = false;
        
        animSprite.speed = 4;
        anim.SetBool("isDashing", isDashing);
    }

    IEnumerator StopDashing(float sec)
    {
        yield return new WaitForSeconds(sec);

        isCanConotrol = true;

        isDashing = false;
        animSprite.speed = 1;
        anim.SetBool("isDashing", isDashing);

    }

    IEnumerator AppearAfterImage(float sec)
    {
        for (int i = 0; i < 4; i++)
        {       
            Instantiate(afterImageObj, ballSpriteObj.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(sec);
        }
    }
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
            StartCoroutine(LenBo(1));

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
            StartCoroutine(LenBo(1));
        }
    }
    IEnumerator LenBo(float sec)
    {
        yield return new WaitForSeconds(sec);
        isDrown = false;
        isCanConotrol = true;

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
            Input.GetKeyDown(KeyCode.Space),
        };

        ClientSend.PlayerMovement(_inputs);
    }
}
