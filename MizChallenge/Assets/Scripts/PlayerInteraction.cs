using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject prompt;

    TextMeshProUGUI promptText;
    public GameObject Mission1Cutscene;
    public Animator HutGate;

    // Start is called before the first frame update
    void Start()
    {
        promptText = prompt.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CaughtTrigger")
        {
            // other.GameObject
            Debug.Log("caught!!");
            //trigger game over cutscene

        }


    }
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Switch")
        {
            promptText.SetText("PRESS [E] TO PRESS");
            prompt.SetActive(true);

        }
        if (other.gameObject.tag == "Documents")
        {
            promptText.SetText("PRESS [E] TO READ");
            prompt.SetActive(true);

        }
        if (other.gameObject.tag == "Satellite")
        {
            promptText.SetText("PRESS [E] TO DAMAGE");
            prompt.SetActive(true);
        }
        if (other.gameObject.tag == "GateMission")
        {
            if (!MissionManager.RoverMissionPassed)
            {
                Mission1Cutscene.SetActive(true);
                RoverMovement.startRoverMovement = true;
            }


        }

        if (other.gameObject.tag == "Rover")
        {
            promptText.SetText("PRESS [E] TO CATCH ROVER");
            prompt.SetActive(true);

        }
        if (other.gameObject.tag == "Hammer")
        {
            promptText.SetText("PRESS [E] TO GRAB HAMMER");
            prompt.SetActive(true);

        }

        if (other.gameObject.tag == "BrokenWall")
        {
            if (MissionManager.HammerMissionPassed)
            {
                promptText.SetText("PRESS [E] TO USE HAMMER");
                prompt.SetActive(true);
            }
            else
            {
                promptText.SetText("NEED TOOL TO BREAK THIS WALL");
                prompt.SetActive(true);
            }


        }
        if (other.gameObject.tag == "SecurityDesk")
        {
            if (MissionManager.SecurityPasswordMissionPassed)
            {
                promptText.SetText("PRESS [E} TO OPEN GATE");
                prompt.SetActive(true);

            }
            else
            {
                promptText.SetText("PASSWORD NEEDED TO LOGIN ");
                prompt.SetActive(true);
            }

        }


        //TAKING ACTION
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.gameObject.tag == "Satellite")
            {
                //smoke particles on
                other.transform.GetChild(0).gameObject.SetActive(true);
                MissionManager.satelliteDamagedCount = MissionManager.satelliteDamagedCount + 1;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                prompt.SetActive(false);

            }
            if (other.gameObject.tag == "Rover")
            {
                Destroy(other.gameObject);
                prompt.SetActive(false);
                MissionManager.RoverMissionPassed = true;

            }
            if (other.gameObject.tag == "Hammer")
            {
                Destroy(other.gameObject);
                prompt.SetActive(false);
                MissionManager.HammerMissionPassed = true;

            }
            if (other.gameObject.tag == "BrokenWall" && MissionManager.HammerMissionPassed)
            {
                Destroy(other.gameObject);
                prompt.SetActive(false);
                MissionManager.BrokenWallMissionPassed = true;

            }
            if (other.gameObject.tag == "Switch")
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                prompt.SetActive(false);
                HutGate.SetTrigger("OpenHutGate");


                //trigger animation

            }
            if (other.gameObject.tag == "SecurityDesk" && MissionManager.SecurityPasswordMissionPassed)
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                prompt.SetActive(false);
                MissionManager.SecurityDeactivatedMissionPassed = true;

            }
            if (other.gameObject.tag == "Documents" && other.gameObject.name == "PasswordNote")
            {
                Destroy(other.gameObject);
                MissionManager.SecurityPasswordMissionPassed=true;
                prompt.SetActive(false);
                Debug.Log("Found pswd");
                //activate password found text animation
            }else if (other.gameObject.tag == "Documents"){
                Destroy(other.gameObject);
            }
        }



    }


    List<string> interactables = new List<string> { "Satellite", "Gate1", "Rover", "Hammer", "BrokenWall", "Switch", "SecurityDesk", "Documents" };

    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (interactables.Contains(other.gameObject.tag))
        {
            prompt.SetActive(false);
        }
    }

}

