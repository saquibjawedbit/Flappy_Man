using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private bool gameStart = false;

    [SerializeField] private List<GameObject> platform_;
    [SerializeField] private List<GameObject> enemy_;

    [SerializeField] private Transform startPos;
    [SerializeField] private Transform parentPos;
    [SerializeField] private Transform movPos;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameText;
    [SerializeField] private float enemyrate = 5f;

    [SerializeField] private GameObject text;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject controller;

    [SerializeField] private chaser chase;

    private float score;

    public static GameManager instance;
  

    private bool generating = false;
    private bool genratingEnemy = false;
    private bool enble = true;

    [SerializeField] private float minFactor = 2f;
    [SerializeField] private float maxFactor = 2.5f;
    [SerializeField] private Animator anim;

    private const string gamePrefix = "Score: ";
    private const string mainPrefix = "High Score: ";

    public AudioSource source;

    private int highScore = 0;

    void Awake()
    {
        if (instance == null) instance = this;
        highScore = int.Parse(PlayerPrefs.GetString("SCORE", "0000"));
        scoreText.text = mainPrefix + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStart)
        {
            gameStart = true; text.SetActive(false);
        }
        if (!gameStart) return;

        movPos.Translate(transform.right * -speed * Time.deltaTime);
        if (!generating) StartCoroutine(Generate());
        if (!genratingEnemy) StartCoroutine(GenerateEnemy());
    }

    private void LateUpdate()
    {
        if (!gameStart) return;
        score += Time.deltaTime;
        scoreText.text = gamePrefix + Mathf.RoundToInt(score).ToString();
    }


    public void GameOver()
    {
        gameStart = false;
        anim.SetTrigger("death");
        gameOverUI.SetActive(true);
        gameText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        if (highScore < score) PlayerPrefs.SetString("SCORE", Mathf.RoundToInt(score).ToString());
        scoreText.text = mainPrefix + highScore.ToString();
        controller.SetActive(false);
    }

    public void Restart()
    {
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator GenerateEnemy()
    {
        if (!genratingEnemy) yield return null;
        genratingEnemy = true;
        yield return new WaitForSeconds(Random.Range(enemyrate, enemyrate + 5f));
        Instantiate(enemy_[Random.Range(0, enemy_.Count)] ,startPos.position + Vector3.up * 2, Quaternion.identity, parentPos);
        genratingEnemy = false;
    }

    IEnumerator Generate()
    {
        if (generating) yield return null;
        generating = true;
        yield return new WaitForSeconds(Random.Range(minFactor, maxFactor));
        int index = Random.Range(0, platform_.Count);
        Instantiate(platform_[index], startPos.position + transform.up * Random.Range(-1.2f, 1.2f), Quaternion.identity, parentPos);
        generating = false;
    }

    public void UserReward()
    {
        anim.SetTrigger("Run");
        chase.ResetEnemy();
        controller.SetActive(true);
        gameOverUI.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // print(collision.name);
        Destroy(collision.gameObject);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Sound()
    {
        //Sound Functionality
        source.enabled = !enble;
        enble = !enble;

    }

    public void close()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

}
