using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IDamageable
{
    public void Damage(int damageAmount) 
    {
        Debug.Log("I Got Damaged!" + damageAmount);
    }
}
