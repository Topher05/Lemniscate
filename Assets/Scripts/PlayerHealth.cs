using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    Animator animator;
	Rigidbody2D rb;
	Collider2D physicsCollider;
	public bool disableSim = false;
	public float _health;
    public float maxHealth;
    public HealthBar healthBar;
	public bool _targetable = true;
		
		public float Health {
			set{
				// if(value< _health){
				// 	animator.SetTrigger("hit");
				// }
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
			if (GameData.health <=0)
			{
					Health = maxHealth;
			} else 
			{
				Health = GameData.health; 
			}
			healthBar = GameObject.FindWithTag("HealthBar").GetComponent<HealthBar>();
			
			healthBar.SetMaxHealth(maxHealth);
			animator = GetComponent<Animator>();
			animator.SetBool("isAlive", true);
			rb = GetComponent<Rigidbody2D>();
			physicsCollider = GetComponent<Collider2D>();
		}

       


		public void OnHit(float damage, Vector2 knockback)
		{
			//Debug.Log("Slime hit for " + damage);
			Health-=damage;
            GameData.health = Health;
            healthBar.SetHealth(Health);
			rb.AddForce(knockback);
		}

		public void OnObjectDestroyed()
		{
			Destroy(gameObject);
		}

		public void OnHit(float damage)
		{
			Health-=damage;
            GameData.health = Health;
            healthBar.SetHealth(Health);
		}
}
