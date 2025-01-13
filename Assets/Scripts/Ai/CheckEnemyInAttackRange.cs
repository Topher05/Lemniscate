using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
	//private static int _enemyLayerMask = 1 << 6;

	private Transform _transform;
	private Animator _animator;
	private float attackRange;

	public CheckEnemyInAttackRange(Transform transform, float range)
	{
		_transform = transform;
		_animator = transform.GetComponent<Animator>();
		attackRange = range;
	}

	public override NodeState Evaluate()
	{
		object t = GetData("target");
		if(t == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		Transform target = (Transform)t;
		
		if(Vector2.Distance(_transform.position, target.position) <= attackRange)
		{
			// set animator or do something for attack
			_animator.SetBool("isMoving", false);
			//Debug.Log("Enemy in Range");
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}

}