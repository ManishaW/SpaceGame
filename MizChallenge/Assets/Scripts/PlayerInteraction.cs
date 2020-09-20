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

    public GameObject SecurityCamCinematic;

    [Header("GameOver Cinematic")]
    public GameObject GameOverCutscene;
    public GameObject FadeToBlack;
    public Animator RaiseAlarms;
    public GameObject MissionUI;
    [Header("Lever Animators")]
    public Animator BlueLeverAnim;
    public Animator PurpleLeverAnim;
    public Animator YellowLeverAnim;

    public GameObject passwordFound;

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
            GameOverCutscene.SetActive(true);
            RaiseAlarms.SetTrigger("PoundTheAlarms");
            FadeToBlack.SetActive(true);
            MissionUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //trigger game over cutscene

        }


    }
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {

        switch (other.gameObject.tag)
        { 
            case "Switch":
                promptText.SetText("PRESS [E] TO PRESS");
                prompt.SetActive(true);
                break;

            case "Reset Button":
                promptText.SetText("PRESS [E] TO RESET LEVERS");
                prompt.SetActive(true);
                break;

            case "Documents":
                promptText.SetText("PRESS [E] TO READ");
                prompt.SetActive(true);
                break;

            case "Satellite":
                promptText.SetText("PRESS [E] TO DAMAGE");
                prompt.SetActive(true);
                break;

            case "TriggerPanToSecurity":
                SecurityCamCinematic.SetActive(true);
                MissionManager.SecurityCameraCinematicShown = true;
                break;

            case "Rover":
                promptText.SetText("PRESS [E] TO CATCH ROVER");
                prompt.SetActive(true);
                break;

            case "Hammer":
                promptText.SetText("PRESS [E] TO GRAB HAMMER");
                prompt.SetActive(true);
                break;
            
            case "Artifact":
                promptText.SetText("PRESS [E] TO PICK UP ARTIFACT");
                prompt.SetActive(true);
                break;

            
               
            
        }

        if (other.gameObject.tag=="ArtifactNeeded"){
            if (!MissionManager.ArtifactCollectedMissionPassed){
                promptText.SetText("ARTIFACT NEEDED BEFORE ENTERING");
                prompt.SetActive(true);
            }
            
            
        }

        if (other.gameObject.tag == "GateMission")
        {
            if (!MissionManager.RoverMissionPassed)
            {
                Mission1Cutscene.SetActive(true);
                RoverMovement.startRoverMovement = true;
                MissionManager.Gate1CinematicPlayed =true;
            }
        }
        else if (other.gameObject.tag == "BrokenWall")
        {
            if (MissionManager.HammerMissionPassed)
            {
                promptText.SetText("PRESS [E] TO USE HAMMER");
                prompt.SetActive(true);
            }
            else
            {
                promptText.SetText("TOOL NEEDED TO BREAK WALL");
                prompt.SetActive(true);
                MissionManager.HammerNeededKnown=true;
            }

        }
        else if (other.gameObject.tag == "SecurityDesk")
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
                MissionManager.PasswordNeededKnown=true;
            }

        }
        else if ((other.gameObject.tag == "YellowLever" || other.gameObject.tag == "BlueLever" || other.gameObject.tag == "PurpleLever") && !MissionManager.leversPulled.Contains(other.gameObject.tag))
        {
            promptText.SetText("PRESS [E] TO TURN");
            prompt.SetActive(true);
        }




        //TAKING ACTION
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (other.gameObject.tag)
            {
                case "Satellite":
                    //smoke particles on
                    other.transform.GetChild(0).gameObject.SetActive(true);
                    MissionManager.satelliteDamagedCount = MissionManager.satelliteDamagedCount + 1;
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                    prompt.SetActive(false);
                    break;

                case "Rover":
                    Destroy(other.gameObject);
                    prompt.SetActive(false);
                    MissionManager.RoverMissionPassed = true;
                    break;

                case "Hammer":
                    Destroy(other.gameObject);
                    prompt.SetActive(false);
                    MissionManager.HammerMissionPassed = true;
                    break;

                case "Switch":
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                    prompt.SetActive(false);
                    HutGate.SetTrigger("OpenHutGate");
                    break;

                case "Documents":
                    Destroy(other.gameObject);
                    prompt.SetActive(false);

                    break;

                case "Reset Button":
                    MissionManager.leversPulled.Clear();
                    BlueLeverAnim.SetTrigger("BlueLeverReset");
                    YellowLeverAnim.SetTrigger("YellowLeverReset");
                    PurpleLeverAnim.SetTrigger("PurpleLeverReset");
                    prompt.SetActive(false);
                    break;
                
                case "Artifact":
                    Destroy(other.gameObject);
                    MissionManager.ArtifactCollectedMissionPassed = true;
                    MissionManager.ExplosionMissionPassed =true;
                    break;
            }


           
          
            if (other.gameObject.tag == "BrokenWall" && MissionManager.HammerMissionPassed)
            {
                Destroy(other.gameObject);
                prompt.SetActive(false);
                MissionManager.BrokenWallMissionPassed = true;

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
                MissionManager.SecurityPasswordMissionPassed = true;
                prompt.SetActive(false);
                passwordFound.SetActive(true);

                //activate password found text animation
            }
            

            //LEVERS
            if (!MissionManager.leversPulled.Contains(other.gameObject.tag))
            {
                if (other.gameObject.tag == "YellowLever")
                {
                    //trigger pull lever animation
                    MissionManager.leversPulled.Add(other.gameObject.tag);
                    YellowLeverAnim.SetTrigger("YellowLeverTurn");
                    prompt.SetActive(false);
                    YellowLeverAnim.ResetTrigger("YellowLeverReset");



                }
                else if (other.gameObject.tag == "BlueLever")
                {
                    MissionManager.leversPulled.Add(other.gameObject.tag);
                    BlueLeverAnim.SetTrigger("BlueLeverTurn");
                    prompt.SetActive(false);
                    BlueLeverAnim.ResetTrigger("BlueLeverReset");


                }
                else if (other.gameObject.tag == "PurpleLever")
                {
                    MissionManager.leversPulled.Add(other.gameObject.tag);
                    PurpleLeverAnim.SetTrigger("PurpleLeverTurn");
                    prompt.SetActive(false);
                    PurpleLeverAnim.ResetTrigger("PurpleLeverReset");

                }

            }
            else
            {
                if (other.gameObject.tag == "YellowLever" || other.gameObject.tag == "BlueLever" || other.gameObject.tag == "PurpleLever")
                {
                    prompt.SetActive(false);

                }
            }
            
        }




    }


    List<string> interactables = new List<string> { "Satellite", "Gate1", "Rover", "Hammer", "BrokenWall", "Switch", "SecurityDesk", "Documents", "YellowLever", "BlueLever", "PurpleLever", "Reset Button", "Artifact", "ArtifactFound"};

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

