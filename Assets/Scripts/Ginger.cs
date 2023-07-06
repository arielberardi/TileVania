using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ginger : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _jumpSpeed = 2.5f;
    [SerializeField] float _climbSpeed = 5.0f;
    [SerializeField] bool _isAlive = true;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _gun;
    
    Vector2 _moveInput = Vector2.zero;
    Rigidbody2D _rigidBody2d;
    Animator _animator;
    CapsuleCollider2D _capsuleCollider2d;
    BoxCollider2D _boxCollider2d;
    
    
    float _gravityScaleStart = 1.0f;

    void Start()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        _boxCollider2d = GetComponent<BoxCollider2D>();
        
        _gravityScaleStart = _rigidBody2d.gravityScale;
    }

    void Update()
    {
        _rigidBody2d.velocity = new Vector2(_moveInput.x * _speed, _rigidBody2d.velocity.y);    
        
        // Is Player Running
        if (Mathf.Abs(_rigidBody2d.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody2d.velocity.x), 1f);
            _animator.SetBool("isRunning", true);
        }
        else 
        {
            _animator.SetBool("isRunning", false);    
        }
        
        // Is Player Climbing
        if (_boxCollider2d.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            _rigidBody2d.velocity = new Vector2(_rigidBody2d.velocity.x, _moveInput.y * _climbSpeed);    
            _rigidBody2d.gravityScale = 0;  
            
            if (_rigidBody2d.velocity.y > Mathf.Epsilon)
            {
                _animator.SetBool("isClimbing", true);
            }
            else 
            {
                _animator.SetBool("isClimbing", false);
            }
        }
        else 
        {
            if (_rigidBody2d.gravityScale != _gravityScaleStart)
            {
                _rigidBody2d.gravityScale = _gravityScaleStart;
            }
        }
        
        if (_isAlive && _capsuleCollider2d.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            _isAlive = false;
            _rigidBody2d.velocity = new Vector2(-1.0f, 20.0f);
            _animator.SetTrigger("Dying");
        }
    }
        
    void OnMove(InputValue value)
    {
        if (_isAlive == false)
        {
            return;
        }
        
        _moveInput = value.Get<Vector2>();
    }
        
    void OnJump(InputValue value)
    {
        if (_isAlive == false)
        {
            return;
        }
        
        if(value.isPressed && _boxCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _rigidBody2d.velocity += new Vector2(0f, _jumpSpeed);
        }
    }
    
    void OnFire(InputValue value)
    {
        if (_isAlive == false)
        {
            return;
        }
        
        Instantiate(_bulletPrefab, _gun.position, Quaternion.identity);
    }
}
