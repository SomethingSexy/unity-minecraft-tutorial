using UnityEngine;
using System.Collections;

// This object will be used to pull in stuff to our player as he walks past
// this will be attached to every drop
public class Magnetism : MonoBehaviour {

    // 
    public Transform target;
    // range of target
    public float range = 2;
    // strength of how fast we pull to the player
    public float strength = 5;

    private void Update() {
        if (InRange()) {
            Attract();
        }
    }

    private bool InRange() {
        // transform is this object, which will be attached to a dropped object, so the distance of the drop
        // check to see if the distance is less than the range
        return Vector3.Distance(transform.position, target.position) <= range;
    }

    private void Attract() {
        // Lerp takes in 2 values 
        // move from the position it is at to the target position (drop to player) over time
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * strength);
    }
}
