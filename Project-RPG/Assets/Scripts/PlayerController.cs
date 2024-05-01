using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private float _cooldownTime = 5f;
    [SerializeField] private float _nextFireTime = 360f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;

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
        GatherInput();
        Look();
    }

    void FixedUpdate() 
    {
        Move();
    }

    void GatherInput() 
    {
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
        anima.SetTrigger("Dodge");
    }
}
