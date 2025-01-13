using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    public enum AttackDirection{
        left,right,up,down
    }
    private bool rotated = false;
    public float damage = 3;
    public AttackDirection attackDirection;
    Vector2 attackOffset;
    public Collider2D swordCollider;
    public float knockbackForce = 5000f;
    // Start is called before the first frame update
    void Start()
    {
        if(swordCollider == null){
            Debug.Log("sword collider not set");
        }
        attackOffset = transform.position;
    }
    public void Attack() {
        switch(attackDirection){
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
            case AttackDirection.up:
                AttackUp();
                break;
            case AttackDirection.down:
                AttackDown();
                break;
        }
    }

    public void AttackUp(){
        swordCollider.enabled = true;
        swordCollider.offset = new Vector2(0.02f,-0.01f);
    }
    public void AttackDown(){
        swordCollider.enabled = true;
        swordCollider.offset = new Vector2(0.02f,-0.2f);
    }
    public void AttackRight(){
        swordCollider.enabled = true;
        swordCollider.offset = new Vector2(-0.1f,-0.1f);
        transform.Rotate(0,0,90);
        rotated = true;
    }
    public void AttackLeft(){
        swordCollider.enabled = true;
        swordCollider.offset = new Vector2(-0.1f,0.1f);
        transform.Rotate(0,0,90);
        rotated = true;
    }

    public void StopAttack(){
        swordCollider.enabled = false;
        if(rotated){
            transform.Rotate(0,0,-90);
            rotated = false;
        }
        
    }
    void OnCollisionEnter2D(Collision2D other) {
        other.collider.SendMessage("OnHit", damage);
    }
    private void OnTriggerEnter2D(Collider2D other) {

        IDamageable damageableObject = other.GetComponent<IDamageable>();
        if(damageableObject != null){
            
            Vector3 parentPosition = transform.parent.position;
            
            Vector2 direction = (Vector2) (other.gameObject.transform.position-parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;  
            //Debug.Log(knockback);
            damageableObject.OnHit(damage, knockback);
        }
        

        

    }

  
}
