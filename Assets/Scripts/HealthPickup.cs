using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    Player playerScript;
    public int healAmount;
    public GameObject PickupEffect;
    public GameObject PickupSound;


    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(PickupEffect, transform.position, transform.rotation);
            Instantiate(PickupSound, transform.position, transform.rotation);
            playerScript.Heal(healAmount);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5f);
    }
}
