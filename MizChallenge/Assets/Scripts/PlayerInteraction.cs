using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    List <string> satellites = new List <string> {"Sat1", "Sat2", "Sat3"};
    public GameObject prompt;

    TextMeshProUGUI promptText;
    public GameObject Mission1Cutscene;
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
        // if (other.gameObject.name=="Sat1"){
        //     // other.GameObject
        //     Debug.Log("test");
        //     // other.gameObject.triggerSmoke();
        //     other.gameObject.startSmoke = true;
        // }
        

    }
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (satellites.Contains(other.gameObject.name)){
            promptText.SetText ("PRESS [E] TO DAMAGE");
            prompt.SetActive(true);
        }
        if (other.gameObject.name == "Gate1")
        {
            if (!MissionManager.RoverMissionPassed)
            {
                // prompt.SetActive(true);
                Mission1Cutscene.SetActive(true);
                // promptText.SetText("[GATE LOCKED]");

            }
            

        }
       
        if (other.gameObject.tag=="Rover"){
            promptText.SetText ("PRESS [E] TO CATCH");
            prompt.SetActive(true);

        }

        //TAKING ACTION
         if (satellites.Contains(other.gameObject.name) && Input.GetKeyDown(KeyCode.E)){
            //smoke particles on
           other.transform.GetChild (0).gameObject.SetActive(true);
           MissionManager.satelliteDamagedCount = MissionManager.satelliteDamagedCount  +1;
           other.gameObject.GetComponent<BoxCollider>().enabled=false;
           prompt.SetActive(false);

        }
        if (other.gameObject.tag=="Rover" && Input.GetKeyDown(KeyCode.E)){
            Destroy(other.gameObject);
           prompt.SetActive(false);
           MissionManager.RoverMissionPassed = true;

        }
        
    }


    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
       
        
    }

    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (satellites.Contains(other.gameObject.name)||other.gameObject.name=="Gate1"||other.gameObject.tag=="Rover"){
            prompt.SetActive(false);
        }
    }

}

