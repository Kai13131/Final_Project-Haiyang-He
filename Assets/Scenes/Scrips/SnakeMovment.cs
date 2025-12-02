using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SnakeMovment : MonoBehaviour
{
    public float speed = 10f;
    public GameObject snakeBodyPrefabs;
    public List<GameObject> snakeParts = new List<GameObject>();
    Vector3 inputDir = new Vector3(1, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if(inputDir.y != -1)
            {
                inputDir = new Vector3(0, 1, 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if(inputDir.y != 1)
            {
                inputDir = new Vector3(0, -1, 0);
            }     
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if(inputDir.x != 1)
            {
                inputDir = new Vector3(-1, 0, 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if(inputDir.x != -1)
            {
                inputDir = new Vector3(1, 0, 0);
            }
        }

        transform.position += inputDir * speed * Time.deltaTime;

        if(transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        if (transform.position.y > 5f)
        {
            transform.position = new Vector3(transform.position.x, -5f,  0);
        }
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(transform.position.x, 5f,  0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SnakeBody"))
        {
            Die();
        }

        if(collision.gameObject.CompareTag("Apple"))
        {
            SnakeGrow();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void SnakeGrow()
    {
        GameObject newPart;
        Vector3 newPartPosition = -inputDir + transform.position;
        newPart = Instantiate(snakeBodyPrefabs, newPartPosition, Quaternion.identity);
        snakeParts.Add(newPart);
    }
}
