using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class MissionManager : MonoBehaviour
{
    public GameObject missionDesc;
    public GameObject pauseMenu;

    TextMeshProUGUI missionDescText;
    [Header("Mission Gate 1")]
    public GameObject gateCinematic;
    public Animator gateOpenAnim;

    [Header("Mission Fence Gate")]
    public Animator gate2OpenAnim;

    public GameObject gate2Cinematic;

    [Header("Shutdown Security")]
    public GameObject ShutdownSecurityCinematic;
    public Animator ShutdownSecurityAnim;
    

    [Header("Artifact Unlocked")]
    public GameObject ArtifactUnlockedCinematic;
    public Animator ArtifactUnlockedAnim;
    public GameObject GateBlockage;

    [Header("Explosion Outro")]
    public GameObject MissionUI;
    public GameObject FadeToBlackOutro;
    public GameObject mainMusic;
    public GameObject OutroCinematic;

    public static int satelliteDamagedCount;
    public static bool RoverMissionPassed = false;
    public static bool Gate1CinematicPlayed = false;
    public static bool SecurityCameraCinematicShown = false;
    public static bool HammerMissionPassed =false;
    public static bool BrokenWallMissionPassed =false;
    public static bool SecurityPasswordMissionPassed =false;
    public static bool SecurityDeactivatedMissionPassed = false;
    public static bool ArtifactMissionPassed=false;
    public static bool ArtifactCollectedMissionPassed=false;
    public static bool ExplosionMissionPassed=false;
    public static bool HammerNeededKnown=false;
    public static bool PasswordNeededKnown=false;
    private bool SatelliteMissionPassed = false;
    public static List<string> leversPulled=new List<string>();
    int satellitesAvail=6;
    private List<string> leverOrderCorrect = new List<string> {"PurpleLever","BlueLever","YellowLever"};

    bool pauseStatus = false;
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
        if (BrokenWallMissionPassed)
        {
            if (PasswordNeededKnown)
            {
                missionDescText.SetText("OBJECTIVE:\nFIND PASSWORD TO SYSTEM");
            }
            else
            {
                missionDescText.SetText("OBJECTIVE:\nEXPLORE THE AREA");

            }
            //cue cutscene
        }
        else if(HammerNeededKnown && !BrokenWallMissionPassed){
            missionDescText.SetText("OBJECTIVE:\nFIND TOOL TO BREAK WALL");
            //cue cutscene
        }
        else if(SatelliteMissionPassed){
            missionDescText.SetText("OBJECTIVE:\nEXPLORE THE AREA");
            //cue cutscene
        }
        else if(!RoverMissionPassed && !Gate1CinematicPlayed){
            missionDescText.SetText("Objective:\nExplore the Area");
        }
        
        if(RoverMissionPassed){
            gateOpenAnim.SetTrigger("OpenGate");
            gateCinematic.SetActive(true);
        }
       
        
        if (Gate1CinematicPlayed){
            missionDescText.SetText("OBJECTIVE:\nCAPTURE THE ROVER");
            
        }
        if (SecurityCameraCinematicShown){
            missionDescText.SetText("OBJECTIVE:\nDAMAGE "+(satellitesAvail- satelliteDamagedCount).ToString()+" SATELLITES");
        }
      
        

        

       


        if (satelliteDamagedCount==satellitesAvail){
            SatelliteMissionPassed =true;
            ShutdownSecurityCinematic.SetActive(true);
            ShutdownSecurityAnim.SetTrigger("ShutdownSecurity");


        }

        if (SecurityDeactivatedMissionPassed){
            gate2OpenAnim.SetTrigger("Gate2Open");
            gate2Cinematic.SetActive(true);

        }
        if (leverOrderCorrect.SequenceEqual(leversPulled)){
            ArtifactUnlockedCinematic.SetActive(true);
            ArtifactUnlockedAnim.SetTrigger("ArtifactUnlock");
            ArtifactMissionPassed =true;
            GateBlockage.SetActive(false);
        }
        if (ExplosionMissionPassed){
            //trigger outro
            FadeToBlackOutro.SetActive(true);
            MissionUI.SetActive(false);
            mainMusic.SetActive(false);
            OutroCinematic.SetActive(true);
        }
        



        //if rover mission passed, trigger cinematic

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseStatus=!pauseStatus;
            pauseMenu.SetActive(pauseStatus);
            if (pauseStatus){
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }else{
                Time.timeScale=1;
                Cursor.lockState=CursorLockMode.Locked;

            }
        }
    }
}
