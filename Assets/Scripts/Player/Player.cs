using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{

    private GameObject checkpoint;
    public GameObject positionMarker;
    private GameObject spawnedMarker;

    public void Start()
    {
        checkpoint = GameObject.Find("Start");
        spawnedMarker = Instantiate(positionMarker);
        spawnedMarker.GetComponent<PositionMarker>().ToggleMarker(false);
    }

    public void executeCommand(Command c)
    {
        if (c != null)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            NavMeshHit hit;
            NavMesh.SamplePosition(c.worldPoint, out hit, 10f, 1);
            agent.SetDestination(hit.position);
            spawnedMarker.GetComponent<PositionMarker>().ToggleMarker(true);
            spawnedMarker.GetComponent<PositionMarker>().MoveMarker(hit.position);
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Obstacle":
                {
                    NavMeshAgent agent = GetComponent<NavMeshAgent>();
                    agent.Stop();
                    agent.enabled = false;
                    NavMeshHit startPos;
                    NavMesh.SamplePosition(checkpoint.transform.position, out startPos, 10f, 1);
                    transform.position = startPos.position;
                    agent.enabled = true;
                    break;
                }
            case "Safe":
                {
                    checkpoint = other.gameObject;
                    break;
                }
        }
    }
}
