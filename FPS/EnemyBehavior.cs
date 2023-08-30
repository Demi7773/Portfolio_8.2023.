using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Target and Navmesh Agent")]
    public Transform playerTransform;
    NavMeshAgent agent;
    public GameManager gm;
    //public UIMngr uiMngr;

    [Header("Enemy Stats")]
    public float closeCombatDistance = 1f;
    public float dmgAmount = 10f;
    public float enemyHp = 100f;
    public int killValue = 10;

    [Header("Enemy Attack Checks")]
    bool isAttacking = false;
    bool canAttack = false;
    public float attackTimerMax = 1f;
    public float attackTimer = 1f;




    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //uiMngr = FindObjectOfType<UIMngr>();
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        isAttacking = false;
        if (!isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);


            if (distanceToPlayer > closeCombatDistance)
            {
                agent.SetDestination(playerTransform.position);
            }
            else
            {
                agent.ResetPath();
                isAttacking = true;
                canAttack = true;
            }

            attackTimer -= Time.deltaTime;
        }

        if (canAttack && attackTimer <= 0f)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        gm.LoseHP(dmgAmount);
        attackTimer = attackTimerMax;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > closeCombatDistance)
        {
            agent.SetDestination(playerTransform.position);
            canAttack = false;
        }
    }

    public void LoseHP(float dmg)
    {
        enemyHp -= dmg;

        if(enemyHp <= 0f)
        {
            gm.AddScore(killValue);
            Destroy(gameObject);
        }
    }
}
