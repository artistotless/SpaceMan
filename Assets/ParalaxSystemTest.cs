using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxSystemTest : MonoBehaviour
{
    private float length, startpos, ypos;
    private GameObject cam;
    public float parallaxEffect;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        ypos = transform.position.y;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        length = GetComponent<SpriteRenderer>().bounds.size.x * (transform.childCount +1 ) - offset ;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);


        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}