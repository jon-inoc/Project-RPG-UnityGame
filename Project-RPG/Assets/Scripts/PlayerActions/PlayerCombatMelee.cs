using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatMelee : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private PlayerController _playerController;

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
        //_playerController.isAnimating = true;
        //_animator.SetTrigger("StrongAttack");
        //_playerController.ResetAnim(1.3f);
    }

    public void OnBlock(InputValue value)
    {
        bool blocking = value.isPressed;
        _playerController.isAnimating = blocking;
        _animator.SetBool("Block", blocking);

        if (!blocking) 
        {
            _playerController.ResetAnim(0.1f);
        }
    }
}
