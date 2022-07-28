using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public GameObject explosion;
    public GameObject soundObject;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile" , lifeTime);
        Instantiate(soundObject , transform.position , transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
        Instantiate(explosion , transform.position , Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy_Class>().TakeDamage(damage);
            DestroyProjectile();
        }
        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
