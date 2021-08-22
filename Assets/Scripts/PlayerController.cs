using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject loxo;
    [SerializeField]
    GameObject gameOverPage, nice, Score, poolObject, water,poleClone;
    [SerializeField]
    Rigidbody2D rb;

    public GameObject fade;
    [SerializeField]
    Animator animFade;
    [SerializeField]
    Animator anim, animNinja;
    public AudioSource Fall, jump_end, jump_start;
    public Text scoreNow;
    public Text gameOverScore;
    public Text highScore;
    public float speed = 7.7f;
    private float thrust = 0f;
    Vector2 permanent;

    public static Vector2 lx, nj;
    public static int score ;
    public static bool check;
    public static bool isOver;
    public static bool isEnd;
    int abc = 0;
    // Start is called before the first frame update
    void Start()
    {
        permanent = transform.localPosition;
        isOver = false;
        isEnd = false;
        check = false;
        score = 0;
        anim.enabled = false;
        lx = loxo.transform.localScale;
        nj = transform.localPosition;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && lx.y >= 0.015f && !isOver)
        {
            //lxnen.Play();
            animNinja.SetBool("State", true);
            animFade.enabled = false;
            fade.SetActive(false);
            abc = 0;
            lx = loxo.transform.localScale;
            nj = transform.localPosition;
            anim.enabled = false;
            isEnd = true;
            lx.y -= 0.0005f;
            nj.y -= 0.0065f;
            loxo.transform.localScale = lx;
            transform.localPosition = nj;
            thrust += speed*Time.deltaTime;
            check = true;

        }
        if (!Input.GetMouseButton(0) && check)
        {
            jump_start.Play();
            animNinja.SetBool("State", false);
            isEnd = false;
            rb.AddForce(new Vector2(0.3f, 0.8f) * thrust);

            isOver = true;
            check = false;
            loxo.SetActive(false);
            thrust = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Fall.Play();
            lx.y = 0.028f;
            isOver = true;
            isEnd = true;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            Score.SetActive(false);
            gameOverPage.SetActive(true);
            fade.SetActive(true);   
            poolObject.SetActive(false);
            water.SetActive(false);
            poleClone.SetActive(false);
            PlayerPrefs.SetInt("ScoreNow", score);
            gameOverScore.text=PlayerPrefs.GetInt("ScoreNow").ToString();
            CheckHighscore();
            transform.position = permanent; 
            loxo.transform.localScale = new Vector2(0.022f, 0.028f);
            loxo.transform.position = new Vector2(-1.829f, -0.0556f);
        }

        if (collision.CompareTag("point"))
        {
            jump_end.Play();
            if (abc >= 2)
            {
                score += 2 * abc;
            }
            else if (abc == 1)
                score++;
            scoreNow.text = score.ToString();
            isOver = false;
           
            loxo.SetActive(true);
            SpawnLoxo(collision.gameObject.transform);
        }
        if (collision.CompareTag("consecutive"))
        {
            abc++;
           
        }

        if (collision.CompareTag("bird"))
        {
            collision.gameObject.GetComponent<Animator>().SetBool("State", true);
            if (collision.gameObject.transform.rotation == Quaternion.Euler(0, 180, 0))
            {
                LeanTween.move(collision.gameObject, new Vector2(Random.Range(transform.position.x+4f, 8f+ transform.position.x), 8f), 1.2f);
            }
            else LeanTween.move(collision.gameObject, new Vector2(Random.Range(transform.position.x - 4f, transform.position.x),8f), 1.2f);
        }
        
    }

    void SpawnLoxo(Transform pole)
    {
       
        lx.y = 0.028f;
        rb.velocity = Vector2.zero;
        loxo.transform.position = new Vector2(transform.position.x - 0.05f, pole.position.y);
        if(GameManager.checkPice1)
        {
            transform.localPosition = new Vector2(transform.position.x, pole.position.y + 0.55f);
        }
        else if (GameManager.checkPice2)
        {
            transform.localPosition = new Vector2(transform.position.x, pole.position.y + 0.54f);
        }
        else if(GameManager.checkPice3)
            transform.localPosition = new Vector2(transform.position.x, pole.position.y + 0.55f);
        else
            transform.localPosition = new Vector2(transform.position.x, pole.position.y + 0.47f);
        anim.enabled = true;

    }

    void CheckHighscore()
    {
        nice.SetActive(false);
        int highscore = PlayerPrefs.GetInt("HighScore");
        if (score > highscore) 
        {
            PlayerPrefs.SetInt("HighScore", score);
            nice.SetActive(true);
        }
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
