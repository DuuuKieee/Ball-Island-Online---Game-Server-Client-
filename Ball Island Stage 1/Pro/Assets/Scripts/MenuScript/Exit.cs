using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
public class Exit: MonoBehaviour { 

    public void exitgame() {  
        //Debug.Log("exitgame");
        GameObject.FindGameObjectWithTag("BALL").GetComponent<Animator>().SetBool("isExit", true);  
        GameObject.FindGameObjectWithTag("BLACK").GetComponent<Animator>().SetBool("isStart", true);
        Invoke("exit",2);  
    }  
    void exit()
    {
        Debug.Log("exitgame");
        Application.Quit();  
    }
}   