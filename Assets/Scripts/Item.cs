using UnityEngine;
using System.Collections;

[System.Serializable]
// stores information about an individual item
public class Item {
    public string name;
    // icon
    public Sprite sprite;

    public int maxStack = 64;
}

// represents a stack of items in your inventory
public class ItemStack {
    // 
    public Item item;
    public int stackSize;

    public ItemStack(Item item, int stackSize) {
        this.item = item;
        this.stackSize = stackSize;
    }
}
