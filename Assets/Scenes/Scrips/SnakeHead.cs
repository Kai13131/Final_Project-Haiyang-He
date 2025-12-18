using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SnakeHead : MonoBehaviour
{
    public GameObject snakeBodyPrefabs;
    public GameObject Body1;
    public GameObject Tail;

    //public Sprite coner_Up_Right;
    //public Sprite coner_Up_Left;
    //public Sprite coner_Down_Right;
    //public Sprite coner_Down_Left;

    public float moveTime = 0.2f;
    private float timer;
    public List<GameObject> bodyParts = new List<GameObject>();

    public float distanceBetweenParts = 1f;

    public List<Vector3> positionHistory = new List<Vector3>();
    public List<Vector3> tailPositionHistory = new List<Vector3>();
    public List<Quaternion> rotationHistory = new List<Quaternion>();

    Quaternion up = Quaternion.Euler(0, 0, 180);
    Quaternion down = Quaternion.Euler(0, 0, 0);
    Quaternion left = Quaternion.Euler(0, 0, 270);
    Quaternion right = Quaternion.Euler(0, 0, 90);

    public AudioSource source;
    public AudioClip eatSound;

    public TextMeshProUGUI scoreText;
    private int score = 0;
    public TextMeshProUGUI gameOverText;

    Vector3 inputDir = Vector3.right;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }    

    // Update is called once per frame
    void Update()
    {
        Movement();
        handleWarpAround();
    }
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= moveTime)
        {
            timer = 0f;
            Move();
        }
    }

    void Movement()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && inputDir.y != -1)
        {
            inputDir = Vector3.up;
            transform.rotation = up;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && inputDir.y != 1)
        {
            inputDir = Vector3.down;
            transform.rotation = down;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && inputDir.x != 1)
        {
            inputDir = Vector3.left;
            transform.rotation = left;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && inputDir.x != -1)
        {
            inputDir = Vector3.right;
            transform.rotation = right;
        }
    }

    void handleWarpAround()
    {
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        if (transform.position.y > 5f)
        {
            transform.position = new Vector3(transform.position.x, -5f, 0);
        }
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(transform.position.x, 5f, 0);
        }
    }

    void Move()
    {
        positionHistory.Insert(0, transform.position);
        rotationHistory.Insert(0, transform.rotation);
        if (bodyParts.Count > 0)
        {
            tailPositionHistory.Insert(0, bodyParts[bodyParts.Count - 1].transform.position);
        }
        transform.position = new Vector3(
                    Mathf.Round(transform.position.x) + inputDir.x,
                    Mathf.Round(transform.position.y) + inputDir.y, 0);

        firstBody();
        
        for(int i = 0; i < bodyParts.Count; i++)
        {
            int index = Mathf.Clamp(i + 1, 0, positionHistory.Count - 1);
            bodyParts[i].transform.position = positionHistory[index];
            bodyParts[i].transform.rotation = rotationHistory[index];

        }
    }

    void firstBody()
    {
        int index = Mathf.Clamp(0, 0, positionHistory.Count - 1);
        Body1.transform.position = positionHistory[index];
        Body1.transform.rotation = rotationHistory[index];
    }

    void EatSound()
    {
        source.PlayOneShot(eatSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SnakeBody") 
            || collision.CompareTag("Wall") 
            || collision.CompareTag("Tail"))
        {
            Die();
        }
                
        if (collision.CompareTag("Apple"))
        {
            EatSound();
            updateScore();
            SnakeGrow();
        }
    }

    void updateScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    void Die()
    {
        GameOver();
        Destroy(gameObject);
    }

    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    void SnakeGrow()
    {
        GameObject newBody = Instantiate(snakeBodyPrefabs);
        if(bodyParts.Count > 0)
        {
            newBody.transform.position = bodyParts[bodyParts.Count - 1].transform.position;
        }
        else
        {
            newBody.transform.position = Body1.transform.position;
        }
        bodyParts.Add(newBody.gameObject);
    }

    //void conerPart()
    //{
    //    if(bodyParts.Count > 0)
    //    {
    //        for (int i = 0; i < bodyParts.Count; i++)
    //        {
    //            if (bodyParts[i].transform.rotation == right && bodyParts[i + 1].transform.rotation == up)
    //            {
    //                bodyParts[i].GetComponent<SpriteRenderer>().sprite = coner_Up_Right;
    //                return;
    //            }
    //        }
    //    }
    //    
    //}

    //void moveTail()
    //{
    //    int index = Mathf.Clamp(1, 0, positionHistory.Count - 1);
    //    if(bodyParts.Count <= 0)
    //    {
    //        Tail.transform.position = positionHistory[index];
    //    }
    //    else
    //    {
    //        index = Mathf.Clamp(0, 0, tailPositionHistory.Count - 1);
    //        Tail.transform.position = tailPositionHistory[index];
    //        tailRotation(index);
    //    }
    //}

    //void tailRotation(int index)
    //{
    //    if (inputDir == Vector3.up)
    //    {
    //        rotationHistory[index] = Quaternion.Euler(0,0,0);
    //        Tail.transform.rotation = rotationHistory[index];
    //    }
    //    if (inputDir == Vector3.down)
    //    {
    //        rotationHistory[index] = Quaternion.Euler(0, 0, 180);
    //        Tail.transform.rotation = rotationHistory[index];
    //    }
    //    if (inputDir == Vector3.left)
    //    {
    //        rotationHistory[index] = Quaternion.Euler(0, 0, 90);
    //        Tail.transform.rotation = rotationHistory[index];
    //    }
    //    if (inputDir == Vector3.right)
    //    {
    //        rotationHistory[index] = Quaternion.Euler(0, 0, 270);
    //        Tail.transform.rotation = rotationHistory[index];
    //    }
    //}
}
