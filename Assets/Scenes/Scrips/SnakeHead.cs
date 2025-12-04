using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public GameObject snakeBodyPrefabs;
    public List<Transform> bodyParts = new List<Transform>();

    public float speed = 5.0f;
    public float distanceBetweenParts = 1f;

    public List<Vector3> positionHistory = new List<Vector3>();

    bool canHitHead = false;
    Vector3 nextPosition = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        positionHistory.Insert(0, transform.position);
        
        for(int i = 0;  i < bodyParts.Count; i++)
        {
            Vector3 targetPosition = positionHistory[(int)((i + 1) * distanceBetweenParts * 50)];
            bodyParts[i].position = targetPosition;
            nextPosition = bodyParts[i].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < positionHistory.Count; i++)
        {
            if (positionHistory[i] == nextPosition)
            {
                canHitHead = true;
            }
            else
            {
                canHitHead = false;
            }
        }
        
        if (!canHitHead && collision.gameObject.CompareTag("SnakeBody"))
        {
            //Die();
        }
        
        
        if (collision.gameObject.CompareTag("Apple"))
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
        GameObject newPart = Instantiate(snakeBodyPrefabs);
        newPart.transform.position = transform.position;
        bodyParts.Add(newPart.transform);
    }
}
