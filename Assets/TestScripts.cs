using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public Vector2 centerOfMass;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Pressed F1");
            rigidbody2d.AddForce(new Vector2(2.0f,0.0f), ForceMode2D.Impulse);
            centerOfMass = rigidbody2d.centerOfMass;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(centerOfMass, 1);
    }
}
