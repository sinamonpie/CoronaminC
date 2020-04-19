using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

    GameObject player;

    private int rand = 0;

    public GameObject[] StartGround;
    public GameObject[] Ground;
    public GameObject[] EndGround;

    public float Speed = 10.0f;
    public float delayTime = 4.5f;
    public float posX = 50.0f;
    public float tempX = 50.0f;
    public float posY = 0.0f;

    public int stage = 1;
    public bool start = false;
    public bool end = false;
    public bool play = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Use this for initialization
    void Start () {
        StartCoroutine("CreateGround");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CreateGround()
    {
        GameObject g = Instantiate(StartGround[stage - 1], new Vector2(posX, posY), Quaternion.identity);
        g.GetComponent<Ground>().Init(Speed);
        play= true;
        while (!player.GetComponent<PlayerCtrl>().death)
        {
            if (end)
            {
                GameObject ground = Instantiate(EndGround[stage - 2], new Vector2(posX + tempX, posY), Quaternion.identity);
                ground.GetComponent<Ground>().Init(Speed);
                end = false;
                start = true;
            }
            else if (start)
            {
                GameObject ground = Instantiate(StartGround[stage - 1], new Vector2(posX+tempX, posY), Quaternion.identity);
                ground.GetComponent<Ground>().Init(Speed);
                start = false;
                play = true;
            }
            else if (play)
            {
                if (stage == 1)
                    rand = UnityEngine.Random.Range(0, 4);
                else if (stage == 2)
                {
                    rand = UnityEngine.Random.Range(5, 9);
                }
                else if (stage == 3)
                {
                    rand = UnityEngine.Random.Range(10, 14);
                }

                GameObject ground = Instantiate(Ground[rand], new Vector2(posX + tempX, posY), Quaternion.identity);
                ground.GetComponent<Ground>().Init(Speed);
            }
            yield return new WaitForSeconds(delayTime);
        }
    }
}
