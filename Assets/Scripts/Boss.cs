using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy_Class[] enemies;
    public float spawnOffset;
    public int damage;

    private int halfHealth;
    private Animator anim;
    //private int i = 0;
    public Slider BosshealthBar;
    private SceneTransitions sceneTransitions;

    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        BosshealthBar = FindObjectOfType<Slider>();
        BosshealthBar.maxValue = health;
        BosshealthBar.value = health;
        BosshealthBar.gameObject.SetActive(true);
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    public void TakeDamage(int damageAmount)
    {
        //i = i + 1;

        health -= damageAmount;
        BosshealthBar.value = health;

        if (health <= 0)
        {
            Destroy(gameObject);
            BosshealthBar.gameObject.SetActive(false);
            sceneTransitions.LoadScene("Win");
        }

        if(health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

            Enemy_Class randomEnemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
