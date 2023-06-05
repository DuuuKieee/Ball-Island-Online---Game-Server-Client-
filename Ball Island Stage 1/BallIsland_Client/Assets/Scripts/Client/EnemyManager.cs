using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public int id;
    public float health;
    [SerializeField] public TextMeshPro hpHeader;

    public GameObject dieObj;

    public void Initialize(int _id)
    {
        id = _id;
        health = 8;
        hpHeader.text = $"{health} HP";
    }

    public void SetHealth(float _health)
    {
        health = _health;
        hpHeader.text = $"{health} HP";

        if (health <= 0f)
        {
            GameManager.enemies.Remove(id);
            GameObject dieObject;
            dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
            Destroy(dieObject, 1);
            Destroy(gameObject);
        }
    }
}