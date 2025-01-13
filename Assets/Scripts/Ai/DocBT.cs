using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
 
public class DocBT : BehaviorTree.Tree
{
    public  UnityEngine.Transform[] waypoints;
    public GameObject bulletPrefab;

	public static float speed = 0.3f;

    public static float fovRange = 1f;
    public static float attackRange = 0.5f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform, attackRange),
                new TaskSpellAttack(transform, bulletPrefab)
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform, fovRange),
                new TaskGoToTarget(transform), 
            }),
            new TaskPatrol(transform, waypoints),
        });

		return root;
    }
}
