using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMushroom : MonoBehaviour
{

    Animator anim;

    public ParticleSystem dustEffect;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.Play("Bounce");
        Instantiate(dustEffect, collision.transform.position, Quaternion.identity);
    }
}
