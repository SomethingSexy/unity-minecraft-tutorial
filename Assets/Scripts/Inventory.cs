using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public int slotCount = 3 * 9;

    // represent each cell in the inventory
    public ItemStack[] itemStacks;

    private ItemDatabase itemDatabase;

    private void Start() {
        itemStacks = new ItemStack[slotCount];
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
    }

    public void AddItem(string name, int count) {
        ItemStack existingStack = FindExistingStack(name, count);

        if (existingStack != null) {
            existingStack.stackSize += count;
        } else {
            int availableSlot = FindFirstAvailableSlot();
            // make sure we can add another stack
            if (availableSlot >= 0) {
                itemStacks[availableSlot] = new ItemStack(itemDatabase.FindItem(name), count);
            } else {
                Debug.LogError("No more room in " + gameObject.name + "'s inventory for " + count + " " + name + "(s)");
            }
        }
    }

    public void RemoveItem(string name, int count) {
        ItemStack existingStack = FindExistingStack(name);

        if (existingStack != null) {
            // don't want to give it 0 items, instead remove the stack
            if (existingStack.stackSize - count >= 1) {
                existingStack.stackSize -= count;
            } else {
                // null out our reference
                existingStack = null;
            }
            
        }
    }

    private ItemStack FindExistingStack(string name) {
        foreach (ItemStack i in itemStacks) {
            if (i.item.name == name) {
                return i;
            }
        }

        return null;
    }
    
    // Will find a stack with a count numnber available
    private ItemStack FindExistingStack(string name, int count) {
        foreach (ItemStack i in itemStacks) {
            // if it is a match and if adding the count to the current stackSize is less than or equal to allowed, then return
            if (i.item.name == name && (i.stackSize + count) <= i.item.maxStack) {
                return i;
            }
        }

        return null;
    }

    private int FindFirstAvailableSlot() {
        for (int i = 0; i <itemStacks.Length; i++) {
            // check to see if we have a stack at this point in our inventory
            if(itemStacks[i] == null) {
                return i;
            }
        }

        // everything is full
        return -1;
    }
}
