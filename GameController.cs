using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject player;
    public Transform SpawnPoint;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    private int score;
    public Text restartText;
    public Text gameOverText;
    public Text LivesText;
    private int lives;

    private bool gameOver;
    private bool restart;
    private bool fullrestart;
    private bool win;
    private bool victory;
    private bool respawn;

    public BG_Scroller background;
    public int bgspeed;
    public StarField stars;
    public int starspeed;
    public FarStarField farstars;
    public int starfast;

    public AudioSource musicSource;
    public AudioClip winMusic;
    public AudioClip lossMusic;

    void Start()
    {
        win = false;
        victory = false;
        gameOver = false;
        restart = false;
        respawn = false;
        fullrestart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        lives = 3;
        UpdateScore();
        UpdateLives();
        StartCoroutine (SpawnWaves ());

        GameObject backgroundObject = GameObject.FindWithTag("Background");
        if (backgroundObject != null)
        {
            background = backgroundObject.GetComponent<BG_Scroller>();
        }

        GameObject starsObject = GameObject.FindWithTag("StarField");
        if (starsObject != null)
        {
            stars = starsObject.GetComponent<StarField>();
        }

        GameObject farstarsObject = GameObject.FindWithTag("FarStarField");
        if (farstarsObject != null)
        {
            farstars = farstarsObject.GetComponent<FarStarField>();
        }
    }

    void Update ()
    {
        
        if (respawn)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                Instantiate(player, SpawnPoint.position, SpawnPoint.rotation);
                restartText.text = "";
                restart = false;
                respawn = false;
                StartCoroutine(SpawnWaves());
            }
        }

        if (fullrestart)
        {
            if (victory)
            {
                background.Speed(bgspeed);
                stars.Speed(starspeed);
                farstars.Speed(starfast);
            }

            if (Input.GetKeyDown (KeyCode.T))
            {
                SceneManager.LoadScene("Final Project WIP"); // or whatever the name of your scene is
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'T' for Restart";
                fullrestart = true;
                gameOverText.text = "Game Over! Game Created by Gavin McAllister!";
                Audio(lossMusic);
                break;
            }

            if (win)
            {
                restartText.text = "Press 'T' for Restart";
                fullrestart = true;
                gameOverText.text = "You win! Game Created by Gavin McAllister!";
                Audio(winMusic);
                victory = true;
                break;
            }

            if (restart)
            {
                restartText.text = "Press 'R' for Next Life";
                respawn = true;
                break;
            }
        }
    }

    public void Audio(AudioClip music)
    {
        musicSource.Stop();
        musicSource.loop = false;
        musicSource.clip = music;
        musicSource.Play();
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;

        if (score >= 100)
        {
            win = true;
          //  fullrestart = true;
        }
    }

    public void AddLives(int newLivesValue)
    {
        lives += newLivesValue;
        UpdateLives();
    }

    public void SubtractLives ()
    {
        lives = lives - 1;
        LivesText.text = lives.ToString();

        if (lives > 0)
        {
            restart = true;
        }

        UpdateLives();
    }

    void UpdateLives()
    {
        LivesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            gameOver = true;
        }
    }
}
