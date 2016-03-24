using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour {

    public GameObject[] waypoints;
    private State moverState = State.MOTION;
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
            case State.MOTION:
                {
                    NavMeshHit hit;
                    NavMesh.SamplePosition(waypoints[currentLocation].transform.position, out hit, 10, 1);
                    GetComponent<NavMeshAgent>().SetDestination(hit.position);
                    if(Vector3.Distance(hit.position, transform.position)<3)
                    {
                        moverState = State.REST;
                    }
                    break;
                }
            case State.REST:
                {
                    currentLocation = (currentLocation + 1) % waypoints.Length;
                    moverState = State.MOTION;
                    break;
                }
        }
    }
}
