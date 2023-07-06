using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goober : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    
    Rigidbody2D _rigidBody2d;
    
    void Start()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidBody2d.velocity = new Vector2(_speed, 0f);
    }
    
    void OnTriggerExit2D(Collider2D other) 
    {
        _speed = -_speed;
        transform.localScale = new Vector2(-Mathf.Sign(_rigidBody2d.velocity.x), 1f);
    }
}
