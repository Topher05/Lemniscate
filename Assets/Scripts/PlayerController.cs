using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Cooldown cooldown;
    
    public float moveSpeed;
    public float maxSpeed;
    public float dodgeSp;
    public float idleFriction = -0.9f;
    UnityEngine.Vector2 movementInput;
    UnityEngine.Vector2 lastmoveInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;
    bool dodging = false;
    public SwordAttack swordAttack;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(movementInput != UnityEngine.Vector2.zero && canMove){
            lastmoveInput = movementInput;
            //rb.velocity = UnityEngine.Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);
            rb.AddForce(movementInput*moveSpeed*Time.deltaTime);
            if(rb.velocity.magnitude > maxSpeed && !dodging){
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }
            if(movementInput.x < 0){
                spriteRenderer.flipX = true;
            }else if (movementInput.x > 0){
                spriteRenderer.flipX = false;
            }
            animator.SetBool("IsMoving", true);
        } else {
            rb.velocity = UnityEngine.Vector2.Lerp(rb.velocity, UnityEngine.Vector2.zero, idleFriction);
            animator.SetBool("IsMoving", false);
        }


    }

        
    
    public void SwAttack(){
        //LockMovement();
        swordAttack.Attack();
    }
    public void EndSwAttack(){
        //UnlockMovement();
        swordAttack.StopAttack();
    }
    void OnFire(){
        animator.SetTrigger(swordAttack.attackDirection.ToString());
    }


    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<UnityEngine.Vector2>();
        //Debug.Log(movementInput);
        if (movementInput != UnityEngine.Vector2.zero){
            if (movementInput.x ==1){
                swordAttack.attackDirection = SwordAttack.AttackDirection.right;
            } else if (movementInput.x == -1){
                swordAttack.attackDirection = SwordAttack.AttackDirection.left;
            } else if (movementInput.y == 1){
                swordAttack.attackDirection = SwordAttack.AttackDirection.up;
            }else if (movementInput.y == -1){
                swordAttack.attackDirection = SwordAttack.AttackDirection.down;
            }
        }
    }
    
    public void LockMovement(){
        canMove = false;
    }
    public void UnlockMovement(){
        canMove = true;
    }

    void OnDodge(){
        if (cooldown.IsCoolingDown) return;
        animator.SetTrigger("dodge");
        cooldown.StartCooldown();
    }
    
    public void startDodge(){ 
        dodging = true; 
        rb.velocity += lastmoveInput * dodgeSp;
        Debug.Log(rb.velocity);
    }
    public void endDodge(){
        dodging = false;
        rb.velocity = UnityEngine.Vector2.zero;
    }
    


}
