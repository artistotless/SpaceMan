using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.IK;

public class IKPicker : MonoBehaviour
{

    CCDSolver2D solver;
    public Transform targetObject;
    [Range(0, 100)] int iterations;
    [Range(0, 100.0f)] float velocityIK;
    [Range(0, 100.0f)] float tolerance;

    // Start is called before the first frame update
    void Start()
    {
        solver.GetChain(1).target = targetObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
