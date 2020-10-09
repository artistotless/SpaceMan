using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickEffect : MonoBehaviour
{
     Animator animator;
     SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        render = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Show(Vector3 position)
    {
        gameObject.transform.position = position;
        animator.enabled = true;
        render.enabled = true;

    }
    public void Hide()
    {
        Debug.Log("Hide smoke!");
        animator.enabled = false;
        render.enabled = false;

    }


}
