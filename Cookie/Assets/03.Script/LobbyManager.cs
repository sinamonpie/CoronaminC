using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyManager : MonoBehaviour
{
    GameObject scUi = null;
    public void Start()
    {
        scUi = GameObject.Find("UIScene");
        if(scUi != null)
        {
            Destroy(scUi.gameObject);
        }
    }

    public void OnStartButton()
    {
        StartCoroutine(this.Load());
    }

    IEnumerator Load()
    {
        SceneManager.LoadScene("scUI");
        AsyncOperation ao = SceneManager.LoadSceneAsync("MainGame_Scene");

        while (!ao.isDone)
        {
            yield return null;
        }
    }
}
