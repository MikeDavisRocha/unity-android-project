using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int score;
    public Text scoreText;
    public GameObject gameStartUI;
    public GameObject gameOverUI;
    public ParticleSystem fireworksVFX;
    public bool isRunning = true;
    bool gameStarted = false;
    public AudioSource soundTrack;
    public Ball ballPrefab;
    public LevelLoader levelLoaderPrefab;
    public Animator gameOverScreen;
    public GameObject youWinScreen;
    [SerializeField] private AudioClip youWinSound;

    private void Awake()
    {
        instance = this;
        Instantiate(ballPrefab.gameObject, new Vector3(0f, -3f, 0f), Quaternion.identity);
        Instantiate(levelLoaderPrefab.gameObject, new Vector3(0f, -3f, 0f), Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRunning)
        {
            if (Input.anyKeyDown)
            {
                isRunning = true;
                Restart();
            }
        }   
    }

    public void GameStart()
    {
        isRunning = true;
        gameStartUI.SetActive(false);
        scoreText.gameObject.SetActive(true);        
        soundTrack.Play();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void TriggerFireworksVFX()
    {
        fireworksVFX.Play();
    }   

    public void ShowVictoryScreen()
    {
        youWinScreen.SetActive(true);
        DelayYouWinSoundAudio();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetTrigger("Start");
        //Time.timeScale = 0;
        //gameOverUI.gameObject.SetActive(true);
    }

    private void DelayYouWinSoundAudio()
    {
        Invoke("PlayYouWinSound", 1.0f);
    }

    private void PlayYouWinSound()
    {
        AudioSource.PlayClipAtPoint(youWinSound, Camera.main.transform.position);
        isRunning = false;
    }
}
