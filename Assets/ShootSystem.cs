using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.IK;
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
    public CCDSolver2D L_arm_IK;
    internal bool isFlipped;


    private void Start()
    {

        ikpicker = gameObject.GetComponent<IKPicker>();
        newPosition = Vector3.zero;
        var cameraPose = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //var cameraPose = Camera.main.ViewportToWorldPoint(Input.mousePosition);

        aim.position = new Vector3(cameraPose.x, cameraPose.y, 0.0f);
        Cursor.visible = false;

        recoilArm = GameObject.FindGameObjectWithTag("RecoilArm").GetComponent<Rigidbody2D>();
        aimDirectionArm = recoilArm.transform.GetChild(0);
        recoilJoint = recoilArm.GetComponent<RelativeJoint2D>();

        InitWeapon();
        //Debug.Log(Mathf.Sin(30f * Mathf.Deg2Rad));
        //Mathf.Sin()



    }
    void InitWeapon()
    {
        if (currentWeapon != null)
        {
            if (currentWeapon.weaponType == WeaponType.Pistol)
            {
                L_arm_IK.enabled = false;
            }
            else
            {
                L_arm_IK.enabled = true;
            }
            aim.GetChild(0).localPosition = currentWeapon.aimPosition;
            currentWeapon.transform.localPosition = currentWeapon.weaponPosition;
            //currentWeapon.transform.localRotation = currentWeapon.weaponRotation;
            //Quaternion.
            //    Math.

        }
    }

    void SwapWeapon()
    {

    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 recoilDirection = Vector3.zero;
            recoilDirection.y = currentWeapon.recoil * (aimDirectionArm.transform.rotation.normalized.z < 0 ? 1 : -1);
            recoilDirection.x = isFlipped ? currentWeapon.recoil : -currentWeapon.recoil;
            //Debug.Log("Pressed Fire!");
            recoilArm.AddForce(recoilDirection, ForceMode2D.Impulse);
            Debug.Log(recoilDirection);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();


        newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // newPosition = Camera.main.ViewportToWorldPoint(Input.mousePosition);

        //newPosition.x = Input.mousePosition.x;
        //newPosition.y = Input.mousePosition.y;

        if (aim.position.x - 2.5f < transform.position.x)
        {
            rootBone.localScale = new Vector3(1, -1, 1);
            isFlipped = true;
            //recoilJoint.enabled = false;
            //recoilArm.bodyType = RigidbodyType2D.Kinematic;
            // ikpicker.R_arm.velocity = 0;
        }
        else
        {

            rootBone.localScale = new Vector3(1, 1, 1);
            isFlipped = false;
            //recoilJoint.enabled = true;
            recoilArm.bodyType = RigidbodyType2D.Dynamic;
            ikpicker.R_arm.velocity = 0.5f;
        }

        aim.position = newPosition * sensitivity;
        // aim.position += direction * Time.deltaTime * sensitivity;
    }
}
