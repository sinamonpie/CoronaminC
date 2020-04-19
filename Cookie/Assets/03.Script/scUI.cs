using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scUI : MonoBehaviour {
    
    public Text Coin_tx;
    public Text Jelly_tx;

    public GameObject Lifebar;
    public GameObject Lifebar_plus;

    public GameObject ReLifeButton;

    public bool Relife = false;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Use this for initialization
    void Start () {
        ReLifeButton.SetActive(false);
        
    }

    public void OnReLifeButton()
    {
        Relife = true;
    }

	// Update is called once per frame
	void Update () {

	}
}
