using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        var distance = Vector3.Distance(agent.transform.position, player.transform.position);// no need to perform this operation twice.
        if (distance < 20)
            agent.SetDestination(player.transform.position);
    }
}
