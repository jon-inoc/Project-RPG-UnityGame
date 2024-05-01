using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatMelee : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void OnAttack(InputValue value)
    {
        _animator.SetTrigger("Attack");
    }

    public void OnStrongAttack(InputValue value)
    {
        _animator.SetTrigger("StrongAttack");
    }

    public void OnBlock(InputValue value) 
    {
        _animator.SetBool("Block", value.isPressed);
    }
}
