using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    public static FixedJoystick instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

}