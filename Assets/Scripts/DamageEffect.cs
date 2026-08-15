using System;
using UnityEngine;

[Serializable]
public class DamageEffect : CardEffect
{
    public int damage;

    public override void Resolve()
    {
        Debug.Log("Dealt " + damage + " damage");
    }
}
