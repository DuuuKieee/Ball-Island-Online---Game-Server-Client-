using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ballSpriteObj, afterImageObj;
    public bool isConfuse, isDie, isDrown;
    Animator animSprite, anim;
    Rigidbody2D rb;
    float xdir, ydir;
    float dashRecoil;
    [SerializeField] float timeDashing;
    public ParticleSystem obtainEff, dushEffect;
    void Start()
    {
        animSprite = ballSpriteObj.GetComponent<Animator>();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {

            //transform.Translate(new Vector2(-collision.contacts[0].normal.x*1.2f, -collision.contacts[0].normal.y*1.2f));
            animSprite.SetBool("isDrown", true);
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
            animSprite.speed = 1;
            animSprite.Play("Drown");
            StartCoroutine(LenBo(5));
        }
    }
    IEnumerator LenBo(float sec)
    {
        yield return new WaitForSeconds(sec);
        animSprite.SetBool("isDrown", false);


    }
}

