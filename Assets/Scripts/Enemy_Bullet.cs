using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    private Player playerScript;
    private Vector2 targetPosition;

    public float speed;
    public int damage;
    public GameObject EnemyDeathEffect;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,targetPosition)> 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Instantiate(EnemyDeathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" )
        {
            playerScript.TakeDamage(damage);
           
            Destroy(gameObject);
        }
    }
}
