using UnityEngine;
using BehaviorTree;
using System;



public class TaskSpellAttack : Node
{
	private Animator _animator;
	private Transform enemyTransform;
	private Transform	_lastTarget;
	private DamageHandler _damageHandler;
	private GameObject bulletPrefab;
	public Vector3 bulletOfsset = new Vector3(0,0.2f,0);
	private MoveForward mF;
	private float _attackTime = 1f;
	private float _attackCounter = 0f;
	int bulletLayer;

	public TaskSpellAttack(Transform transform, GameObject gameObject)
	{
		_animator = transform.GetComponent<Animator>();
		enemyTransform = transform;
		bulletPrefab = gameObject;
		bulletLayer = transform.gameObject.layer;
	}

    public override NodeState Evaluate()
    {
		
      Transform target = (Transform)GetData("target");
		//Debug.Log(target.position);
		if(target != _lastTarget)
		{
			_damageHandler = target.GetComponent<DamageHandler>();
			_lastTarget = target;
		}

		_attackCounter +=Time.deltaTime;
		if(_attackCounter >= _attackTime)
		{
			Vector3 offset = enemyTransform.rotation * bulletOfsset;
         GameObject bulletGo = (GameObject)BehaviorTree.Tree.Instantiate(bulletPrefab, enemyTransform.position + offset, enemyTransform.rotation);
         mF = bulletGo.GetComponent<MoveForward>();
			mF.SetTargetPosition(target);
			bulletGo.layer = bulletLayer;
			//Debug.Log(enemyTransform.position);
			ClearData("target");
			_attackCounter = 0f;
		}
		
		state = NodeState.RUNNING;
	 	return state;
    }

	 
}