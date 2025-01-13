using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DamageHandler : MonoBehaviour, IDamageable
{   
	
	Animator animator;
	Rigidbody2D rb;
	Collider2D physicsCollider;
	public HealthBar healthBar;
	public bool disableSim = false;
	public float _health;
	public float maxHealth;
	public bool _targetable = true;
		
		public float Health {
			set{
				if(value< _health){
						animator.SetTrigger("hit");
				}
				_health = value;
				
				if(_health<=0){
					animator.SetBool("isAlive", false);
					Tragetable = false;
				}
			}
			get {
				return _health;
			}
		}

		public bool Tragetable { get { return _targetable; } 
		set {
			_targetable = value;
			if(disableSim)
			{
				rb.simulated = value;//makes it so knockback would not apply after death
			}
			
			
			physicsCollider.enabled = value;
		}  }

		void Start(){
			if(healthBar != null)
			{
				healthBar.SetMaxHealth(maxHealth);
				healthBar.SetHealth(maxHealth);
			}
			animator = GetComponent<Animator>();
			animator.SetBool("isAlive", true);
			rb = GetComponent<Rigidbody2D>();
			physicsCollider = GetComponent<Collider2D>();
		}


		public void OnHit(float damage, Vector2 knockback)
		{
			//Debug.Log("Slime hit for " + damage);
			Health-=damage;
			rb.AddForce(knockback);
			if(healthBar != null)
			{
				healthBar.SetHealth(Health);
			}
		}

		public void OnObjectDestroyed()
		{
			Destroy(gameObject);
		}

		public void OnHit(float damage)
		{
			Health-=damage;
			if(healthBar != null){
				healthBar.SetHealth(Health);
			}
		}
}