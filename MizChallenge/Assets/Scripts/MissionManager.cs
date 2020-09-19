using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public GameObject missionDesc;
    TextMeshProUGUI missionDescText;

    public static int satelliteDamagedCount;
    public static bool RoverMissionPassed = false;
    public static bool HammerMissionPassed =false;
    public static bool BrokenWallMissionPassed =false;
    public static bool SecurityPasswordMissionPassed =false;
    public static bool SecurityDeactivatedMissionPassed = false;
    public Animator gateOpenAnim;
    public Animator gate2OpenAnim;
    public GameObject gateCinematic;
    public GameObject gate2Cinematic;
    private bool SatelliteMissionPassed = false;

    
    // Start is called before the first frame update
    void Start()
    {
        satelliteDamagedCount =0;
        missionDescText = missionDesc.GetComponent<TextMeshProUGUI>();
        // SatelliteSmoke[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!RoverMissionPassed){
            missionDescText.SetText("Mission 1:\nCatch the Rover");
        }
        if(RoverMissionPassed){
            missionDescText.SetText("Mission 1:\nExpore or something");
            gateOpenAnim.SetTrigger("OpenGate");
            gateCinematic.SetActive(true);
        }
      
        if(SatelliteMissionPassed){
            missionDescText.SetText("Mission 2:\nFind a way out");
            //cue cutscene
        }
        if(BrokenWallMissionPassed){
            missionDescText.SetText("Mission 3:\nShut down the Security System.");
            //cue cutscene
        }
        if (satelliteDamagedCount==3){
            SatelliteMissionPassed =true;

        }
        if (SecurityDeactivatedMissionPassed){
            gate2OpenAnim.SetTrigger("Gate2Open");
            gate2Cinematic.SetActive(true);

        }
        //if rover mission passed, trigger cinematic
        
    }
}
