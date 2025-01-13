using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chort : MonoBehaviour
{
public float knockbackForce = 300f;
    public float damage = 1;
    public float movementSpeed =  5;
    public float idleFriction = 0.9f;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    Animator animator;

    void Start(){
        rb = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
    }

   void FixedUpdate(){
        
        if(detectionZone.detectedObjs.Count > 0){
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            rb.AddForce(direction*movementSpeed*Time.deltaTime);
            animator.SetBool("isMoving", true);
        }
        else
        {
            rb.velocity = UnityEngine.Vector2.Lerp(rb.velocity, UnityEngine.Vector2.zero, idleFriction);
            animator.SetBool("isMoving", false);
        }
   }

    void OnCollisionEnter2D(Collision2D other) {
        Collider2D collider = other.collider;
        IDamageable damageable = other.collider.GetComponent<IDamageable>();
        
        if(damageable != null){
            
            
            Vector2 direction = (Vector2) (other.gameObject.transform.position-transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;  
            //Debug.Log(knockback);
            damageable.OnHit(damage, knockback);
        }
    }
}
