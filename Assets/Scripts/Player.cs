using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int direction = 0;
    public KeyCode leftKey, rightKey, jumpKey;
    public float moveSpeed = 3f;
    public float jumpForce = 250f;

    private Rigidbody2D body;

    private void Start() {
        // this will grab the RigidBody2D component that we added to the player game object in unity
        body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        UpdateControls();
        UpdateMovement();
    }

    private void UpdateControls() {
        // movement controls
        // GetKey returns every frame while the key is held down
        if (Input.GetKey(leftKey)) {
            // -1 is going to represent left;
            direction = -1;
        } else if (Input.GetKey(rightKey)) {
            direction = 1;
        } else {
            direction = 0;
        }

        // jump controls
        // GetKeyDown gets called once when you push it
        if (Input.GetKeyDown(jumpKey)) {
            Jump();
        }
    }

    private void UpdateMovement() {
        // set the velocity to the current velocity for the y, meaning it won't be affected right now
        // when we move left and right we only want to move to the left and right, not up and down
        // moveSpeed * 1 = 3 which means go right, moveSpeed * -1 = -3 which means go left, zero means don't move
        body.velocity = new Vector2(moveSpeed * direction, body.velocity.y);
    }

    private void Jump() {
        // propell him up
        body.AddForce(transform.up * jumpForce);
    }
}
