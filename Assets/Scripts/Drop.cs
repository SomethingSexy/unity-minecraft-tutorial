using UnityEngine;
using System.Collections;

[System.Serializable]
public class Drop {

    // whatever item we want this drop to be dropping
    public string itemName;

    // chance of it actually dropping, most will be 1
    [Range(0.0f, 1.0f)]
    public float dropChance;

    public bool DropChanceSuccess() {
        // if random is less than or equal to dropChance, that we will drop it
        return Random.value <= dropChance;
    }

}
