using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BossBT : BehaviorTree.Tree
{
    public  UnityEngine.Transform[] waypoints;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;

	public static float speed = 0.3f;

    public static float fovRange = 10f;
    public static float attackRange = 2.5f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform, attackRange),
                new RandomSequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform, attackRange),
                new TaskSpellAttack(transform, bulletPrefab),
                new TaskEnemySummon(transform, enemyPrefab),
                new TaskGoToTarget(transform)
            })
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform, fovRange),
                new TaskGoToTarget(transform), 
            })
        });

		return root;
    }
}
