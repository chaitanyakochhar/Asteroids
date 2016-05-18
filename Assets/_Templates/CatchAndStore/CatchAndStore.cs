using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Authenticator))]
public class CatchAndStore : MonoBehaviour
{
    public bool GivingThemSomething = true;
    public GameObject playerCharacter;
    public GameObject givingThis;
    public GameObject spawnPoint;

    public int numberOfPlayers = 2;
    private int itemsLeft;
    // Update is called once per frame
    public void Start()
    {
        itemsLeft = GameObject.FindGameObjectsWithTag("Obstacle").Length;
        StartCoroutine(Authenticate());
    }

    void Update()
    {
        MouseListener();

    }

    private void MouseListener()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Command c = Command.createCommandWithoutRaycast(Input.mousePosition, true);
            if (c != null)
            {
                CharacterPingPong(c);
            }

        }
    }

    private void CharacterPingPong(Command c)
    {
        if (numberOfPlayers > 0)
        {
            numberOfPlayers--;
            GameObject GO = Instantiate(playerCharacter, transform.position, Quaternion.identity) as GameObject;
            Carrier carrier;
            if (GO.GetComponent<Carrier>() == null)
            {
                carrier = GO.AddComponent<Carrier>();
            }
            else
            {
                carrier = GO.GetComponent<Carrier>();
            }
            carrier.startPoint = transform.position;
            Vector3 v = c.worldPoint;
            v.z = transform.position.z;
            carrier.givingThis = givingThis;
            carrier.isGiving = GivingThemSomething;
            carrier.GoToAndReturn(v);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Carrier carrier = collision.transform.GetComponent<Carrier>();
        if (carrier != null && !carrier.Going)
        {
            Destroy(collision.gameObject);
            numberOfPlayers++;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        Carrier carrier = collision.transform.GetComponent<Carrier>();
        if (carrier != null && !carrier.Going)
        {
            Destroy(collision.gameObject);
            numberOfPlayers++;
        }
    }

    private IEnumerator Authenticate()
    {
        while (true)
        {
            itemsLeft = GameObject.FindGameObjectsWithTag("Obstacle").Length;

            if (itemsLeft == 0)
            {
                GetComponent<Authenticator>().isAuthenticated = true;
            }
            yield return null;
        }
    }
}
