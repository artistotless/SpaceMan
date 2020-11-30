using UnityEngine;

public class CameraCorner : MonoBehaviour
{
    public float distance;

    private void OnDrawGizmos()
    {
        ShowCorners(Camera.main.farClipPlane, Color.red);
        //ShowCorners(Camera.main.farClipPlane, Color.green);

        //Vector3 p1 = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, distance));
        //Vector3 p2 = Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelHeight, distance));
        //Vector3 p3 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, distance));
        //Vector3 p4 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0f, distance));
        //Gizmos.DrawLine(p1, p2);
        //Gizmos.DrawLine(p2, p3);
        //Gizmos.DrawLine(p3, p4);
        //Gizmos.DrawLine(p4, p1);
    }

    private void ShowCorners(float distance, Color color)
    {
        var lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        var lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        var upperLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        var upperRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        Gizmos.color = color;
        float size = 3.0f;

        Gizmos.DrawSphere(lowerLeft, size);
        //Gizmos.DrawSphere(lowerRight, size);
        //Gizmos.DrawSphere(upperLeft, size);
        //Gizmos.DrawSphere(upperRight, size);
    }
}