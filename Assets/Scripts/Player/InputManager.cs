﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.touches.Length > 0)
        {
            Touch firstTouch = Input.touches[0];
            Command c = processTouch(firstTouch);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().executeCommand(c);
        }
    }

    private Command processTouch(Touch t)
    {
        Command c = null;
        switch(t.phase)
        {
            case TouchPhase.Ended:
                {
                    c = Command.createCommand(t.position);
                    break;
                }
        }

        return c;
    }
}
