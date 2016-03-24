using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    public void executeCommand(Command c)
    {
        if (c != null)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            NavMeshHit hit;
            NavMesh.SamplePosition(c.worldPoint, out hit, 10f, 1);
            agent.SetDestination(hit.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.Stop();
        agent.enabled = false;
        NavMeshHit startPos;
        NavMesh.SamplePosition(GameObject.Find("Start").transform.position, out startPos, 10f, 1);
        transform.position = startPos.position;
        agent.enabled = true;
    }
}
