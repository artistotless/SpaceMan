using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Rifle,
    Pistol,
    GrenadeLauncher
}

public class Weapon : MonoBehaviour
{
    
    public Transform self;
    public string name;
    public float damage;
    public WeaponType weaponType;
    // ограничения и данные для позиционирования в руке.

    // Start is called before the first frame update
    void Start()
    {
        self = gameObject.transform;
    }


}
