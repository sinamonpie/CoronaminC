using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour {

    GameObject player;

    public GameObject[] StartGround;
    public GameObject[] Ground;
    public GameObject[] EndGround;


    public int stage = 1;
    public bool start = false;
    public bool end = false;
    public bool play = false;

    public float Speed = 10.0f;
    public float delayTime = 4.5f;
    public float posX = 50.0f;
    public float tempX = 50.0f;
    public float posY = 0.0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine("CreateGround");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CreateGround()
    {
        GameObject g = Instantiate(StartGround[stage - 1], new Vector2(posX, posY), Quaternion.identity);
        g.GetComponent<BackGround>().Init(Speed);
        play = true;

        while (!player.GetComponent<PlayerCtrl>().death)
        {
            if (end)
            {
                GameObject ground = Instantiate(EndGround[stage - 2], new Vector2(posX + tempX, posY), Quaternion.identity);
                ground.GetComponent<BackGround>().Init(Speed);
                end = false;
                start = true;
            }
            else if (start)
            {
                GameObject ground = Instantiate(StartGround[stage - 1], new Vector2(posX + tempX, posY), Quaternion.identity);
                ground.GetComponent<BackGround>().Init(Speed);
                start = false;
                play = true;
            }
            else if (play)
            {
                GameObject ground = Instantiate(Ground[stage-1], new Vector2(posX + tempX, posY), Quaternion.identity);
                ground.GetComponent<BackGround>().Init(Speed);
            }
            yield return new WaitForSeconds(delayTime);
        }
    }
    
}
