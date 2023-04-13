using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    Queue<string> sentences = new Queue<string>();

    [SerializeField] Text nameText;
    [SerializeField] Text sentenceText;

    string str; //trich sentence tu sentences
    float sec;
    public bool isTalking = false;
    public bool isEndDialogue;
    // Start is called before the first frame update
    void Start()
    {
        //nameText.text = dialogue.name;
        //foreach (string sentence in dialogue.sentences)
        //{
        //    sentences.Enqueue(sentence);
        //}

        //sec = 0.1f;
        //isEndDialogue = false;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isTalking == false)
        {
            if (sentences.Count == 0)
            {
                isEndDialogue = true;
                print("End Dialogue");
            }
            else
            {
                DisplayDialogue();
            }
        }

        if (Input.GetKey(KeyCode.Z))
        {
            sec = 0.01f;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            sec = 0.05f;
        }
        if (isEndDialogue && Time.timeScale == 0 && isTalking == false)
        {
            Time.timeScale = 1;
        }
    }

    public void ResetStart()
    {
        nameText.text = dialogue.name;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        sec = 0.1f;
        isEndDialogue = false;
        DisplayDialogue();
    }
    public void Talk()
    {
        if (sentences.Count == 0)
        {
            print("End Dialogue");
        }
        else
        {
            if (isTalking == false)
            {
                DisplayDialogue();
            }
        }
    }    

    void DisplayDialogue()
    {
        str = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(str));
    }

    IEnumerator TypeSentence(string str)
    {
        isTalking = true;
        sentenceText.text = "";
        foreach (char letter in str.ToCharArray())
        {
            sentenceText.text += letter;
            yield return new WaitForSecondsRealtime(sec);
        }
        isTalking = false;
    }
}
