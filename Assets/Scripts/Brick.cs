using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public ParticleSystem DestroyEffect;
    [SerializeField] private AudioClip crackSound;
    public LevelTransitionAnimation levelTransitionAnimation;
    private GameObject theBall;
    public Ball ballPrefab;
    public LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        levelLoader = GameObject.FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Transform containerTransform, Sprite sprite, Color color, int hitpoints)
    {
        this.transform.SetParent(containerTransform);
        //this.sr.sprite = sprite;
        //this.sr.color = color;
        //this.Hitpoints = hitpoints;
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameManager.instance.ScoreUp();
            SpawnBrickEffect();
            AudioSource.PlayClipAtPoint(crackSound, Camera.main.transform.position);
            OnBrickDestroy();
        }
    }

    private void TakeDamage()
    {
    }

    private void SpawnBrickEffect()
    {
        Vector3 position = this.transform.position;
        Vector3 spawnPosition = new Vector3(position.x, position.y, position.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        Destroy(effect, 0.5f);
    }

    private void OnBrickDestroy()
    {
        Destroy(this.gameObject);
        BricksManager.Instance.RemainingBricks.Remove(this);
        if (BricksManager.Instance.RemainingBricks.Count <= 0)
        {
            theBall = GameObject.FindGameObjectWithTag("Ball");
            Destroy(theBall);
            levelLoader.LoadNextLevel();
            Instantiate(ballPrefab.gameObject, new Vector3(0f, -3f, 0f), Quaternion.identity);       
        }
    }
}




