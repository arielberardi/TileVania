using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    
    Rigidbody2D _rigidbody2D;
    Ginger _player;
    
    float _bulletSpeed = 0.0f;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();   
        _player = FindObjectOfType<Ginger>();
        _bulletSpeed = _player.transform.localScale.x * _speed;
    }

    void Update()
    {
        _rigidbody2D.velocity = new Vector2(_bulletSpeed, 0f);
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        
        Destroy(gameObject);
    }
    
    void OnCollisionEnter2D(Collider2D other) 
    {
        Destroy(gameObject);
    }
}
 