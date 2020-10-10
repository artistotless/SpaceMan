using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public Vector2 centerOfMass;
    public float recoilPower;
    Transform recoilArm;
    public ShootSystem ssystem;
    Vector3 recoilDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        recoilArm = GameObject.FindGameObjectWithTag("RecoilArm").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    recoilDirection.y = recoilPower * (recoilArm.rotation.normalized.z<0?1:-1);
        //    recoilDirection.x = -recoilPower;
        //    //Debug.Log("Pressed Fire!");
        //    rigidbody2d.AddForce(recoilDirection, ForceMode2D.Impulse);
        //    centerOfMass = rigidbody2d.centerOfMass;
        //    Debug.Log(recoilDirection);
        //}
        if (Input.GetKeyDown(KeyCode.F1)) ssystem.enabled = ssystem.enabled ? false : true;

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(centerOfMass, 1);
    }
}
