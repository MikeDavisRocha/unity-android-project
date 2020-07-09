using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    float bounceForce;
    [SerializeField]
    float randomFactor = 0.5f;
    public Animator ballHitAnimation;
    private AudioSource ballSound;
    private bool gameStarted;
    [SerializeField] private AudioClip gameOverSound;    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        
        ballSound = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update() {
        if (!gameStarted && GameManager.instance.isRunning)
        {
            StartBounce();
        }
    }

    public void StartBounce()
    {
        if (Input.anyKeyDown)
        {
            this.gameObject.SetActive(true);
            GameManager.instance.GameStart();
            gameStarted = true;
            rb.velocity = new Vector2(Random.Range(1f, 5f), Random.Range(10f, 15f));
        }
    }    



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ballHitAnimation.SetTrigger("Hit");
        //cameraTransform.DOShakePosition(1f, 1f, 10, 25, true);

        if (collision.gameObject.tag != "Brick")
        {
            ballSound.Play();
        }
        if (collision.gameObject.tag == "Brick")
        {
            rb.velocity = new Vector2(Random.Range(1.5f, 2f), Random.Range(10f, 12f));
        }
        if (collision.gameObject.tag == "FallCheck")
        {
            GameManager.instance.ShowGameOverScreen();
            DelayGameOverSoundAudio();
            ballSound.PlayDelayed(3f);
            this.gameObject.SetActive(false);
            GameManager.instance.isRunning = false;
        }
        else if (collision.gameObject.tag == "Paddle")
        {
            GameManager.instance.TriggerFireworksVFX();
        }        
    }

    private void DelayGameOverSoundAudio()
    {
        Invoke("PlayGameOverSound", 1.0f);
    }

    private void PlayGameOverSound()
    {
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
    }
}
