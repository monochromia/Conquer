﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    #region Stats
    //How many points the enemy is worth to the spawner
    [SerializeField] private int enemyPoints = 1;

    //How many hits it takes to kill the enemy.
    [SerializeField] private float health = 2;

    //How much damage the enemy deals on hit.
    [SerializeField] private int damage = 2;

    //Amount of seconds between attacks.
    [SerializeField] private float attackRate = 1;

    //Speed at whichc bullets travel
    [SerializeField] private float bulletSpeed = 10;

    public bool isFireImmune;
    public bool isIceImmune;
    public bool isStunImmune;
    #endregion

    #region UnityComponents

    //Pickup the enemy will drop
    [SerializeField] GameObject pickUp = null;

    [SerializeField] GameObject childEnemy = null;

    public int children;

    //Enemy's color and renderer
    private Renderer enemyRender;
    public Color enemyColor;

    Animator anim;
    GameObject player;
    Player playerScript;
    #endregion

    public void Start()
    {
        GetComponent<AudioSource>().enabled = true;
        enemyRender = GetComponentInChildren<Renderer>();
        enemyColor = enemyRender.material.color;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (enemyRender.material.color != enemyColor)
            enemyRender.material.color = Color.Lerp(enemyRender.material.color, enemyColor, 0.1f);
    }
    
    #region Getters and Setters
    public int GetPoints()
    {
        return enemyPoints;
    }
    public float GetHealth()
    {
        return health;
    }
    public int GetDamage()
    {
        return damage;
    }
    public float GetAttackRate()
    {
        return attackRate;
    }
    public float GetMovementSpeed()
    {
        return GetComponent<NavMeshAgent>().speed;
    }
    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    public void SetAttackRate(float _attackRate)
    {
        attackRate = _attackRate;
    }
    public void SetMovementSpeed(float _speed)
    {
        GetComponent<NavMeshAgent>().speed = _speed;
    }
    public void SetBulletSpeed(float _speed)
    {
        bulletSpeed = _speed;
    }
    #endregion

    #region EnemyFunctions
    //Our enemy is damaged
    public void TakeDamage(float _damage = 1)
    {
        if(damage > 0)
        {
            BlinkOnHit();
            health -= _damage;
            if (health <= 0)
            {
                if (anim != null)
                {
                    anim.SetBool("Dead", true);
                }
                Kill();
            }
        }
    }

    //Kill function
    public void Kill()
    {
        if (pickUp != null)
        {
            Vector3 vec = GetComponent<Transform>().position;
            vec = new Vector3(vec.x, vec.y + 0.5f, vec.z);
            Instantiate(pickUp, vec, Quaternion.identity);
        }
        if(childEnemy != null && children > 0)
        {
            Split(children);
        }
        if (playerScript != null)
            playerScript.GainExperience(enemyPoints);
        Destroy(gameObject);
    }

    public void Split(int children)
    {
        for (int i = 0; i < children; i++)
        {
            Instantiate(childEnemy, transform, childEnemy.transform);
        }
    }

    //Color feedback on damage taken
    public void BlinkOnHit()
    {
        if (anim != null)
            anim.SetTrigger("On Hit");
        enemyRender.material.color = Color.red;
    }
    #endregion
}