using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnakeHead : MonoBehaviour
{
    public GameObject snakeBodyPrefabs;
    public GameObject Body1;
    public GameObject Tail;

    public float moveTime = 0.2f;
    private float timer;
    public List<Transform> bodyParts = new List<Transform>();

    public float distanceBetweenParts = 1f;

    public List<Vector3> positionHistory = new List<Vector3>();
    public List<Vector3> lastBodyPartPositionHistory = new List<Vector3>();

    Vector3 inputDir = Vector3.right;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    

    // Update is called once per frame
    void Update()
    {
        Movement();

        
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
            transform.up = inputDir;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && inputDir.y != 1)
        {
            inputDir = Vector3.down;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && inputDir.x != 1)
        {
            inputDir = Vector3.left;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && inputDir.x != -1)
        {
            inputDir = Vector3.right;
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
        if (bodyParts.Count > 0)
        {
            lastBodyPartPositionHistory.Insert(0, bodyParts[bodyParts.Count - 1].position);
        }
        transform.position = new Vector3(
                    Mathf.Round(transform.position.x) + inputDir.x,
                    Mathf.Round(transform.position.y) + inputDir.y, 0);

        handleWarpAround();

        for(int i = 0; i < bodyParts.Count; i++)
        {
            int index = Mathf.Clamp(i + 1, 0, positionHistory.Count - 1);
            bodyParts[i].position = positionHistory[index];
        }
        firstBody();
        moveTail();
    }

    void firstBody()
    {
        int index = Mathf.Clamp(0, 0, positionHistory.Count - 1);
        Body1.transform.position = positionHistory[index];
    }

    void moveTail()
    {
        int index = Mathf.Clamp(1, 0, positionHistory.Count - 1);
        if(bodyParts.Count <= 0)
        {
            Tail.transform.position = positionHistory[index];
        }
        else
        {
            index = Mathf.Clamp(0, 0, lastBodyPartPositionHistory.Count - 1);
            Tail.transform.position = lastBodyPartPositionHistory[index];
        }
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
            SnakeGrow();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void GameOver()
    {
        
    }

    void SnakeGrow()
    {
        GameObject newBody = Instantiate(snakeBodyPrefabs);
        if(bodyParts.Count > 0)
        {
            newBody.transform.position = bodyParts[bodyParts.Count - 1].position;
        }
        else
        {
            newBody.transform.position = Body1.transform.position;
        }
        bodyParts.Add(newBody.transform);
    }
}
