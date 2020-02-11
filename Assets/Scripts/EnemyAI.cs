using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public GameObject gameOver;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        var distance = Vector3.Distance(agent.transform.position, player.transform.position);// no need to perform this operation twice.
        if (distance < 20)
            agent.SetDestination(player.transform.position);
        if (distance <= 2)
        {
            Debug.Log("In range");
            gameOver.SetActive(true);
             Cursor.visible = true;
        }
    }
}
