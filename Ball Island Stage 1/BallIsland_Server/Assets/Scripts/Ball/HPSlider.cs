using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    [SerializeField] Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider.maxValue = Ball.maxHP;
        
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = Ball.HP;
    }
}
