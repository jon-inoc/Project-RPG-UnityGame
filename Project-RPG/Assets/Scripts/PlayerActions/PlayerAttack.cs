using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void OnAttack(InputValue value) 
    {
        Debug.Log("Attack");
        _animator.SetTrigger("Attack");
    }

    public void OnStrongAttack(InputValue value)
    {
        Debug.Log("Strong Attack");
        _animator.SetTrigger("StrongAttack");
    }
}
