using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;

      public float dashRecoil;
      public bool isDashing;
      [SerializeField] float timeDashing;
      public ParticleSystem obtainEff, dushEffect;

    private float moveSpeed = 500f / Constants.TICKS_PER_SEC;
    private bool[] inputs;
    public Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

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
        if (inputs[4])
        {
            isDashing = true;
        }
        dashRecoil +=  Time.deltaTime;

        Move(_inputDirection, isDashing);
    }

    /// <summary>Calculates the player's desired movement direction and moves him.</summary>
    /// <param name="_inputDirection"></param>
    private void Move(Vector2 _inputDirection, bool isDashing)
    {

        rb.AddForce(new Vector2(_inputDirection.x * moveSpeed, _inputDirection.y * moveSpeed));
       
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