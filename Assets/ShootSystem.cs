using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootSystem : MonoBehaviour
{
    public float sensitivity = 2f;
    public Transform aim;

    public Weapon[] weapons;
    public Weapon currentWeapon;
    
    Vector3 direction;
    Vector3 newPosition;
    IKPicker ikpicker;

    private void Start()
    {
        ikpicker = gameObject.GetComponent<IKPicker>();
        newPosition = Vector3.zero;
        direction = new Vector3(aim.position.x, aim.position.y, aim.position.z);
        var cameraPose = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        aim.position = new Vector3(cameraPose.x, cameraPose.y, 0.0f);
        Cursor.visible = false;
       
       


    }
    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Mouse X");
        direction.y = Input.GetAxis("Mouse Y");
        newPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        newPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        if(aim.position.x-2.0f < transform.position.x)
        {
            ikpicker.R_arm.velocity = 0;
        }
        else
        {
            ikpicker.R_arm.velocity = 0.5f;
        }
        aim.position = newPosition ;
        // aim.position += direction * Time.deltaTime * sensitivity;
    }
}
