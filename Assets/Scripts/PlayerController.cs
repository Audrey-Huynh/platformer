using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    public int speed = 2;
    public float jumpForce = 12.0f;

    float horizontal = 0;
    float vertical = 0;

    Rigidbody rb;

    bool jumping = false;

    //this score variable is just for logs, not the actual thing being updated (the score displayed on screen is through UpdateScore() and GameManager.cs)
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();
        Debug.Log("Oh no, enemies have infiltrated and taken over your sandcastle! It's up to you to reclaim your castle and treasure!");
        Debug.Log("You can use your water gun to shoot at the enemies. The enemies have water guns of their own, so be careful and dodge their bullets.");
        Debug.Log("Collect as many coins as you can for a high score. Good luck!");
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumping = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BeachBall"))
        {
            Debug.Log("Good kick!");
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            jumping = false;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            score--;
            Debug.Log("Oh no! The enemy has stolen one of your coins! You currently have a total of " + score + " coin(s).");

            gameManager.UpdateScore(-1, false);
        }
        if (other.gameObject.CompareTag("Sandcastle"))
        {
            if (score >= 10)
            {
                Debug.Log("Congratulations, you have reclaimed your sandcastle! Wow, you have a high score of " + score + " coins! (Click the run button twice if you would like to restart the game and play again.)");
            }
            if (score < 10 && score > 5)
            {
                Debug.Log("Congratulations, you have reclaimed your sandcastle! You have a total score of " + score + " coins! (Click the run button twice if you would like to restart the game and play again.)");
            }
            if (score <= 5)
            {
                Debug.Log("Congratulations, you have reclaimed your sandcastle! You have a total score of " + score + " coin(s). Try to aim for a higher score next time! (Click the run button twice if you would like to restart the game and play again.)");
            }

            gameManager.UpdateScore(0, true);
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(horizontal*speed, rb.velocity.y, vertical*speed);
        rb.velocity = moveDirection;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            score++;
            Debug.Log("You currently have a total of " + score + " coin(s)! :)");

            gameManager.UpdateScore(1, false);
        }
        if (other.gameObject.CompareTag("Ocean"))
        {
            score = 0;
            Debug.Log("Uh-oh! You fell into the ocean and lost all of your coins! Your score is now " + score + ". (Click the run button twice if you would like to restart the game and play again.)");

            gameManager.UpdateScore(-100, false);
        }
    }

}
