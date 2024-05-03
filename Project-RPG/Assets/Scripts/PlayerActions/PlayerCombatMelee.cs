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
    [SerializeField] private float StrongCooldownTime = 4f;
    [SerializeField] private int Damage;
    [SerializeField] private int StrongDamage;
    [SerializeField] private AttackArea _attackArea;

    public bool isAttacking = false;
    public bool isAttackingStrong = false;
    public bool isStrongAttackCooldown = false;
    public bool isBlocking = false;
    public bool isHitByEnemy = false;

    private void Awake()
    {
        Instance = this;
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (isStrongAttackCooldown) 
        {
            StrongCooldownTime -= Time.deltaTime;
            if (StrongCooldownTime < 0) 
            {
                isStrongAttackCooldown = false; 
                StrongCooldownTime = 4f;
            }
        }
    }

    public void OnAttack(InputValue value)
    {
        isAttacking = true;
        StartCoroutine(Hit(false));
    }

    public void OnStrongAttack(InputValue value)
    {
        if (isStrongAttackCooldown) return;

        isAttackingStrong = true;
        isStrongAttackCooldown = true;
        StartCoroutine(Hit(true));
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
            attackAreaDamageable.Damage(Damage * (isStrongAtk ? StrongDamage : Damage), isStrongAtk);
        }
    }
}
