using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    public Text[] price;
    public static bool checkPice1, checkPice2, isMute, checkPice3;
    public Button Buy, after, next,BttPlay;
    public AudioSource play;
    [SerializeField]
    GameObject startPage, ninja,Ninja1,ninja2,ninja3, cloud, loxo, poleClone, scoreText, poleClassic, gameOverPage ;
    [SerializeField]
    GameObject[] background, backgroundImage, ninja1;
    [SerializeField]
    GameObject poolobject, water, playbutton, player, loxoBackground, shop, volume0,volume1,volume2, poleBackground, shopNinjaPage;
    public GameObject fade;
    [SerializeField]
    GameObject newNinja, player1, newNinjab;
  
    [SerializeField]
    Animator animFade;
    public static int index;
    public static int x;
    [System.Serializable]
    public struct YSpawnRange
    {
        public float min;
        public float max;
    }
    public YSpawnRange ySpawnRange;

    private void Start()
    {
        x = 0;
        fade.SetActive(false);
        animFade.enabled = false;
        index = Random.Range(0,background.Length);
        if(index==0)
        {
            water.SetActive(true);
        }
        if (checkPice1)
        {
            x = 0;
            player1.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
            player.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
        }
        if (checkPice2)
        {
            x = 1;
            player1.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
            player.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
        }

        if (!isMute)
        {
            volume0.GetComponent<Image>().sprite = volume1.GetComponent<Image>().sprite;
        }
        else
            volume0.GetComponent<Image>().sprite = volume2.GetComponent<Image>().sprite;
        backgroundImage[index].SetActive(true);
    }

    public void PlayGame()
    {
        play.Play();
        shopNinjaPage.SetActive(false);
        scoreText.SetActive(true);
        background[index].SetActive(true);
        if (checkPice1)
        {
            Ninja1.SetActive(true);
        }
        else if (checkPice2)
        {
            ninja2.SetActive(true);
        }
        else if (checkPice3)
            ninja3.SetActive(true);
        else 
            ninja.SetActive(true);
        loxo.SetActive(true);
        poleClassic.SetActive(true);
        cloud.SetActive(true);
        poleClone.SetActive(true);
        startPage.SetActive(false);
    }

    public void RestartGame()
    {
        play.Play();
        Fading();
        
        water.SetActive(false);
        for (int c=0; c < background.Length; c++)
        {
            background[c].SetActive(false);
        }
        index = Random.Range(0, background.Length);
        
        if (index == 0)
        {
            water.SetActive(true);
        } 
        background[index].SetActive(true);
        scoreText.SetActive(true);
        score.text = "0";
        PlayerController.score = 0;
        PoleController.distance = 0f;

        gameOverPage.SetActive(false);

        PlayerController.isEnd = false;
        PlayerController.isOver = false;
        if(checkPice1)
            Ninja1.GetComponent<Rigidbody2D>().isKinematic = false;
        else if(checkPice2)
            ninja2.GetComponent<Rigidbody2D>().isKinematic = false;
        else if (checkPice3)
        {
            ninja3.GetComponent<Rigidbody2D>().isKinematic = false;
        }else
            ninja.GetComponent<Rigidbody2D>().isKinematic = false;
        loxo.SetActive(true);
        for (int i=0; i < PoolObject.Instance.pole.Count; i++)
        {
            PoolObject.Instance.pole[i].SetActive(false);
        }

        for (int i = 0; i < PoolObject.Instance.bird.Count; i++)
        {
            PoolObject.Instance.bird[i].SetActive(false);
        }

        for (int i = 0; i < PoolObject.Instance.tree1.Count; i++)
        {
            PoolObject.Instance.tree1[i].SetActive(false);
        }
        for (int i = 0; i < PoolObject.Instance.tree2.Count; i++)
        {
            PoolObject.Instance.tree2[i].SetActive(false);
        }
        poleClone.SetActive(true);
        GameObject obj = PoolObject.Instance.SpawnPole();

        obj.transform.position = new Vector2(Camera.main.transform.position.x + 15f, Random.Range(ySpawnRange.min, ySpawnRange.max));
        float m = Random.Range(0.1f, 1f);
        LeanTween.move(obj, new Vector2(m, obj.transform.position.y), 0.7f);
        PoleController.distance = m;

        obj = PoolObject.Instance.SpawnPole();
        obj.transform.position = new Vector2(Camera.main.transform.position.x + 15f, Random.Range(ySpawnRange.min, ySpawnRange.max));
        m = Random.Range(1f, 2f);
        LeanTween.move(obj, new Vector2(PoleController.distance + m, obj.transform.position.y),1f);
        PoleController.distance += m;
        if (index != 0)
        {
            obj = PoolObject.Instance.SpawnTree1();
            obj.transform.position = new Vector2(0, -0.05f);
            TreeController.ds = 0;

            obj = PoolObject.Instance.SpawnTree2();
            obj.transform.position = new Vector2(1, 0.4f);
            Tree2Controller.ds1 = 1;
        }

        poolobject.SetActive(true); 
    }

    public void Home()
    {
        play.Play();
        Application.LoadLevel("GameScene");
    }

    void  Fading()
    {
        fade.SetActive( true);
        animFade.enabled = true;
    }
    
    public void ShopNinja()
    {
        playbutton.SetActive(false);
        player.SetActive(false);
        loxoBackground.SetActive(false);
        shop.SetActive(false);
        volume0.SetActive(false);
        poleBackground.SetActive(false);
        shopNinjaPage.SetActive(true);
        ninja1[x].SetActive(true);
        BttPlay.enabled = false;
        BttPlay.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
        if (checkPice1)
        {
            Buy.enabled = false;
            Buy.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
            price[0].enabled = false;
        }
        else if(checkPice2)
        {
            Buy.enabled = false;
            Buy.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
            price[1].enabled = false;
        }
        else if (checkPice3)
        {
            Buy.enabled = false;
            Buy.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
            price[2].enabled = false;
        }
        after.enabled = false;
        after.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
        next.enabled = true;
        next.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        
        
    }
    public void BuyNinja()
    {
        if (x == 0)
        {
            checkPice1 = true;
           
        }
        else if (x == 1)
        {
            checkPice2 = true;
            
        }
        else if (x == 2)
        {
            checkPice3 = true;
        }
        if (checkPice1||checkPice2||checkPice3)
        {
            Buy.enabled = false;
            Buy.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
            BttPlay.enabled = true;
            BttPlay.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            price[x].enabled = false;
        }
        
        newNinja.SetActive(true);  
        newNinjab.GetComponent<Image>().sprite= ninja1[x].GetComponent<Image>().sprite;
        player1.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
        player.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
    }
    public void ButtonCancel()
    {
        newNinja.SetActive(false); 
    }
    public void Cancel()
    { 
        shopNinjaPage.SetActive(false);
        playbutton.SetActive(true);
        player.SetActive(true);
        loxoBackground.SetActive(true);
        shop.SetActive(true);
        volume0.SetActive(true);
        poleBackground.SetActive(true);
       
    }

    public void After()
    {
        ninja1[x].SetActive(false);
        x -= 1;
        ninja1[x].SetActive(true);
        CheckAfterorBefore(x);
        
    }

    public void Next()
    {
        ninja1[x].SetActive(false);
        x +=1;
        ninja1[x].SetActive(true);
        CheckAfterorBefore(x);
    }
    void CheckAfterorBefore(int x)
    {
        if (x == 0)
        {
            player1.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
            after.enabled = false;
            after.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
            next.enabled = true;
            next.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (!checkPice1)
            {
                Buy.enabled = true;
                Buy.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                BttPlay.enabled = false;
                BttPlay.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
                price[x].enabled = true;
            }
        }
        else if (x == 2)
        {
            player1.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
            after.enabled = true;
            after.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            next.enabled = false;
            next.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
            if (!checkPice3)
            {
                Buy.enabled = true;
                Buy.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                BttPlay.enabled = false;
                BttPlay.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
                price[x].enabled = true;
            }
        }
        else
        {
            player1.GetComponent<Image>().sprite = ninja1[x].GetComponent<Image>().sprite;
            after.enabled = true;
            after.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            next.enabled = true;
            next.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (!checkPice2)
            {
                Buy.enabled = true;
                Buy.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                BttPlay.enabled = false;
                BttPlay.GetComponent<Image>().color = new Color32(255, 255, 255, 112);
                price[x].enabled = true;
            }
        }
    }
    public void MuteAllSound()
    {
        if (DontDestroy.Instance.backgroundAudio[GameManager.index].isPlaying)
        {
            DontDestroy.Instance.backgroundAudio[GameManager.index].Pause();
            volume0.GetComponent<Image>().sprite = volume2.GetComponent<Image>().sprite;
            isMute = true;
        }
        else
        {
            DontDestroy.Instance.backgroundAudio[GameManager.index].Play();
            volume0.GetComponent<Image>().sprite = volume1.GetComponent<Image>().sprite;
            isMute = false;
        }
    }
}
