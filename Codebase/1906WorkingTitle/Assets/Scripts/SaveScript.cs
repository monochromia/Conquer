﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    Player player = null;
    Inventory playerInventory = null;
    int saveSlot = 0;
    GameObject[] animalCompanions = new GameObject[14];
    LinkedListNode<NonMonoWeapon> weaponNode = null;
    LinkedListNode<NonMonoConsumable> consumableNode = null;
    [SerializeField] Sprite[] sprites = new Sprite[13];
    [SerializeField] GameObject[] consumableEffects = new GameObject[7];
    ConditionManager conManager = null;

    void Start()
    {
        player = GetComponent<Player>();
        playerInventory = GetComponent<Inventory>();
        animalCompanions = GameObject.FindGameObjectsWithTag("Companion");
        conManager = GetComponent<ConditionManager>();
    }

    public void Save()
    {
        Vector3 playerPosition = player.GetPosition();
        float playerMovementSpeed = player.GetMovementSpeed();
        float maxPlayerHealth = player.GetMaxHealth();
        float playerAttackSpeed = player.GetFireRate();
        float playerExperience = player.GetExperience();
        float nextLevelExperience = player.GetNextLevelExperience();
        int playerDefense = player.GetDefense();
        int visualAttackSpeed = player.GetAttackSpeed();
        int playerAttackDamage = player.GetDamage();
        int playerLevel = player.GetLevel();
        int playerSpendingPoints = player.GetSpendingPoints();
        int playerGold = playerInventory.GetCoins();
        int playerBoxes = playerInventory.GetBoxPieces();
        int bulletCount = playerInventory.GetBulletCount();
        string animalName = "";
        if (player.GetCompanion() != null)
        {
            animalName = player.GetCompanion().gameObject.name;
            if (player.GetCompanion().gameObject.name == "Health Regen Companion")
                maxPlayerHealth -= 10;
            else if (player.GetCompanion().gameObject.name == "Attack Companion")
                playerAttackDamage -= 1;
            else if (player.GetCompanion().gameObject.name == "Defense Companion")
                playerDefense -= 1;
            else if (player.GetCompanion().gameObject.name == "Movement Speed Companion")
                playerMovementSpeed -= 3;
        }

        for (int i = 0; i < animalCompanions.Length; i++)
        {
            Vector3 animalPosition = animalCompanions[i].transform.position;
            PlayerPrefs.SetFloat($"{animalCompanions[i].name}PositionX{saveSlot}", animalPosition.x);
            PlayerPrefs.SetFloat($"{animalCompanions[i].name}PositionY{saveSlot}", animalPosition.y);
            PlayerPrefs.SetFloat($"{animalCompanions[i].name}PositionZ{saveSlot}", animalPosition.z);
        }

        int weaponNumber = 1;
        weaponNode = playerInventory.weaponList.First;
        while (weaponNode != null)
        {
            string name = weaponNode.Value.GetName();
            int damage = weaponNode.Value.GetAttackDamage();
            float speed = weaponNode.Value.GetAttackSpeed();
            PlayerPrefs.SetString($"Weapon{weaponNumber}{saveSlot}", name);
            PlayerPrefs.SetInt($"WeaponDamage{weaponNumber}{saveSlot}", damage);
            PlayerPrefs.SetFloat($"WeaponSpeed{weaponNumber}{saveSlot}", speed);
            weaponNode = weaponNode.Next;
            weaponNumber++;
        }

        int consumableNumber = 1;
        consumableNode = playerInventory.consumableList.First;
        while (consumableNode != null)
        {
            string name = consumableNode.Value.GetName();
            int intMod = consumableNode.Value.GetIntModifier();
            float floatMod = consumableNode.Value.GetFloatModifier();
            PlayerPrefs.SetString($"Consumable{consumableNumber}{saveSlot}", name);
            PlayerPrefs.SetInt($"ConsumableInt{consumableNumber}{saveSlot}", intMod);
            PlayerPrefs.SetFloat($"ConsumableFloat{consumableNumber}{saveSlot}", floatMod);
            consumableNode = consumableNode.Next;
            consumableNumber++;
        }

        PlayerPrefs.SetFloat($"PlayerX{saveSlot}", playerPosition.x);
        PlayerPrefs.SetFloat($"PlayerY{saveSlot}", playerPosition.y);
        PlayerPrefs.SetFloat($"PlayerZ{saveSlot}", playerPosition.z);
        PlayerPrefs.SetFloat($"MoveSpeed{saveSlot}", playerMovementSpeed);
        PlayerPrefs.SetFloat($"MaxHealth{saveSlot}", maxPlayerHealth);
        PlayerPrefs.SetFloat($"AttackSpeed{saveSlot}", playerAttackSpeed);
        PlayerPrefs.SetFloat($"Exp{saveSlot}", playerExperience);
        PlayerPrefs.SetFloat($"NextLevelExp{saveSlot}", nextLevelExperience);
        PlayerPrefs.SetInt($"Defense{saveSlot}", playerDefense);
        PlayerPrefs.SetInt($"VisualAttackSpeed{saveSlot}", visualAttackSpeed);
        PlayerPrefs.SetInt($"Damage{saveSlot}", playerAttackDamage);
        PlayerPrefs.SetInt($"Level{saveSlot}", playerLevel);
        PlayerPrefs.SetInt($"SpendingPoints{saveSlot}", playerSpendingPoints);
        PlayerPrefs.SetInt($"Gold{saveSlot}", playerGold);
        PlayerPrefs.SetInt($"Boxes{saveSlot}", playerBoxes);
        PlayerPrefs.SetInt($"BulletCount{saveSlot}", bulletCount);
        PlayerPrefs.SetString($"AnimalName{saveSlot}", animalName);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey($"MoveSpeed{saveSlot}"))
        {
            float playerX = PlayerPrefs.GetFloat($"PlayerX{saveSlot}");
            float playerY = PlayerPrefs.GetFloat($"PlayerY{saveSlot}");
            float playerZ = PlayerPrefs.GetFloat($"PlayerZ{saveSlot}");
            float playerMovementSpeed = PlayerPrefs.GetFloat($"MoveSpeed{saveSlot}");
            float maxPlayerHealth = PlayerPrefs.GetFloat($"MaxHealth{saveSlot}");
            float playerAttackSpeed = PlayerPrefs.GetFloat($"AttackSpeed{saveSlot}");
            float playerExperience = PlayerPrefs.GetFloat($"Exp{saveSlot}");
            float nextLevelExperience = PlayerPrefs.GetFloat($"NextLevelExp{saveSlot}");
            int playerDefense = PlayerPrefs.GetInt($"Defense{saveSlot}");
            int visualAttackSpeed = PlayerPrefs.GetInt($"VisualAttackSpeed{saveSlot}");
            int playerAttackDamage = PlayerPrefs.GetInt($"Damage{saveSlot}");
            int playerLevel = PlayerPrefs.GetInt($"Level{saveSlot}");
            int playerSpendingPoints = PlayerPrefs.GetInt($"SpendingPoints{saveSlot}");
            int playerGold = PlayerPrefs.GetInt($"Gold{saveSlot}");
            int playerBoxes = PlayerPrefs.GetInt($"Boxes{saveSlot}");
            string animalName = PlayerPrefs.GetString($"AnimalName{saveSlot}");
            int bulletCount = PlayerPrefs.GetInt($"BulletCount{saveSlot}");

            for (int i = 0; i < animalCompanions.Length; i++)
            {
                float animalX = PlayerPrefs.GetFloat($"{animalCompanions[i].name}PositionX{saveSlot}");
                float animalY = PlayerPrefs.GetFloat($"{animalCompanions[i].name}PositionY{saveSlot}");
                float animalZ = PlayerPrefs.GetFloat($"{animalCompanions[i].name}PositionZ{saveSlot}");
                animalCompanions[i].transform.position = new Vector3(animalX, animalY, animalZ);
                if (animalX < -22 && animalZ < -30 && animalX > -60 && animalZ > -47)
                    animalCompanions[i].GetComponent<Companion>().Deactivate();
            }

            int weaponNumber = 1;
            while (PlayerPrefs.HasKey($"Weapon{weaponNumber}{saveSlot}"))
            {
                string name = PlayerPrefs.GetString($"Weapon{weaponNumber}{saveSlot}");
                int damage = PlayerPrefs.GetInt($"WeaponDamage{weaponNumber}{saveSlot}");
                float speed = PlayerPrefs.GetFloat($"WeaponSpeed{weaponNumber}{saveSlot}");
                NonMonoWeapon weapon = new NonMonoWeapon();
                weapon.SetName(name);
                weapon.SetAttackDamage(damage);
                weapon.SetAttackSpeed(speed);
                weapon.SetType(NonMonoBaseItem.Type.Weapon);
                weapon.SetDesc("");
                weapon.SetValue(0);
                if (name == "Shortbow")
                    weapon.SetSprite(sprites[2]);
                else if (name == "Longbow")
                    weapon.SetSprite(sprites[3]);
                else if (name == "Crossbow")
                    weapon.SetSprite(sprites[0]);
                else if (name == "Sling")
                    weapon.SetSprite(sprites[4]);
                else if (name == "Recurve Bow")
                    weapon.SetSprite(sprites[1]);
                playerInventory.AddNonMonoWeapon(weapon);
                weaponNumber++;
            }

            int consumableNumber = 1;
            while (PlayerPrefs.HasKey($"Consumable{consumableNumber}{saveSlot}"))
            {
                string name = PlayerPrefs.GetString($"Consumable{consumableNumber}{saveSlot}");
                int intMod = PlayerPrefs.GetInt($"ConsumableInt{consumableNumber}{saveSlot}");
                float floatMod = PlayerPrefs.GetFloat($"ConsumableFloat{consumableNumber}{saveSlot}");
                NonMonoConsumable consumable = new NonMonoConsumable();
                consumable.SetName(name);
                consumable.SetIntModifier(intMod);
                consumable.SetFloatModifier(floatMod);
                consumable.SetDesc("");
                consumable.SetValue(0);
                if (name == "Health Potion" || name == "Speed Potion")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Consumable);
                    consumable.SetSprite(sprites[5]);
                }
                else if (name == "Defense Potion")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Consumable);
                    consumable.SetSprite(sprites[6]);
                }
                else if (name == "Damage Buff Potion")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Consumable);
                    consumable.SetSprite(sprites[7]);
                }
                else if (name == "Fire Potion")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Thrown);
                    consumable.SetSprite(sprites[6]);
                    consumable.SetConsumableEffect(consumableEffects[0]);
                }
                else if (name == "Ice Potion")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Thrown);
                    consumable.SetSprite(sprites[7]);
                    consumable.SetConsumableEffect(consumableEffects[1]);
                }
                else if (name == "Love Potion")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Thrown);
                    consumable.SetSprite(sprites[7]);
                    consumable.SetConsumableEffect(consumableEffects[2]);
                }
                else if (name == "Hex Shot")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Thrown);
                    consumable.SetSprite(sprites[11]);
                    consumable.SetConsumableEffect(consumableEffects[3]);
                }
                else if (name == "Fireball")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Thrown);
                    consumable.SetSprite(sprites[10]);
                    consumable.SetConsumableEffect(consumableEffects[4]);
                }
                else if (name == "Lightning Bolt")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Thrown);
                    consumable.SetSprite(sprites[12]);
                    consumable.SetConsumableEffect(consumableEffects[5]);
                }
                else if (name == "Death Aura")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Thrown);
                    consumable.SetSprite(sprites[9]);
                }
                else if (name == "Cone of Cold")
                {
                    consumable.SetType(NonMonoBaseItem.Type.Consumable);
                    consumable.SetConsumableType(NonMonoConsumable.ConsumableType.Thrown);
                    consumable.SetSprite(sprites[8]);
                    consumable.SetConsumableEffect(consumableEffects[6]);
                }
                playerInventory.AddNonMonoConsumable(consumable);
                consumableNumber++;
            }

            player.SetMovementSpeed(playerMovementSpeed);
            conManager.SetMaxSpeed(playerMovementSpeed);
            player.SetHealth(maxPlayerHealth);
            player.SetMaxHealth(maxPlayerHealth);
            player.SetFireRate(playerAttackSpeed);
            player.SetExperience(playerExperience);
            player.SetNextLevelExperience(nextLevelExperience);
            player.SetDefense(playerDefense);
            player.SetAttackSpeed(visualAttackSpeed);
            player.SetDamage(playerAttackDamage);
            player.SetLevel(playerLevel);
            player.SetSpendingPoints(playerSpendingPoints);
            playerInventory.SetCoins(playerGold);
            playerInventory.SetBoxPieces(playerBoxes);
            playerInventory.SetBulletCount(bulletCount);
            GetComponent<CharacterController>().enabled = false;
            if (player.GetLives() <= 0)
            {
                conManager.TimerSet("fire", 0);
                conManager.TimerSet("thaw", 0);
                conManager.TimerSet("stun", 0);
                player.SetPosition(player.GetPosition());
                StartCoroutine(player.Invincible());
            }
            else
            {
                player.SetPosition(new Vector3(playerX, playerY, playerZ));
            }
            GetComponent<CharacterController>().enabled = true;
            player.SetLives(5);

            if (GameObject.Find(animalName))
            {
                if (player.GetCompanion() != null)
                    player.GetCompanion().Deactivate();
                GameObject.Find(animalName).GetComponent<Companion>().Activate();
            }

        }
        else
        {
            if (player.GetLives() <= 0)
            {
                conManager.TimerSet("fire", 0);
                conManager.TimerSet("thaw", 0);
                conManager.TimerSet("stun", 0);
                player.SetPosition(player.GetPosition());
                StartCoroutine(player.Invincible());
            }
            else
                player.SetPosition(new Vector3(-1.4f, -9.9f, -55.6f));
            player.SetLives(5);
        }
    }

    public int GetSaveSlot()
    {
        return saveSlot;
    }

    public void SetSaveSlot(int _newSave)
    {
        saveSlot = _newSave;
    }
}
