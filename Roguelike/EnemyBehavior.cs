using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Plug in components")]
    public GameManager gm;
    public Transform player;
    public NavMeshAgent agent;

    [Header("Enemy Stats")]
    public float dmg;
    public float closeCombatDistance = 2f; 
    public float attackRate = 2.0f;         
    public int enemyScore = 10;

    [Header("Bools for Attack Mechanics")]
    public bool isAttacking = false;
    public bool canAttack = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < closeCombatDistance && canAttack)
        {
            agent.ResetPath();
            AttackPlayer();
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        isAttacking = true;
        gm.LoseHP(dmg);
        //Debug.Log("Enemy does " + dmg + "dmg to Player");
        StartCoroutine(AttackCD());
    }

    IEnumerator AttackCD()
    {
        canAttack = false;
        isAttacking = false;
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}
