using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public double distance;
    //public 
    private Transform hero;

    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").transform;
        
    }


    void FixedUpdate()
    {
        if (gameObject.transform.position.x >= 0)
        {
            distance = System.Math.Round(hero.position.x * 0.1, 0);
        }

    }
}
