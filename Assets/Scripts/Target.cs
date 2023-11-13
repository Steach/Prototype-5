using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 6;
    private float ySpawnPos = -3;
    private float randomTorque;
    private float randomSpeed;
    private float randonPosition;
    private GameManager gameManager;
    public int scorePoint;
    public ParticleSystem explosionParticle;
    private bool pausedGame;
    private bool activeGame;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRB = GetComponent<Rigidbody>();

        RandomPosition();
        targetRB.AddForce(RandomRange(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        
        RandomSpownPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnMouseDown() 
    {
        GetPaused();
        if (!pausedGame)
        {
             if (gameObject.CompareTag("Bad"))
            {
                gameManager.PlayBadTarget();
            }
            else
            {
                gameManager.PlayGoodTarget();
            }
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(scorePoint);
        }
    }*/

    void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(1);
            //gameManager.GameOver();
        }
    }

    public void DestroyTarget()
    {
        GetPaused();
        SetActiveGame();
        if (!pausedGame)
        {
            if (activeGame)
            {
                Destroy(gameObject);
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                gameManager.UpdateScore(scorePoint);
            }
        }
    }

    float RandomPosition()
    {
        randonPosition = Random.Range(-xRange, xRange);
        return randonPosition;
    }

    Vector3 RandomSpownPosition()
    {
        transform.position = new Vector3(randonPosition, ySpawnPos);
        return transform.position;
    }
    Vector3 RandomRange()
    {
        randomSpeed = Random.Range(minSpeed, maxSpeed);
        return Vector3.up * randomSpeed;
    }

    float RandomTorque()
    {
        randomTorque = Random.Range(-maxTorque, maxTorque);
        return randomTorque;
    }

    void GetPaused()
    {
        pausedGame = gameManager.ReturnPaused();
    }

    void SetActiveGame()
    {
        activeGame = gameManager.ReturnGameActive();
    }
}
