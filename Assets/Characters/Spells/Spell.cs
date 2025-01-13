using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float maxSpeed = 0f;
    public float knockbackForce = 300f;
    public float damage = 3;
    public UnityEngine.Vector2 direction;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetTargetPosition(Transform target){
        
        direction = target.position - transform.position;
        //Debug.Log(direction);
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 pos = transform.position;
        //Debug.Log(direction);
        if(direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        UnityEngine.Vector3 velocity = direction * maxSpeed * Time.deltaTime;
        pos += velocity;
        transform.position = pos;
    }

 
    void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        if(damageable != null){
            
            
            UnityEngine.Vector2 direction = (UnityEngine.Vector2) (other.gameObject.transform.position-transform.position).normalized;
            UnityEngine.Vector2 knockback = direction * knockbackForce;  
            //Debug.Log(knockback);
            damageable.OnHit(damage);
        }
    }

}
