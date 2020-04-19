using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreManage : MonoBehaviour
{
    public Canvas can;
    public GameObject cookie;
    public GameObject buy;
    public GameObject[] button;
    public Text price_tx;
    public Text Coin;
    public Text[] Jelly;
    public Text[] Life;
    public Text[] item;

    public GameObject[] Cookiegun;
    public GameObject[] Moon;

    public GameObject[] Cookie;

    string lv = "LV.";

    int price;
    int index;

    public int hpup=0;
    public int coinx2 = 0;
    public int lifeup = 0;

    public int JellyLevel = 1;
    public int LifeLevel = 1;

    public int havecoin=0;

    public bool cookiegun = true;
    public bool moon = false;

    bool moonbuy = false;


    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SelectCookiegun()
    {
        moon = false;
        cookiegun = true;
    }

    public void SelectMoon()
    {
        if (!moonbuy)
        {
            if (havecoin >= 10000)
            {
                havecoin -= 10000;
                moonbuy = true;
                moon = true;
                cookiegun = false;
            }
            else
            {
                moon = false;
                cookiegun = true;
            }
        }
        else
        {
            moon = true;
            cookiegun = false;
        }
    }

    public void SelectCookie()
    {
        cookie.SetActive(true);
    }
    
    public void ExitSelectCookie()
    {
        cookie.SetActive(false);
    }

    public void SelectJellyLv()
    {
        index = 0;
        buy.SetActive(true);
        for(int i = 0; i < 5; i++)
        {
            button[i].SetActive(false);
        }
        button[index].SetActive(true);
        price = 1000 * JellyLevel * LifeLevel * 1/10;
    }

    public void SelectLifeLv()
    {
        index = 1;
        buy.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            button[i].SetActive(false);
        }
        button[index].SetActive(true);
        price = 1000 * LifeLevel * JellyLevel * 1 / 10;
    }

    public void SelectHpUp()
    {
        index = 2;
        buy.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            button[i].SetActive(false);
        }
        button[index].SetActive(true);
        price = 500;
    }

    public void SelectGoldX2()
    {
        index = 3;
        buy.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            button[i].SetActive(false);
        }
        button[index].SetActive(true);
        price = 800;
    }

    public void SelectLifeUp()
    {
        index = 4;
        buy.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            button[i].SetActive(false);
        }
        button[index].SetActive(true);
        price = 1000;
    }

    public void BuyItem()
    {
        if (havecoin > price)
        {
            switch (index)
            {
                case 0:
                    havecoin -= price;
                    JellyLevel++;
                    price = 1000 * JellyLevel * LifeLevel * 1 / 10;
                    break;

                case 1:
                    havecoin -= price;
                    LifeLevel++;
                    price = 1000 * JellyLevel * LifeLevel * 1 / 10;
                    break;

                case 2:
                    hpup++;
                    havecoin -= price;
                    break;

                case 3:
                    coinx2++;
                    havecoin -= price;
                    break;

                case 4:
                    lifeup++;
                    havecoin -= price;
                    break;
            }
        }
    }

    public void GameStart()
    {
        StartCoroutine(this.Game());
    }

    IEnumerator Game()
    {
        can.gameObject.SetActive(false);
        SceneManager.LoadScene("scUI");
        AsyncOperation ao = SceneManager.LoadSceneAsync("MainGame_Scene");
        
        while (!ao.isDone)
        {
            yield return null;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            havecoin += 30000;
        }

        if (cookiegun)
        {
            Cookiegun[0].SetActive(false);
            Cookiegun[1].SetActive(true);
            if (!moonbuy)
            {
                Moon[0].SetActive(false);
                Moon[1].SetActive(false);
                Moon[2].SetActive(true);
            }
            else
            {
                Moon[0].SetActive(true);
                for (int i = 1; i < 3; i++)
                {
                    Moon[i].SetActive(false);
                }
                Cookie[0].SetActive(true);
                Cookie[1].SetActive(false);
            }
        }

        else if (moon)
        {
            if (!moonbuy)
            {
                Moon[0].SetActive(false);
                Moon[1].SetActive(false);
                Moon[2].SetActive(true);
            }
            else
            {
                Moon[0].SetActive(false);
                Moon[1].SetActive(true);
                Moon[2].SetActive(false);

                Cookiegun[0].SetActive(true);
                for (int i = 1; i < 2; i++)
                {
                    Cookiegun[i].SetActive(false);
                }
                Cookie[0].SetActive(false);
                Cookie[1].SetActive(true);
            }
        }

        price_tx.text = price.ToString();
        Jelly[0].text = lv+JellyLevel.ToString();
        Jelly[1].text = lv+JellyLevel.ToString();
        Life[0].text = lv+LifeLevel.ToString();
        Life[1].text = lv+LifeLevel.ToString();
        item[0].text = hpup.ToString();
        item[1].text = coinx2.ToString();
        item[2].text = lifeup.ToString();
        Coin.text = havecoin.ToString();
    }
}
