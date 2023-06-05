using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball: MonoBehaviour
{
    [SerializeField] GameObject ballSpriteObj, afterImageObj, dieObj, blackScene;
    [SerializeField] Slider dashSlider, confuseSlider;


    [SerializeField] float moveSpeed, dashSpeed, dashStopSpeed;
    [SerializeField] float timeDashing, timeHurting;


    Animator animSprite, anim;
    Rigidbody2D rb;

    float xdir, ydir, xdirRaw, ydirRaw, xdirRawDash, ydirRawDash; //2 bien cuoi: bien luu tru gia tri cuar Input.getAxitRaw khi bat dau Dash
    public bool isPressMoveKey, isDashing, isCanConotrol, isJumping, isHurting, isCanBeHurted, isConfuse, isDrown, isDie, isGoal;
    public float dashRecoil;


    static public float HP, maxHP;
    static public float life;

    Color color;

    public ParticleSystem obtainEff, dushEffect;
    private void Awake()
    {
        maxHP = 4;
        HP = maxHP;
        life = 3;
    }
    private void Start()
    {
        animSprite = ballSpriteObj.GetComponent<Animator>();

        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        color = ballSpriteObj.GetComponent<SpriteRenderer>().material.color;

        isCanConotrol = true;
        isCanBeHurted = true;
        isConfuse = false;
        isDie = false;
    }

    private void Update()
    {
        dashRecoil +=  Time.deltaTime;
        if (HP <= 0 && isDie == false)
        {
            
            Invoke("Die", 0.5f);
            isDie = true;
        }
        xdir = Input.GetAxis("Horizontal");
        ydir = Input.GetAxis("Vertical");

        xdirRaw = Input.GetAxisRaw("Horizontal");
        ydirRaw = Input.GetAxisRaw("Vertical");

        if (xdirRaw != 0 || ydirRaw != 0) isPressMoveKey = true;
        else isPressMoveKey = false;

        
        if (isCanConotrol && isDrown == false) Walking();
        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false /*&& isCanConotrol*/ && isPressMoveKey && dashRecoil >= 3 && isDrown == false)
        {
            xdirRawDash = xdirRaw;
            ydirRawDash = ydirRaw;
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
            if (isConfuse) rb.AddForce(- new Vector2(xdir * moveSpeed, ydir * moveSpeed));
            else rb.AddForce(new Vector2(xdir * moveSpeed, ydir * moveSpeed));

            if (isHurting == true) animSprite.speed = 1;
            else
                animSprite.speed = Mathf.Clamp(Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y) / 5, 0, 1);
            animSprite.SetFloat("xSpeed", rb.velocity.x);
            animSprite.SetFloat("ySpeed", rb.velocity.y);
        }
    }

    void Dash()
    {
        isCanConotrol = false;
        if (isConfuse) rb.velocity = - new Vector2(xdirRawDash, ydirRawDash).normalized * dashSpeed;
        else rb.velocity = new Vector2(xdirRawDash, ydirRawDash).normalized * dashSpeed;
        
        animSprite.speed = 4;
        anim.SetBool("isDashing", isDashing);
    }

    IEnumerator StopDashing(float sec)
    {
        yield return new WaitForSeconds(sec);

        isCanConotrol = true;

        if (isDashing)
        {
            if (isConfuse) rb.velocity = -new Vector2(xdirRawDash, ydirRawDash).normalized * dashStopSpeed;
            else
                rb.velocity = new Vector2(xdirRawDash, ydirRawDash).normalized * dashStopSpeed;
        }
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

    IEnumerator EndHurt(float sec)
    {
        yield return new WaitForSeconds(sec);

        isHurting = false;
        isCanBeHurted = true;

        //animSprite.Play("MoveBlendTree");
        anim.SetBool("isHurting", isHurting);
    }

    public void Land()
    {
        isJumping = false;
        isCanBeHurted = true;
        Invoke("CreateDustEffect", 0.05f);
    }

    void CreateDustEffect()
    {
        Instantiate(dushEffect, transform.position, Quaternion.identity);
    }

    void Jump()
    {
        if (!isHurting)
            anim.Play("Jump");
        //Ham InAir duoc lam o trong animation
    }

    void InAir()
    {
        isJumping = true;
        isCanBeHurted = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mushroom")
        {
            if (isJumping == false)
            {
                collision.GetComponent<Mushroom>().Bounce();
                Jump();
            }
        }
    }

    Vector3 enterWaterPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
       
        if (collision.gameObject.tag == "Water")
        {
            enterWaterPos = transform.position - new Vector3(Mathf.Clamp(rb.velocity.x, 0, 1), Mathf.Clamp(rb.velocity.y, 0, 1), 0);
        }
        if (collision.tag == "Heart" && isJumping == false)
        {
            if (HP<maxHP) HP++;

            Instantiate(obtainEff, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);            
        }
        if (collision.tag == "ConfuseItem" && isJumping == false)
        {
            isConfuse = true;
            Destroy(collision.gameObject);

            confuseSlider.gameObject.SetActive(true);
            confuseSlider.value = 0;

            color.r = 0;
            ballSpriteObj.GetComponent<SpriteRenderer>().material.color = color;
        }
        if (collision.tag == "Goal")
        {
            isCanConotrol = false;
            isGoal = true;
            Destroy(gameObject, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing) Instantiate(dushEffect, transform.position, Quaternion.identity);
        if (collision.gameObject.tag == "Water" && isCanBeHurted)
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
            isCanConotrol = false;
            isDrown = true;
            anim.Play("Drown");
            animSprite.speed = 1;
            animSprite.Play("Drown");
            StartCoroutine(LenBo(1, landPos));
        }

        isDashing = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water" && isCanBeHurted)
        {
            print("hit water");
            Vector3 landPos = enterWaterPos;

            rb.velocity = Vector2.zero;
            isCanConotrol = false;
            isDrown = true;
            anim.Play("Drown");
            animSprite.speed = 1;
            animSprite.Play("Drown");
            StartCoroutine(LenBo(1, landPos));
        }
    }


    IEnumerator LenBo(float sec, Vector3 landPos)
    {
        yield return new WaitForSeconds(sec);
        transform.position = landPos;
        isDrown = false;
        isCanConotrol = true;

        //Mot cai giong ham Hurt() danh rieng cho viec roi xuong nuoc
        HP--;
        animSprite.speed = 0;
        anim.Play("Blink");
        anim.SetBool("isHurting", isHurting);
        
        animSprite.Play("MoveBlendTree");
        isHurting = true;
        isCanBeHurted = false;
        StartCoroutine(EndHurt(timeHurting));

        print("Player Hurt");
    }


    void Die()
    {
        anim.Play("Die");
        GameObject dieObject;
        dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 0.5f);
        Invoke("BlackSceneEnd", 0.5f);
    }

    void BlackSceneEnd()
    {
        blackScene.GetComponent<Animator>().Play("End");
        Destroy(gameObject, 0.6f);
    }
    private void OnDestroy()
    {

    }
}
