using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float shootRadius = 15f;
    public float timeBetweenShots = 1f;
    public GameObject enemyBullet;
    public GameObject player;

    float shotTime = 0;
    
    public GameObject coin;

    int enemies = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Shoot();
    }

    void Shoot()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < shootRadius)
        {
            shotTime += Time.deltaTime;
            if (shotTime >= timeBetweenShots)
            {
                Instantiate(enemyBullet, transform.position, transform.rotation);
                shotTime = 0;
            }
        }
    }

    void Turn()
    {
        transform.LookAt(player.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            DestroyEnemy();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BeachBall"))
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Instantiate(coin, transform.position, transform.rotation);
        Destroy(gameObject);
        enemies--;
        Debug.Log("Great job! You have defeated an enemy! Total number of enemies defeated: " + enemies);
        if (enemies <= 0)
        {
            Debug.Log("Great job! You have defeated all of the enemies! Total number of enemies defeated: " + enemies);
        }
    }

}
