using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour {

    public GameObject[] waypoints;
    private PlayerState moverState = PlayerState.MOTION;
    private int currentLocation = 0;
	// Update is called once per frame
	void Update ()
    {
        if (waypoints.Length > 0)
        {
            moveBetweenWaypoints();
        }
	}

    private void moveBetweenWaypoints()
    {
        switch(moverState)
        {
            case PlayerState.MOTION:
                {
                    NavMeshHit hit;
                    NavMesh.SamplePosition(waypoints[currentLocation].transform.position, out hit, 10, 1);
                    GetComponent<NavMeshAgent>().SetDestination(hit.position);
                    if(Vector3.Distance(hit.position, transform.position)<0.7)
                    {
                        moverState = PlayerState.REST;
                    }
                    break;
                }
            case PlayerState.REST:
                {
                    currentLocation = (currentLocation + 1) % waypoints.Length;
                    moverState = PlayerState.MOTION;
                    break;
                }
        }
    }
}
