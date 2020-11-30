using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ParallaxObjectType { Default, Recycle, Fixed }
public enum ParallaxAxis { X, Y, Both }
public enum ChildSide { Left, Right }

[System.Serializable]
public struct LayerStruct
{
    public Transform transform;
    public Vector2 speed;
    public ParallaxAxis axis;
    public ParallaxObjectType type;

    internal SpriteRenderer sr;
    internal SpriteRenderer[] srChilds;


}



public class ParallaxSystem : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float rangeOfViewRecycleCamera = 0.0f;
    public LayerStruct[] layers;
    private Transform target;
    private Vector3 targetLastPosition;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetLastPosition = target.position;
        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i].type == ParallaxObjectType.Recycle)
            {
                SpriteRenderer[] childs = new SpriteRenderer[layers[i].transform.childCount];
                for (int j = 0; j < childs.Length; j++)
                {
                    childs[j] = layers[i].transform.GetChild(j).GetComponent<SpriteRenderer>();
                }
                layers[i].srChilds = childs;
            }

            layers[i].sr = layers[i].transform.GetComponent<SpriteRenderer>();
        }

    }

    void MoveLayers(LayerStruct[] layers)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            Vector3 delta = Vector3.zero;
            var layer = layers[i];
            var cameraLeftPointX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
            var cameraRightPointX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;

            delta = new Vector3(
                (layer.axis == ParallaxAxis.X || layer.axis == ParallaxAxis.Both) ? target.position.x - targetLastPosition.x : 0,
                (layer.axis == ParallaxAxis.Y || layer.axis == ParallaxAxis.Both) ? target.position.y - targetLastPosition.y : 0
                );

            delta.x *= layer.speed.x;
            delta.y *= layer.speed.y;

            switch (layer.type)
            {
                case ParallaxObjectType.Default:

                    if (layer.sr.bounds.max.x < cameraLeftPointX)
                        layer.transform.position = new Vector3(cameraRightPointX, layer.transform.position.y);

                    else if (layer.sr.bounds.min.x > cameraRightPointX)
                        layer.transform.position = new Vector3(cameraLeftPointX, layer.transform.position.y);

                    break;

                case ParallaxObjectType.Recycle:

                    var leftChild = GetChildOfRecycle(layer.srChilds, ChildSide.Left);
                    var rightChild = GetChildOfRecycle(layer.srChilds, ChildSide.Right);

                    if (cameraLeftPointX - rangeOfViewRecycleCamera < leftChild.bounds.min.x)
                    {
                        var offsetLeftChild = 6.3f;
                        Debug.Log("LeftCorner Camera < LeftGround bound");
                        rightChild.transform.position = new Vector3(leftChild.transform.position.x - rightChild.bounds.size.x + offsetLeftChild, leftChild.transform.position.y);
                    }


                    if (cameraRightPointX + rangeOfViewRecycleCamera > rightChild.bounds.max.x)
                    {
                        var offsetRightChild = -6.3f;
                        Debug.Log("RightCorner Camera > RightGround bound");
                        leftChild.transform.position = new Vector3(rightChild.transform.position.x + leftChild.bounds.size.x + offsetRightChild, rightChild.transform.position.y);
                    }

                    break;

                case ParallaxObjectType.Fixed:
                    layer.speed = Vector2.left;

                    break;
            }

            layer.transform.position = layer.transform.position - delta;

        }
    }

    public SpriteRenderer GetChildOfRecycle(SpriteRenderer[] array, ChildSide side)
    {
        float center = array[0].bounds.center.x;
        SpriteRenderer result = array[0];

        switch (side)
        {
            case ChildSide.Left:
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].bounds.center.x < center)
                    {
                        center = array[i].bounds.center.x;
                        result = array[i];
                    }
                }
                break;

            case ChildSide.Right:
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j].bounds.center.x > center)
                    {
                        center = array[j].bounds.center.x;
                        result = array[j];
                    }
                }
                break;
        }

        return result;
    }
    private void OnDrawGizmos()
    {

        Vector3 p1 = Camera.main.ScreenToWorldPoint(new Vector3(-rangeOfViewRecycleCamera, 0f, Camera.main.nearClipPlane));
        Vector3 p2 = Camera.main.ScreenToWorldPoint(new Vector3(-rangeOfViewRecycleCamera, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        Vector3 p3 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth + rangeOfViewRecycleCamera, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        Vector3 p4 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth + rangeOfViewRecycleCamera, 0f, Camera.main.nearClipPlane));
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetLastPosition != target.position)
        {
            MoveLayers(layers);
            targetLastPosition = target.position;
        }
    }


}
