using UnityEngine;
using System.Collections;

// this script will be used to pick up drops for the user
// it will be attached to the DropCollilder that was added to the player prefab
// isTrigger was set to true in unity against the DropCollider
public class PickupDrops : MonoBehaviour {

    private Inventory connectedInv;
    private ItemDatabase itemDatabase;

    private void Start() {
        // since this is attached to the player, we can get the parent (player) and then get the inventory that is attached to him
        connectedInv = transform.parent.GetComponent<Inventory>();
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
    }

    // check if any other colliders are triggered by the collider we are attached too
    // other will be the collider 
    // this is automatically picked up by Unity
    private void OnTriggerEnter2D(Collider2D other) {
        // 9 is our drop layer, the only thing on layer 9 are drops
        if (other.gameObject.layer == 9) {
            connectedInv.AddItem(other.name, 1);
            GameObject.Destroy(other.gameObject);
        }    
    }
}
