﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartAI : MonoBehaviour
{

    [SerializeField] private float attackRate;
    //[SerializeField] private int attackMax = 120;

    private float bulletSpeed;
    int bulletDamage;

    //If this enemy's attack behavior is enabled or not.
    [SerializeField] bool attackEnabled = true;

    //What projectile the enemy shoots
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectilePos;

    [SerializeField] AudioClip fire;
    
    AudioSource source;
    
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        attackRate = 0.5f;
        bulletSpeed = 10;
        bulletDamage = 1;
        source = GetComponentInParent<AudioSource>();
        source.enabled = true;
    }


    IEnumerator attack()
    {
        attackEnabled = false;
        Quaternion temp = projectile.transform.rotation;
        temp.x = 0;
        temp.z = 0;
        GameObject clone = Instantiate(projectile, projectilePos.transform.position, temp);
        clone.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
        clone.gameObject.layer = 12;
        clone.SetActive(true);
        source.PlayOneShot(fire);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(attackRate);
        attackEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {

            if (attackEnabled)
            {
                StartCoroutine(attack());
            }
        }
    }

    #region Pause/Unpause
    public void OnPauseGame()
    {
        paused = true;
    }
    public void OnResumeGame()
    {
        paused = false;
    }
    #endregion

    public void DisableAttack()
    {
        StopAllCoroutines();
        attackEnabled = false;
    }
}
