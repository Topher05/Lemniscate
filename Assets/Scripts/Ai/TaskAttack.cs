using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Unity.VisualScripting;

public class TaskAttack : Node
{
	private Animator _animator;

	private Transform	_lastTarget;
	private DamageHandler _damageHandler;

	private float _attackTime = 1f;
	private float _attackCounter = 0f;

	public TaskAttack(Transform transform)
	{
		_animator = transform.GetComponent<Animator>();
	}

    public override NodeState Evaluate()
    {
      Transform target = (Transform)GetData("target");
		if(target != _lastTarget)
		{
			
			_damageHandler = target.GetComponent<DamageHandler>();
			_lastTarget = target;
		}
		
		_attackCounter +=Time.deltaTime;
		if(_attackCounter >= _attackTime)
		{
			//either continue attacking or stop and change state

		}
		else
		{
			_attackCounter = 0f;
		}
		
		state = NodeState.RUNNING;
	 	return state;
    }

	 
}