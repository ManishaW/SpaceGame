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
            missionDescText.SetText("Explore the Area");
        }
        if (satelliteDamagedCount==3){
            SatelliteMissionPassed =true;
            Debug.Log("satellite mission passed");
        }
        //if rover mission passed, trigger cinematic
        
    }
}
