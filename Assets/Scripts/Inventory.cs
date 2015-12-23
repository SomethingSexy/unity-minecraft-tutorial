using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public int slotCount = 3 * 9;

    // represent each cell in the inventory
    public List<ItemStack> itemStacks;

    private ItemDatabase itemDatabase;

    private void Start() {
        itemStacks = new List<ItemStack>();
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
    }

    public void AddItem(string name, int count) {
        ItemStack existingStack = FindExistingStack(name, count);

        if (existingStack != null) {
            existingStack.stackSize += count;
        } else {
            // make sure we can add another stack
            if(itemStacks.Count < slotCount) {
                itemStacks.Add(new ItemStack(itemDatabase.FindItem(name), count));
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
                itemStacks.Remove(existingStack);
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
}
