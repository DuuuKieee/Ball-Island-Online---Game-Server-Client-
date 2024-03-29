﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject ballSpriteObj, afterImageObj;
    [SerializeField] FixedJoystick joystick; // Đối tượng Joystick
    public GameObject cameraController;
    public bool isPressMoveKey, isDashing, isCanConotrol, isJumping, isCanBeHurted, isConfuse, isDie, isGoal, isDrown;
    Animator animSprite, anim;
    Rigidbody2D rb;
    float xdir, ydir;
    float dashRecoil;
    [SerializeField] float timeDashing;
    public ParticleSystem obtainEff, dushEffect;

    void Awake()
    {
        joystick = FixedJoystick.instance;
        animSprite = ballSpriteObj.GetComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        isCanConotrol = true;
        cameraController = GameObject.FindGameObjectWithTag("MainCamera");
        cameraController.GetComponent<CameraControler>().canTarget = true;
    
    }
    private void Update()
{
    if (isDrown == false && isCanConotrol)
    {
        xdir = joystick.Horizontal;
        ydir = joystick.Vertical;
        // Round the direction to the nearest 45 degrees
        xdir = Mathf.RoundToInt(xdir / 0.7071f) * 0.7071f;
        ydir = Mathf.RoundToInt(ydir / 0.7071f) * 0.7071f;
        
        animSprite.speed = 1;
        animSprite.SetFloat("xSpeed", xdir);
        animSprite.SetFloat("ySpeed", ydir);
    }
    else
    {
        xdir = 0;
        ydir = 0;
        animSprite.SetFloat("xSpeed", 0);
        animSprite.SetFloat("ySpeed", 0);
    }
    SendInputToServer();
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
            animSprite.SetBool("isDrown", true);
            anim.Play("Drown");
            animSprite.speed = 1;
            animSprite.Play("Drown");
            StartCoroutine(LenBo(5));

        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {

            animSprite.SetBool("isDrown", true);
            anim.Play("Drown");
            animSprite.speed = 1;
            animSprite.Play("Drown");
            StartCoroutine(LenBo(5));
        }
    }
    IEnumerator LenBo(float sec)
    {
        yield return new WaitForSeconds(sec);
        animSprite.SetBool("isDrown", false);
        anim.SetBool("isDrown", false);


    }



    /// <summary>Sends player input to the server.</summary>
    private void SendInputToServer()
    {

        bool[] _inputs = new bool[]
        {
            ydir > 0,
            ydir < 0,
            xdir < 0,
            xdir > 0,

        };
        ClientSend.PlayerMovement(_inputs);
    }
}