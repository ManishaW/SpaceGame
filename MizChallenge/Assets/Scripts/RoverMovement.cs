using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverMovement : MonoBehaviour
{
    public List<Transform> targets;
    public float moveSpeed;
    public float rotationSpeed;
    private Transform selectedTarget;
    public GameObject rover;
    public static bool startRoverMovement=false;
    // Start is called before the first frame update
    void Start()
    {
        //enable once gate cutscene triggered
        var rando = Random.Range(0, 8);
        selectedTarget = targets[rando];
        InvokeRepeating("ChooseNewTarget", 3f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (startRoverMovement && !MissionManager.RoverMissionPassed){
            rover.SetActive(true);
            rover.transform.position = Vector3.MoveTowards(rover.transform.position, selectedTarget.position, moveSpeed * Time.deltaTime);

        }
    }
    void ChooseNewTarget()
    {
        var rando = Random.Range(0, 8);
        selectedTarget = targets[rando];
    }
}
