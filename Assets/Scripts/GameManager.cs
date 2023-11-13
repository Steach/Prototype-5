using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    private int score;
    private int lives;
    private bool isGameActive;
    public Button restarButton;
    public GameObject titleScreen;
    private AudioSource targetAudio;
    public AudioClip goodTargets;
    public AudioClip badTargets;
    public Slider volumeSlider;
    public GameObject pausedScreen;
    private bool paused;
    

    // Start is called before the first frame update
    void Start()
    {
        targetAudio = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CheckPaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int indexTarget = Random.Range(0, targets.Count);
            Instantiate(targets[indexTarget]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    void SetLives(int livesInStart)
    {
        lives = livesInStart;
        livesText.text = "Lives: " + lives;
    }
    public void UpdateLives(int livesCounter)
    {
        if (lives > 0)
        {
            lives -= livesCounter;
        }
        livesText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        restarButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        gameOverText.gameObject.SetActive(false);
        restarButton.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
        volumeSlider.gameObject.SetActive(false);
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;
        lives = 3;
        SetLives(3);
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    void CheckPaused()
    {
        if(!paused)
        {
            paused = true;
            pausedScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pausedScreen.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void PlayGoodTarget()
    {
        targetAudio.PlayOneShot(goodTargets, 1.0f);
    }

    public void PlayBadTarget()
    {
        targetAudio.PlayOneShot(badTargets, 1.0f);
    }

    public bool ReturnPaused()
    {
        return paused;
    }

    public bool ReturnGameActive()
    {
        return isGameActive;
    }
}
