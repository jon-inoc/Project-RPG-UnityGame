using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IDamageable
{
    private Animator animator;

    [SerializeField] private float _hitPoints = 10f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Damage(int damageAmount, bool isStrongAttack) 
    {
        Debug.Log("I Got Damaged! " + damageAmount);
        if (isStrongAttack)
        {
            animator.Play("StrongAttackHit");
        }
        else 
        {
            animator.Play("AttackHit");
        }

        _hitPoints = _hitPoints - damageAmount;
        Debug.Log("HitPoints left: " + _hitPoints);

        if (_hitPoints < 0) 
        {
            animator.Play("Death");
        }
    }

    private void PlayDeath() 
    {
    
    }

}
