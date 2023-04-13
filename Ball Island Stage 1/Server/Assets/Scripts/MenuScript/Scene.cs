using UnityEngine;
using UnityEngine.SceneManagement;
 
public class Scene : MenuAnim
{
    public void ButtonMoveScene(string scene)
    {
        Invoke("MoveScene",2);
        GameObject.FindGameObjectWithTag("BALL").GetComponent<Animator>().SetBool("isStart", true);
        GameObject.FindGameObjectWithTag("BLACK").GetComponent<Animator>().SetBool("isStart", true);
    }
    void MoveScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}