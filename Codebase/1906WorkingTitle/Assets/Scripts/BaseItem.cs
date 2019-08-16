﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    #region BaseProperties
    public enum Type { Weapon, Consumable, EOF};
    [SerializeField] private Type itemType = Type.EOF;
    [SerializeField] private int value = 0;
    [SerializeField] private new string name = "";
    [SerializeField] private Sprite sprite;
    #endregion

    #region BaseFunctions
    #region Gets
    public Type GetItemType()
    {
        return itemType;
    }

    public int GetValue()
    {
        return value;
    }

    public Sprite GetSprite()
    {
        if (sprite != null)
            return sprite;
        else
            return Resources.Load<Sprite>("Sprites/background");
    }

    public string GetName()
    {
        return name;
    }
    #endregion

    #region Sets
    protected void SetValue(int _value)
    {
        value = _value;
    }

    protected void SetSprite(Sprite _sprite)
    {
        sprite = _sprite;
    }

    protected void SetName(string _name)
    {
        name = _name;
    }
    #endregion
    #endregion
}
