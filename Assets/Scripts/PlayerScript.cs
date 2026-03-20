//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //These are the player's Variables, the raw info that defines them
    
    //The Rigidbody2D is a component that gives the player physics, and is what we use to move
    public Rigidbody2D RB;

    //TextMeshPro is a component that draws text on the screen.
    //We use this one to show our score.
    public TextMeshPro ScoreText;
    //This will control how fast the player moves
    public float Speed = 10;
    
    //This is how many points we currently have
    public int Score = 0;

    public TextMeshPro HealthText;

    public int Health = 3;

    public CoinScript CoinPrefab;

    public poweruphealth_script poweruphealthPrefab;

    public PlayerScript P;



    //Start automatically gets triggered once when the objects turns on/the game starts
    void Start()
    {
        //During setup we call UpdateScore to make sure our score text looks correct
        UpdateScore();
        UpdateHealth();
    }

    //Update is a lot like Start, but it automatically gets triggered once per frame
    //Most of an object's code will be called from Update--it controls things that happen in real time
    void Update()
    {
        //The code below controls the character's movement
        //First we make a variable that we'll use to record how we want to move
        Vector2 vel = new Vector2(0,0);
        
        //Then we use if statement to figure out what that variable should look like
        
        //If I hold the right arrow key, the player should move right. . .
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = Speed;
        }
        //If I hold the left arrow, the player should move left. . .
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -Speed;
        }
        //If I hold the up arrow, the player should move up. . .
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vel.y = Speed;
        }
        //If I hold the down arrow, the player should move down. . .
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vel.y = -Speed;
        }
        
        //Finally, I take that variable and I feed it to the component in charge of movement
        RB.linearVelocity = vel;

        if (transform.position.x < -9.5f)
        {
            Vector3 pos = transform.position;
            pos.x = 9.5f;
            transform.position = pos;
        }
        if (transform.position.x > 9.5f)
        {
            Vector3 pos = transform.position;
            pos.x = -9.5f;
            transform.position = pos;
        }
        if (transform.position.y < -5.5f)
        {
            Vector3 pos = transform.position;
            pos.y = 5.5f;
            transform.position = pos;
        }
        if (transform.position.y > 5.5f)
        {
            Vector3 pos = transform.position;
            pos.y = -5.5f;
            transform.position = pos;
        }
     }

    //This gets called whenever you bump into another object, like a wall or coin.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //This checks to see if the thing you bumped into had the Hazard tag
        //If it does...
        if (other.gameObject.CompareTag("Hazard"))
        {
            //Run your 'you lose' function!
            Health-- ;
            UpdateHealth();
        }

        if (Health == 0)
        {
            Die();
        }
        
        //This checks to see if the thing you bumped into has the CoinScript script on it
        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        //If it does, run the code block belows
        if (coin != null)
        {
            //Tell the coin that you bumped into them so they can self destruct or whatever
            coin.GetBumped();
            Vector3 pos = new Vector3();
            pos.x = Random.Range(-8, 8f);
            pos.y = Random.Range(-4, 4f);
            Instantiate(CoinPrefab,pos,Quaternion.identity);
            //Make your score variable go up by one. . .
            Score++;
            //And then update the game's score text
            UpdateScore();
        }
        poweruphealth_script poweruphealth = other.gameObject.GetComponent<poweruphealth_script>();
        if (poweruphealth != null)
        {
            poweruphealth.GetBumped();
            P.Health += 1;
            if(Health > 3)
            {
              Health = 3;
            }
        }
    }


    public void UpdateHealth()
    {
        HealthText.text = "Health: " + Health;
    }

    //This function updates the game's score text to show how many points you have
    //Even if your 'score' variable goes up, if you don't update the text the player doesn't know
    public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score;
    }

    //If this function is called, the player character dies. The game goes to a 'Game Over' screen.
    public void Die()
    {
        SceneManager.LoadScene("Game Over");
    }
}
