using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootSystem : MonoBehaviour
{
    public float sensitivity = 2f;
    public Transform aim;
    public Transform rootBone;
    public Weapon[] weapons;
    public Weapon currentWeapon;
    
    Vector3 direction;
    Vector3 newPosition;
    IKPicker ikpicker;

    Rigidbody2D recoilArm;
    Transform aimDirectionArm;
    RelativeJoint2D recoilJoint;

    private void Start()
    {
        
        ikpicker = gameObject.GetComponent<IKPicker>();
        newPosition = Vector3.zero;
        direction = new Vector3(aim.position.x, aim.position.y, aim.position.z);
        var cameraPose = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        aim.position = new Vector3(cameraPose.x, cameraPose.y, 0.0f);
        Cursor.visible = false;
       
        recoilArm = GameObject.FindGameObjectWithTag("RecoilArm").GetComponent<Rigidbody2D>();
        aimDirectionArm = recoilArm.transform.GetChild(0);
        recoilJoint = recoilArm.GetComponent<RelativeJoint2D>();

    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 recoilDirection = Vector3.zero;
            recoilDirection.y = currentWeapon.recoil  * (aimDirectionArm.transform.rotation.normalized.z < 0 ? 1 : -1);
            recoilDirection.x = -currentWeapon.recoil;
            //Debug.Log("Pressed Fire!");
            recoilArm.AddForce(recoilDirection, ForceMode2D.Impulse);
            Debug.Log(recoilDirection);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
        direction.x = Input.GetAxis("Mouse X");
        direction.y = Input.GetAxis("Mouse Y");
        newPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        newPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

            if (aim.position.x - 2.5f < transform.position.x)
            {
                rootBone.localScale = new Vector3(1, -1, 1);
                //recoilJoint.enabled = false;
                //recoilArm.bodyType = RigidbodyType2D.Kinematic;
            // ikpicker.R_arm.velocity = 0;
        }
            else
            {

                rootBone.localScale = new Vector3(1, 1, 1);
                //recoilJoint.enabled = true;
                recoilArm.bodyType = RigidbodyType2D.Dynamic;
                ikpicker.R_arm.velocity = 0.5f;
            }
        
        aim.position = newPosition ;
        // aim.position += direction * Time.deltaTime * sensitivity;
    }
}
