using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatMelee : MonoBehaviour
{
    public static PlayerCombatMelee Instance;
    public Animator _animator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float DamageAfterTime;
    [SerializeField] private float StrongDamageAfterTime;
    [SerializeField] private int Damage;
    [SerializeField] private AttackArea _attackArea;

    public bool isAttacking = false;
    public bool isBlocking = false;

    private void Awake()
    {
        Instance = this;
        _animator = GetComponentInChildren<Animator>();
    }

    public void OnAttack(InputValue value)
    {
        //_animator.SetTrigger("Attack");
        isAttacking = true;
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

    private IEnumerator Hit(bool isStrongAtk) 
    {
        yield return new WaitForSeconds(isStrongAtk ? StrongDamageAfterTime : DamageAfterTime);
        foreach (var attackAreaDamageable in _attackArea.Damageables) 
        {
            attackAreaDamageable.Damage(Damage * (isStrongAtk ? 5 : 3));
        }
    }
}
