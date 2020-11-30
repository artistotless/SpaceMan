using UnityEngine;


public class CamerFollow : MonoBehaviour
{
    private Transform hero;
    private Vector3 lastPoseHero;
    public float speed = 1;

    public Vector2 max, min;
    public Vector2 offset;

    ShootSystem ssystem;


    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").transform;
        ssystem = hero.GetComponent<ShootSystem>();
        lastPoseHero = hero.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        var destination = new Vector2(
                ssystem.isFlipped ? hero.position.x - offset.x : hero.position.x + offset.x,
                 hero.position.y + offset.y);

        destination.x = (destination.x < min.x && min.x != 0) ? min.x : destination.x;
        destination.x = (destination.x > max.x && max.x != 0) ? max.x : destination.x;
        destination.y = (destination.y < min.y && min.y != 0) ? min.y : destination.y;
        destination.y = (destination.y > max.y && max.y != 0) ? max.y : destination.y;

        transform.position = Vector2.Lerp(transform.position, destination, speed * Time.deltaTime);

    }
}
