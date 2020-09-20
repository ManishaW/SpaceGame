using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    Vector3 velocity;
    public float speed = 12f;
    private float gravity=-9.81f;
    public GameObject [] spawnNodes;

    // Start is called before the first frame update
    void Start()
    {
        // transform.position = spawnNodes[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x= Input.GetAxis("Horizontal");
        float z= Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward *z;
        controller.Move(move *speed *Time.deltaTime);
        velocity.y +=gravity *Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);

    }

     
}
