using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public GameObject snakeHead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        snakeHead.GetComponent<SnakeMovment>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirction = gameObject.transform.position - snakeHead.transform.position;

    }
}
