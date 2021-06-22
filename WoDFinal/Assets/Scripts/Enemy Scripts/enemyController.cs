using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Transform player;
    private Unit unitScript;
    private Animator animator;

    private float health = 30;

    private bool chasing;
    private bool attack;
    private bool takeDamage;
    private bool death;

    private float destroyObjectTimer = 2;
    private float hurtPlayerDelay = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        unitScript = GetComponent<Unit>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();

        animator.SetBool("chasing", true);
        animator.SetBool("readyToAttack", false);
        animator.SetBool("getHit", false);
        animator.SetBool("death", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!death)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < 2)
            {
                hurtPlayerDelay -= Time.deltaTime;
                if (hurtPlayerDelay <= 0)
                {
                    player.GetComponent<playerController>().TakeDamage(15);
                    hurtPlayerDelay = 1.5f;
                }
                unitScript.isChasing = false;
                chasing = false;
                attack = true;
                // Debug.Log("attacking");
            }
            else
            {
                unitScript.isChasing = true;
                chasing = true;
                attack = false;
                //Debug.Log("chasing");

            }

        }

        healthManager();
        animatorController();

    }

    void animatorController()
    {
        if (chasing == true && attack == false && takeDamage == false && death == false)
        {
            animator.SetBool("chasing", true);
            animator.SetBool("readyToAttack", false);
            animator.SetBool("getHit", false);
            animator.SetBool("death", false);
        } else if (chasing == false && attack == true && takeDamage == false && death == false)
        {
            animator.SetBool("chasing", false);
            animator.SetBool("readyToAttack", true);
            animator.SetBool("getHit", false);
            animator.SetBool("death", false);
        }
        else if (chasing == false && attack == false && takeDamage == false && death == true)
        {
            animator.SetBool("chasing", false);
            animator.SetBool("readyToAttack", false);
            animator.SetBool("getHit", false);
            animator.SetBool("death", true);
        }

    }

    void healthManager()
    {
        if(health <= 0)
        {
            GetComponent<Collider>().enabled = false;
            unitScript.StopCoroutine("FollowPath");
            unitScript.isChasing = false;
            chasing = false;
            attack = false;
            death = true;
            destroyObjectTimer -= Time.deltaTime;
            if(destroyObjectTimer <= 0)
            {
                GameObject.Find("Enemy Spawner").GetComponent<enemySpawner>().zombieMinus();
                Destroy(this.gameObject);
            }
        }
    }
    void bulletDamage()
    {
        health -= 10;
    }
}
