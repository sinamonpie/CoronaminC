using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    
    GameObject player;

    private Rigidbody2D ground;
    public float Pos = -50.0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Init(float speed)
    {
        ground = GetComponent<Rigidbody2D>();
        ground.velocity = new Vector2(-speed, 0.0f);
    }

	// Update is called once per frame
	void Update () {
		if(Pos >= transform.position.x)
        {
            Destroy(gameObject);
        }

        if (player.GetComponent<PlayerCtrl>().death)
        {
            if (!player.GetComponent<PlayerCtrl>().ReLife) {
                ground.velocity = Vector2.zero;
            }
        }
	}
}
