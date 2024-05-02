using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator anima;
    public Transform player;
    public Rigidbody _rb;
    ProjectRPGGame _inputs;
    private Vector2 _move;


    private Vector3 _moveInput;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _turnSpeed = 360f;

    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    public bool isAnimating = false;

    public bool isBlocking = false;

    private void Awake()
    {
        anima = GetComponentInChildren<Animator>();
        _inputs = new ProjectRPGGame();
        _inputs.Player.Move.performed += context => _move = context.ReadValue<Vector2>();
        _inputs.Player.Move.canceled += context => _move = Vector2.zero;
    }
    void OnEnable() => _inputs.Enable();

    void OnDisable() => _inputs.Disable();
    // Update is called once per frame
    void Update()
    {
        if (isBlocking)
        {
            return;
        }
        GatherInput();
        Look();
    }

    void FixedUpdate()
    {

        Move();
    }

    void GatherInput() 
    {
        if (isAnimating || isBlocking)
        {
            return;
        }
        _moveInput = new Vector3(_move.x, 0, _move.y);
    }

    void Look() 
    {
        if (_moveInput != Vector3.zero) 
        {
            var relative = (transform.position + _moveInput.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
    }

    void Move() 
    {
        if (isAnimating || isBlocking)
        {
            anima.SetFloat("Speed", 0f);
            return;
        }
        _rb.MovePosition(transform.position + (transform.forward * _moveInput.magnitude) * _speed * Time.deltaTime);
        if (_moveInput.magnitude > 0)
        {
            anima.SetFloat("Speed", 1f);
        }
        else 
        {
            anima.SetFloat("Speed", 0f);
        }
    }

    public void OnDodge()
    {
        isAnimating = true;
        anima.SetTrigger("Dodge");

        _rb.AddForce(transform.forward * 450f);
        ResetAnim(0.2f);
    }


    public void ResetAnim(float duration) 
    {
        StartCoroutine(ResetAnimCoroutine(duration));
    }

    private IEnumerator ResetAnimCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        _speed = 5f;
        isAnimating = false;
    }

    public void OnAttack() 
    {
        isAnimating = true;
        _speed = 1.5f;
        ResetAnim(0.2f);
    }

    public void OnBlock(InputValue value) 
    {
        isBlocking = value.isPressed;
    }
}
