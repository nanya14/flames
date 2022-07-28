using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int health;
    public Image[] hearts;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Animator hurtAnim;

    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private Animator anim;
    private SceneTransitions sceneTransitions;

    //private GameObject WeaponHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        //WeaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning" , true);
        }

        else
        {
            anim.SetBool("isRunning" , false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthUI(health);
        hurtAnim.SetTrigger("hurt");
        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransitions.LoadScene("Lose");
        }
    }

    public void ChangeWeapon(Player_Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
       // Vector2 position = new Vector2(transform.position.x + 0.69f, transform.position.y + 0.52f );
        Instantiate(weaponToEquip , transform.position , transform.rotation , transform);
   
    }

    void UpdateHealthUI(int currentHealth)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount)
    {
        if(health + healAmount > 5 )
        {
            health = 5;
        }

        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }
}
