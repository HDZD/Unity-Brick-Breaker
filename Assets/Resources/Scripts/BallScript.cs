using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    Rigidbody2D rigidBody;
    Vector3 stageDimensions;

    void Start() {
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
    }

    void OnEnable() {
        ResetPosition();
    }

    void ResetPosition () {
        //Set the ball at a fixed position with a random x direction, but fixed y direction (upwards)
        if (rigidBody == null) rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.zero;
        transform.position = new Vector3(0,-2.7f,0);
        float x = 0;
        while (x == 0) x = Random.Range(-5,6);
        rigidBody.AddForce(new Vector3(x,5,0) * 50);
    }

    void FixedUpdate (){
        if (transform.position.x < -stageDimensions.x || transform.position.x > stageDimensions.x || transform.position.y < -stageDimensions.y || transform.position.y > stageDimensions.y)
            gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col){
        //Speed up the ball just slightly everytime it hits the paddle (for extra challenge)
        if (col.gameObject.name == "Paddle")
            rigidBody.AddForce(Vector3.Normalize(rigidBody.velocity)*5);
        
        //Ball has fallen and needs to be deactivated
        if (col.gameObject.name == "LowerBound"){
            gameObject.SetActive(false);
            BallPooler.ballCount--;
        }

        //Ball has hit a brick and needs to destroy it
        if (col.gameObject.tag == "Brick"){
            col.gameObject.SetActive(false);
            GameManager.KillBrick();
        }

        //Reset the ball if it is stuck in a loop (hitting objects completely perpendicularly)
        if (Vector2.Angle(rigidBody.velocity,Vector2.up) < 3 || Vector2.Angle(rigidBody.velocity,Vector2.right) < 3)
            ResetPosition();
    }
}
