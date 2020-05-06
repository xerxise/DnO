using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isPause = false;
    public int dayCount = 1;
    public int playerHealth;
    public Camera mCamera;
    public Transform player;

    private Transform survivalJoystick;
    private Transform defenseJoystick;
    private Transform dLight;

    private SpawnManager sManager;
    private GraveSpawnManager gsManager;

    void Awake()
    {
        mCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("PLAYER").transform;
        survivalJoystick = GameObject.Find("SurvivalJoystick").transform;
        survivalJoystick.gameObject.SetActive(false);
        defenseJoystick = GameObject.Find("Joystick").transform;
        dLight = GameObject.Find("Directional Light").transform;
        sManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gsManager = GameObject.Find("GraveSpawnManager").GetComponent<GraveSpawnManager>();
        gsManager.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToSurvival()
    {
        sManager.gameObject.SetActive(false);
        gsManager.gameObject.SetActive(true);
        player.GetComponent<MoveRotation1>().isSurvival = true;
        player.position = new Vector3(100.0f, 0.5f, 0.0f);
        mCamera.transform.position = new Vector3(100.0f, mCamera.transform.position.y, mCamera.transform.position.z);
        survivalJoystick.gameObject.SetActive(true);
        defenseJoystick.gameObject.SetActive(false);
        dLight.gameObject.SetActive(false);
        GameObject.Find("Build").GetComponent<Image>().enabled = false;
        GameObject.Find("Build").transform.GetChild(0).GetComponent<Image>().enabled = false;
        GameObject.Find("Build").transform.GetChild(1).GetComponent<Image>().enabled = false;
    }

    public void GoToDefense()
    {
        sManager.gameObject.SetActive(true);
        gsManager.gameObject.SetActive(false);
        sManager.spawnCount = 0;
        sManager.wsFloat = 10.0f;
        player.GetComponent<MoveRotation1>().isSurvival = false;
        player.GetComponent<MoveRotation1>().crrentHp = 10;
        player.position = new Vector3(0.0f, 0.5f, 0.0f);
        mCamera.transform.position = new Vector3(0.0f, mCamera.transform.position.y, mCamera.transform.position.z);
        GameObject.Find("Canvas").SetActive(true);
        survivalJoystick.gameObject.SetActive(false);
        defenseJoystick.gameObject.SetActive(true);
        dLight.gameObject.SetActive(true);
        GameObject.Find("Build").GetComponent<Image>().enabled = true;
        GameObject.Find("Build").transform.GetChild(0).GetComponent<Image>().enabled = true;
        GameObject.Find("Build").transform.GetChild(1).GetComponent<Image>().enabled = true;
        dayCount++;
    }
}
