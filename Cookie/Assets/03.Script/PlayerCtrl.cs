using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour {

    scUI scUi;
    GameObject Store;
    public GameObject Ground;
    public GameObject BackGround;

    Slider LifeSlider;

    public Animator[] Anim;
    Animator who;
    public bool death = false;

    int coin = 0;
    int save_coin = 0;
    int jelly = 0;
   
    public BoxCollider2D Wake_col;
    public BoxCollider2D Slide_col;

    public float life = 1000.0f;
    public Renderer Arpha;

    private Animator anim;

    int count;
    public float max_life = 1000.0f;
    [HideInInspector]
    public int jumpcount = 2;

    [HideInInspector]

    bool jump = false;
    bool slide = false;

    public bool Hit = false;

    public float jumpForce = 1000f;
    public float doublejumpForce = 500f;
    public float x = 0f;
    public float y = -10f;

    bool cookiegun = true;
    bool moon = false;

    int coin2 = 1;
    int jellylv = 1;

    public bool ReLife = false;

    private Rigidbody2D rigidbody2D;

    bool paused= false;

    int stage = 1;
    //Physics2D.gravity = new Vector2(3.0f,3.0f);
    // Use this for initialization
    void Awake()
    {
        //store = GameObject.Find("StoreMan")
        Wake_col.enabled = true;
        Slide_col.enabled = false;
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Arpha = GetComponent<SpriteRenderer>();
        who = GetComponent<Animator>();
        //Store = GameObject.Find("StoreManager");
        scUi = GameObject.Find("UIScene").GetComponent<scUI>();
        StartCoroutine(this.stageNext());
       
    }

    private void Start()
    {
        LifeSlider = scUi.Lifebar_plus.GetComponentInChildren<Slider>();
        //if (Store.GetComponent<StoreManage>().cookiegun)
        //{
        who.runtimeAnimatorController = Anim[0].runtimeAnimatorController;
        //}
        //else if (Store.GetComponent<StoreManage>().moon)
        //{
        //    who.runtimeAnimatorController = Anim[1].runtimeAnimatorController;
        //}

        //if (Store.GetComponent<StoreManage>().hpup > 0)
        //{
        //    Store.GetComponent<StoreManage>().hpup--;
        //    scUi.Lifebar.SetActive(false);
        //    scUi.Lifebar_plus.SetActive(true);
        //    LifeSlider = scUi.Lifebar_plus.GetComponentInChildren<Slider>();
        //    max_life += 20.0f;
        //}
        //else
        //{
        //    scUi.Lifebar_plus.SetActive(false);
        //    scUi.Lifebar.SetActive(true);
        //    LifeSlider = scUi.Lifebar.GetComponentInChildren<Slider>();
        //    max_life = 1000.0f;
        //}

        //if (Store.GetComponent<StoreManage>().coinx2 > 0)
        //{
        //    Store.GetComponent<StoreManage>().coinx2--;
        //    coin2 = 2;
        //}
        //else
        //{
        //    coin2 = 1;
        //}

        //if (Store.GetComponent<StoreManage>().lifeup > 0)
        //{
        //    Store.GetComponent<StoreManage>().lifeup--;
        //    ReLife = true;
        //}
        //else
        //{
        //    ReLife = false;
        //}

        //max_life = 1000 +(Store.GetComponent<StoreManage>().LifeLevel*10);
        //jellylv = Store.GetComponent<StoreManage>().JellyLevel;
        life = max_life;
        jumpcount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        life -= 0.02f;
        LifeBarUpdate();
        scUi.Jelly_tx.text = jelly.ToString();
        scUi.Coin_tx.text = coin.ToString();

        if (Hit&&!death)
        {
            Arpha.material.color = new Color(Arpha.material.color.r, Arpha.material.color.g, Arpha.material.color.b, 0.5f);
        }
        else
        {
            Arpha.material.color = new Color(Arpha.material.color.r, Arpha.material.color.g, Arpha.material.color.b, 1.0f);
        }

        if (scUi.Relife)
        {
            paused = !paused;
        }
        else

        if(paused)
        {
            scUi.ReLifeButton.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if (!paused)
        {
            scUi.ReLifeButton.SetActive(false);
            Time.timeScale = 1.0f;
        }
     }
    
    IEnumerator stageNext()
    {
        while (!death)
        {
            if (stage >= 2)
            {
                yield return new WaitForSeconds(10.0f);
            }
            yield return new WaitForSeconds(20.0f);


            stage++;
            if (stage > 3)
            {
                stage = 3;
            }
            else
            {
                Ground.GetComponent<GroundManager>().play = false;
                Ground.GetComponent<GroundManager>().end = true;
                Ground.GetComponent<GroundManager>().stage = stage;

                BackGround.GetComponent<BackGroundManager>().play = false;
                BackGround.GetComponent<BackGroundManager>().end = true;
                BackGround.GetComponent<BackGroundManager>().stage = stage;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        {
            jumpcount = 2;
            jump = false;
            anim.SetInteger("Jump", 2);
        }
        
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeathZone")
        {
            life = 0f;
            ReLife = false;
            LifeBarUpdate();
        }
        else if (collision.gameObject.tag == "HeartLife")
        {
            life += 10.0f;
            if (life >= max_life)
            {
                life = max_life;
            }
            LifeBarUpdate();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "BigLifeKit")
        {
            life += 30.0f;
            if (life >= max_life)
            {
                life = max_life;
            }
            LifeBarUpdate();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "GoldCoin")
        {
            coin += 50*coin2;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "SleverCoin")
        {
            coin += 100 * coin2;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "GoldJelly")
        {
            jelly += 10000*jellylv/10;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "PinkJelly")
        {
            jelly += 5000 * jellylv /10;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "S_GoldCoin")
        {
            coin += 5 * coin2;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "S_SleverCoin")
        {
            coin += 10 * coin2;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "S_GoldJelly")
        {
            jelly += 1000 * jellylv / 10;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "S_PinkJelly")
        {
            jelly += 500 * jellylv / 10;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Trap"&&!Hit)
        {
            anim.SetTrigger("Hit");
            life -= 10f;
            LifeBarUpdate();
            Hit = true;
            //Crash();
            yield return new WaitForSeconds(2.0f);
            Hit = false;
        }
    }

    void LifeBarUpdate()
    {
        LifeSlider.value = life / max_life;
        if(LifeSlider.value <= 0)
        {
            StartCoroutine(this.Die());
        }
    }

    IEnumerator Die()
    {
        anim.SetBool("Die", true);
        death = true;
        rigidbody2D.velocity = Vector2.zero;
        //StoreManage
        //SceneManager.UnloadScene("scUI");
        //AsyncOperation ao = SceneManager.UnloadSceneAsync("MainGame_Scene");
        //Store.GetComponent<StoreManage>().havecoin += coin;
        //SceneManager.LoadScene("Store_Scene");
        SceneManager.LoadScene("Lobby_Scene");
        yield return null;
        
    }

    public void Jump()  //점프
    {
        jumpcount--;
        if (!slide)
        {
            if (jumpcount > -1)
            {
                //Debug.Log(jumpcount);
                if (jumpcount == 1) //1단
                {
                    Debug.Log("1단");
                    Debug.Log(jumpcount);
                    anim.SetInteger("Jump", 1);
                    rigidbody2D.velocity = Vector2.up * jumpForce;
                }
                else if (jumpcount == 0) //2단
                {
                    Debug.Log("2단");
                    Debug.Log(jumpcount);
                    anim.SetInteger("Jump", 0);
                    rigidbody2D.velocity = Vector2.up * doublejumpForce;
                }
            }
        }

    }

    public void Slide() //슬라이드
    {
        if (jumpcount == 2)
        {
            if (!slide)
            {
                slide = true;
                Wake_col.enabled = false;
                Slide_col.enabled = true;
            }
            else
            {
                slide = false;
                Wake_col.enabled = true;
                Slide_col.enabled = false;
            }
        }
    }

    private void LateUpdate()
    {
        save_coin = coin;
    }
    void FixedUpdate()
    {
        if (slide)
        {
            anim.SetBool("Slide", true);
        }
        else if (!slide)
        {
            anim.SetBool("Slide", false);
        }
    }
}
