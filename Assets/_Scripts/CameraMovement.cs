using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerPosition;
    private GameObject golemposition;
    private float offset = -10;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        golemposition = GameObject.FindGameObjectWithTag("Golem");

        if(golemposition == null)
        {
            transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, offset);
        }
        else
        {
            transform.position = new Vector3(golemposition.transform.position.x, golemposition.transform.position.y, offset);
        }
    }
}
