using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public GameObject snakeHead;
    public float speed = 3.0f;
    public float mag = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, snakeHead.transform.position, mag * Time.deltaTime);
    }
}
