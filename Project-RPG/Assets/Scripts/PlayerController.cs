using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anima;
    public Transform player;
    public Rigidbody _rb;

    private Vector3 _input;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _turnSpeed = 360f;

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
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look() 
    {
        if (_input != Vector3.zero) 
        {
            var relative = (transform.position + _input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
    }

    void Move() 
    {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
    }
}
