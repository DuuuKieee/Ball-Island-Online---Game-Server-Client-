using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject blackScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Goal");
            blackScene.GetComponent<Animator>().Play("End");
            //Invoke("NextLevel", 1);
        }
    }

    //void NextLevel()
    //{
    //    GameController.NextLevel();
    //}
}
