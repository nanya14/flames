using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Player_Weapon weaponToEquip;
    public GameObject PickupEffect;
    public GameObject PickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(PickupEffect, transform.position, transform.rotation);
            Instantiate(PickupSound,transform.position,transform.rotation);
            collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5f);
    }
}
