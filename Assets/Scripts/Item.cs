using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
    public string name;
    // icon
    public Sprite sprite;

    public int maxStack = 64;
}

public class ItemStack {
    public Item item;
    public int stackSize;

    public ItemStack(Item item, int stackSize) {
        this.item = item;
        this.stackSize = stackSize;
    }
}
