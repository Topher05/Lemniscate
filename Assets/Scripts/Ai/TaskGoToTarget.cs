using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node
{
	private Transform _transform;
	private static int _enemyLayerMask = 1 << 6;
	public TaskGoToTarget(Transform transform)
	{
		_transform = transform;
	}

    public override NodeState Evaluate()
    {
		Transform target = (Transform)GetData("target");
		if (target == null){
			Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 10, _enemyLayerMask);
			target = colliders[0].transform;
		}
		if(Vector2.Distance(_transform.position, target.position) > 0.01f)
		{
			//Debug.Log("going to target");
			_transform.position = Vector2.MoveTowards(_transform.position, target.position,DocBT.speed * Time.deltaTime);
			//_transform.LookAt(target.position);
		}
       state = NodeState.RUNNING;
		 return state;
    }
}