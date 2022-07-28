using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy_Class
{
    public float stopDistance;
    public Transform shotPoint;
    public GameObject EnemyBullet;

    private float attackTime;
    private Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if (Time.time >= attackTime)
            {
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");
            }
        }
    }

    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(EnemyBullet, shotPoint.position, shotPoint.rotation);

    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 8;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }



    }

}