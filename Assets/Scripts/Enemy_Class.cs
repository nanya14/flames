using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Class : MonoBehaviour
{
    public int health;

    [HideInInspector]
    public Transform player;

    public float speed;
    public int damage;
    public float timeBetweenAttacks;
    public int pickupChance;
    public GameObject[] pickups;
    public int healthpickupChance;
    public GameObject healthPickup;
    public GameObject EnemyDeathEffect;
    public GameObject soundObject;

    //private int i = 1;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount)
    {
        

        health -= damageAmount;
        if (health <= 0)
        {
            
                int randomNumber = Random.Range(0, 101);
                if (randomNumber < pickupChance)
                {
                    GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                    Instantiate(randomPickup, transform.position, transform.rotation);

                }

                if (randomNumber > healthpickupChance)
                {
                    Instantiate(healthPickup, transform.position, transform.rotation);
                }

                Instantiate(EnemyDeathEffect , transform.position , transform.rotation);
                Instantiate(soundObject, transform.position, transform.rotation);
                Destroy(gameObject);
                         
        }
    }
}
