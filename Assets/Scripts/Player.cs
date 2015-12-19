using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int direction = 0;
    public KeyCode leftKey, rightKey, jumpKey;
    public float moveSpeed = 3f;
    public float jumpForce = 250f;

    private Rigidbody2D body;
    private Animator anim;
    private GUIManager guiManager;

    private void Start() {
        // this will grab the RigidBody2D component that we added to the player game object in unity
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        guiManager = GameObject.Find("GUI").GetComponent<GUIManager>();
    }

    // called each frame
    private void Update() {
        UpdateControls();
        UpdateMovement();
        UpdateBreaking();
    }

    private void UpdateControls() {
        // movement controls
        // GetKey returns every frame while the key is held down
        if (Input.GetKey(leftKey)) {
            // -1 is going to represent left;
            FaceDirection(-1);
            anim.SetBool("isWalking", true);
        } else if (Input.GetKey(rightKey)) {
            // right
            FaceDirection(1);
            anim.SetBool("isWalking", true);
        } else {
            FaceDirection(0);
            anim.SetBool("isWalking", false);
        }

        // jump controls
        // GetKeyDown gets called once when you push it
        if (Input.GetKeyDown(jumpKey)) {
            Jump();
        }

        // Inventory
        if (Input.GetKeyDown(KeyCode.E)) {
            if (guiManager.isPlayerInventoryOpen) {
                guiManager.ShowPlayerInventory(false);
            } else {
                guiManager.ShowPlayerInventory(true);
            }
        }
    }

    private void FaceDirection(int dir) {
        if (dir != direction) {
            // set the direction if it hasn't changed
            direction = dir;

            // now switch between sprites to animate the movement
            // this is steve facing front
            if (dir == 0) {
                // given our tranform component on our gane object find a child gameobject 
                // this will toggle to make sure we are using the front 
                transform.FindChild("Side").gameObject.SetActive(false);
                transform.FindChild("Front").gameObject.SetActive(true);
            } else {
                // this is steve facing side
                transform.FindChild("Side").gameObject.SetActive(true);
                transform.FindChild("Front").gameObject.SetActive(false);
                // this will toggle the direction of the side from facing left or right using the same sprite automatically
                transform.FindChild("Side").localScale = new Vector3(dir * -1, 1, 1);
            }
        }
        
    }

    private void UpdateMovement() {
        // set the velocity to the current velocity for the y, meaning it won't be affected right now
        // when we move left and right we only want to move to the left and right, not up and down
        // moveSpeed * 1 = 3 which means go right, moveSpeed * -1 = -3 which means go left, zero means don't move
        body.velocity = new Vector2(moveSpeed * direction, body.velocity.y);
    }

    // check to see if we are going to break a block
    private void UpdateBreaking() {
        // 0 is the left mouse button
        if (Input.GetMouseButtonDown(0)) {
            // pixel coordinates of the mouse are different than the world position
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ray cast, will check between two positions and see if anything is blocking it
            RaycastHit2D hit = Physics2D.Raycast(pos, transform.position);

            // if there is a collider on the gameobject that is hit
            if (hit.collider != null) {
                if (hit.collider.gameObject.tag == "Block") {
                    GameObject.Find("World").GetComponent<WorldGen>().DestroyBlock(hit.collider.gameObject);
                }
            }
        }
    }

    private void Jump() {
        // propell him up
        body.AddForce(transform.up * jumpForce);
    }
}
