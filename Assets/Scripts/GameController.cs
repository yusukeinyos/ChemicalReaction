using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject medicineSpawn;
    public GameObject scoreText;
    public GUIText gameoverText;
    public Text orderText;
    public GameObject hpGUI;
    public GameObject playerParticle;

    private float startTime;
    private float waitTime;
    private float hpReduceTime;
    private float time;

    private bool gameOver;
    private bool isRestart;

    private string orderedMoleculeName;
    private string gotMoleculeName;

    // Use this for initialization
    void Start()
    {
        gameOver = false;
        isRestart = false;
        StartCoroutine(OrderChange());
        //StartCoroutine(SpawnWave());

    }

    // Update is called once per frame
    void Update()
    {

        if (time < 5.0f)
        {
            time += Time.deltaTime;
        }
        else
        {
            //SetOrderedMoleculeName();
            hpGUI.SendMessage("AddHP", -0.05f);
            time = 0;
        }

    }

    IEnumerator SpawnWave()
    {
        startTime = 5;//Random.Range(2, 3);
        waitTime = 3;
        hpReduceTime = 5;
        //yield return new WaitForSeconds(hpReduceTime);
        //hpGUI.SendMessage("AddHP", -0.01f);
        yield return new WaitForSeconds(startTime);
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 7, 0);
            medicineSpawn.tag = "Medicine";
            medicineSpawn.layer = 13; //Medicine
            medicineSpawn.GetComponent<BoxCollider2D>().isTrigger = true;
            Instantiate(medicineSpawn, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(waitTime);
            if (gameOver)
            {
                GameOver();
                break;
            }
        }
    }

    IEnumerator OrderChange()
    {
        waitTime = 20;
        startTime = 5;
        yield return new WaitForSeconds(startTime);
        while (true)
        {
            SetOrderedMoleculeName();
            yield return new WaitForSeconds(waitTime);
            if (gameOver)
            {
                GameOver();
                break;
            }
        }
    }

    public void HpUpdate(float deltaHP)
    {
        hpGUI.SendMessage("AddHP", deltaHP);
    }

    public void ScoreUpdate(float deltaScore)
    {
        scoreText.SendMessage("AddScore", deltaScore);
    }


    public void CheckMoleculeName(string gotMoleculeName)
    {
        GameObject player = GameObject.Find("UnityChan2D");

        if (orderedMoleculeName == gotMoleculeName)
        {
            Instantiate(playerParticle, player.transform.position, Quaternion.identity);
            ScoreUpdate(10.0f);
        }
        if (gotMoleculeName == "酸素")
        {
            Instantiate(playerParticle, player.transform.position, Quaternion.identity);
            HpUpdate(0.1f);
        }
    }

    private void SetOrderedMoleculeName()
    {
        int flag = Random.Range(0, 3);
        switch (flag)
        {
            case 0:
                orderedMoleculeName = "水素";
                break;
            case 1:
                orderedMoleculeName = "二酸化炭素";
                break;
            case 2:
                orderedMoleculeName = "メタン";
                break;
        }
        orderText.text = "Need :" + orderedMoleculeName;
    }

    public void GameOver()
    {
        gameOver = true;
        Application.LoadLevel("gameover");
        //結果画面に遷移
    }
}
