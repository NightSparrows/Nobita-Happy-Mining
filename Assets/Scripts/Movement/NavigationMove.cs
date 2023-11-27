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
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    public override float Speed
    {
        get
        {
            return agent.speed;
        }
        set
        {
            agent.speed = value;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
