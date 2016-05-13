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
    public int itemsToGive = 3;
    public int itemsCollected = 0;

    // Update is called once per frame
    void Update()
    {
        MouseListener();

    }

    private void MouseListener()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Command c = Command.createCommandWithoutRaycast(Input.mousePosition);
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
            if(carrier.Done)
            {
                itemsCollected++;
                Authenticate();
            }
            Destroy(collision.gameObject);
            numberOfPlayers++;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        Carrier carrier = collision.transform.GetComponent<Carrier>();
        if (carrier != null && !carrier.Going)
        {
            if (carrier.Done)
            {
                itemsCollected++;
                Authenticate();
            }
            Destroy(collision.gameObject);
            numberOfPlayers++;
        }
    }

    private void Authenticate()
    {
        if (itemsToGive == itemsCollected)
        {
            GetComponent<Authenticator>().isAuthenticated = true;
        }
    }
}
