using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public GameObject snakeBodyPrefabs;
    public List<Transform> bodyParts = new List<Transform>();

    public float distanceBetweenParts = 1f;

    public List<Vector3> positionHistory = new List<Vector3>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        positionHistory.Insert(0, transform.position);

        for (int i = 0;  i < bodyParts.Count; i++)
        {
            int index = Mathf.Min((int)((i + 1) * distanceBetweenParts * 100), positionHistory.Count - 1);
            Vector3 targetPosition = positionHistory[index * 20];
            bodyParts[i].position = targetPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.gameObject.CompareTag("SnakeBody"))
        {
            if(bodyParts.Count > 5)
            {
                //Die();
            }
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
        if(positionHistory.Count > 0)
        {
            newPart.transform.position = positionHistory[positionHistory.Count - 1];
        }
        else
        {
            newPart.transform.position = transform.position;
        }
        bodyParts.Add(newPart.transform);
    }
}
