using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy_Class
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float timeBetweenSummons;
    public float stopDistance;
    public float meleeAttackSpeed;
    public Enemy_Class enemyToSummon;

    private Vector2 targetPosition;
    private Animator anim;   
    private float summonTime;    
    private float timer;    

    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX,maxX);
        float randomY = Random.Range(minY,maxY);
        targetPosition = new Vector2(randomX,randomY);
        //Debug.Log(targetPosition);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(player != null)
        {
            if((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
                //Debug.Log("SUMMONER IS RUNNING");
            }

            else
            {
                anim.SetBool("isRunning",false);

                if(Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time >= timer)
                {
                    StartCoroutine(Attack());
                    timer = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void Summon()
    {
        if(player != null)
        {
            Instantiate(enemyToSummon , transform.position , transform.rotation);
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;

        while (percent <= 1)
        {
            percent += Time.deltaTime * meleeAttackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 8;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }
    }
}
