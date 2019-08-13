﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    #region CreepManagerProperties
    Player player = null;
    EnemyStats enemy = null;

    bool isFireImmune = false;
    bool isIceImmune = false;

    [SerializeField] float particleDamage = 0;
    #endregion

    #region CreepManagerFunctions
    private void OnTriggerStay(Collider collide)
    {
        string colliderTag = collide.gameObject.tag;
        ConditionManager con = collide.gameObject.GetComponentInParent<ConditionManager>();

        //Burn sound effect
        //audioSource.PlayOneShot(burn);

        if (colliderTag == "Player")
        {
            player = collide.GetComponent<Player>();
            isFireImmune = player.isFireImmune;
            isIceImmune = player.isIceImmune;
        }
        else if (colliderTag == "Enemy" || colliderTag == "BulletHell Enemy" || colliderTag == "Fire Enemy" || colliderTag == "Ice Enemy")
        {
            enemy = collide.GetComponent<EnemyStats>();
            isFireImmune = enemy.isFireImmune;
            isIceImmune = enemy.isIceImmune;
        }
        if (con != null)
        {
            switch (gameObject.tag)
            {
                case "FirePot":
                    {
                        if ((player != null || enemy != null) && !isFireImmune)
                            con.TimerAdd("fire", 3);
                        break;
                    }
                case "Explosion":
                    {
                        if ((player != null || enemy != null) && !isFireImmune)
                        {
                            con.TimerAdd("fire", 1);
                            con.Damage(particleDamage);
                        }
                        break;
                    }
                case "IcePot":
                    {
                        if ((player != null || enemy != null) && !isIceImmune)
                        {
                            con.SubtractSpeed(0.006f);
                            con.TimerAdd("thaw", 1);
                        }
                        break;
                    }
                case "IceSpell":
                    {
                        if ((player != null || enemy != null) && !isIceImmune)
                        {
                            con.SubtractSpeed(0.006f);
                            con.TimerAdd("thaw", 1);
                            con.Damage(particleDamage);
                        }
                        break;
                    }
                case "Aura":
                    {
                        if (player != null || enemy != null)
                            con.TimerAdd("aura", 2);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    public void SetParticleDamage(float _particleDamage)
    {
        particleDamage = _particleDamage;
    }

    public float GetParticleDamage()
    {
        return particleDamage;
    }
    #endregion
}
