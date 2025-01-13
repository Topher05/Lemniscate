using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    private static int _enemyLayerMask = 1 << 6;
    private Transform _transform;
    private Animator _animator;
    private float fovRange;
    public CheckEnemyInFOVRange(Transform transform, float range)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        fovRange = range;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t==null)
        {
           
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, fovRange, _enemyLayerMask);
            
            if(colliders.Length > 0)
            {
                //Debug.Log(colliders[0].tag);
                //Debug.Log("found player" + colliders[0].transform.position);
                parent.parent.SetData("target", colliders[0].transform);
                _animator.SetBool("isMoving", true);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }

}
