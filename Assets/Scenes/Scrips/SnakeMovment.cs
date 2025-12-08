using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class SnakeMovment : MonoBehaviour
{
    public float moveTime = 0.2f;
    private float timer;

    Vector3 inputDir = new Vector3(1, 0, 0);

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
            inputDir = new Vector3(0, 1, 0);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && inputDir.y != 1)
        {
            inputDir = new Vector3(0, -1, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && inputDir.x != 1)
        {
            inputDir = new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && inputDir.x != -1)
        {
            inputDir = new Vector3(1, 0, 0);
        }

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
        transform.position = new Vector3(Mathf.Round(transform.position.x) + inputDir.x,Mathf.Round(transform.position.y) + inputDir.y,0);
    }
}
