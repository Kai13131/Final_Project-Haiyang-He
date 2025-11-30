using UnityEngine;

public class AppleSpawn : MonoBehaviour
{   
    public GameObject applePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake"))
        {
            SpawnApples();
            Destroy(applePrefab); 
        }
    }

    void SpawnApples()
    {
        int randomX = Random.Range(-8, 8);
        int randomY = Random.Range(-4, 4);
        Instantiate(applePrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
    }

}
