using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationMove : BaseMovement
{

    private NavMeshAgent agent;

    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);
    }

    public override float baseSpeed
    {
        get
        {
            return baseValue;
        }
        set
        {
            baseValue = value;
            agent.speed = speed;
        }
    }

    public override float speedMultiplier
    {
        get
        {
            return valueMultiplier;
        }
        set
        {
            valueMultiplier = value;
            agent.speed = speed;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
