using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ginger : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    
    Vector2 _moveInput = Vector2.zero;
    Rigidbody2D _rigidBody2D;
    
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidBody2D.velocity = new Vector2(_moveInput.x * _speed, _rigidBody2D.velocity.y);    
        
        // Is Player Running
        if (Mathf.Abs(_rigidBody2D.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody2D.velocity.x), 1f);
        }
    }
        
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
}
