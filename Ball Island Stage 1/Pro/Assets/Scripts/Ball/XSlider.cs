using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class XSlider : MonoBehaviour
{
    [SerializeField] Slider Slider;
    GameObject playerAnim;

    public float yPos;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("PlayerAnim");
    }

    private void Update()
    {
        Slider.transform.position = playerAnim.transform.position + new Vector3(0, yPos, 0);
    }
    void FixedUpdate()
    {
        if (Slider.value < Slider.maxValue) Slider.value++;
        else
        {
            Slider.gameObject.SetActive(false);
        }
    }
}
