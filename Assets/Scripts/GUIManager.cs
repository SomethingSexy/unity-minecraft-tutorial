using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
       
    // this gets set via unity
    public Image playerInventory;
    public bool isPlayerInventoryOpen = false;
    public Image[] slots;
    public GameObject slotPrefab;

    // stack of items that we have picked up, going to remove it from the inventory and temporarily store it in this variable
    private ItemStack cursorStack;
    // 
    private GameObject cursorIcon;
    private Inventory playerInventoryScript;

    private void Start() {
        cursorIcon = GameObject.Instantiate(slotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        // set the parent of the cursorIcon to this gameobject, which is the GUI
        cursorIcon.transform.SetParent(gameObject.transform);
    }

    private void Update() {
        // since the player is spawned in we need to set this once the player has been created.
        if (playerInventoryScript == null) {
            playerInventoryScript = GameObject.FindWithTag("Player").GetComponent<Inventory>();
            // playerInventoryScript.AddItem("Balze Rod", 25);
            // playerInventoryScript.AddItem("Diamond Axe", 1);
        }
        
        // render whatever might be set in our cursorStack, as in whatever the user has selected from the inventory
        RenderCursorStack();

        if (isPlayerInventoryOpen) {
            if (Input.GetMouseButtonDown(0)) {
                for (int i = 0; i < slots.Length; i++) {
                    if (isMouseOverSlot(i)) {
                        // if we have pressed the mouse on a slot
                        // and we haven't selected anything yet
                        if (cursorStack == null) {
                            // check the item stack that matches with the one clicked
                            if (playerInventoryScript.itemStacks[i] != null) {
                                // whatever item stack we are on, grab it and put it in the cursorStack cache
                                cursorStack = playerInventoryScript.itemStacks[i];
                                // now remove it from our item stack
                                playerInventoryScript.itemStacks[i] = null;
                            } else {

                            }
                        } else { 
                            // we already have an item selected, put that down to the selected slot
                            // now remove it from our item stack
                            playerInventoryScript.itemStacks[i] = cursorStack;
                            // whatever item stack we are on, grab it and put it in the cursorStack cache
                            cursorStack = null;
                        }
                    }
                }
            }
            // if the inventory is open, render our slots
            RenderSlots();
        }
    }

    private void RenderSlots() {
        // loop through all of the slots we have for our inventory
        for (int i = 0; i < slots.Length; i++) {

            // check to see if something exists here
            ItemStack itemStack = playerInventoryScript.itemStacks[i];
            
            // check to see if there is a stack of items in that slot
            if (itemStack != null) {
                // color a is the alpha channel, which is opacity
                // if the slot is invisible, then we know there is no item shown
                if (slots[i].color.a != 1) {
                    // set it to white and alpha to 1
                    slots[i].color = new Color(1, 1, 1, 1);
                    slots[i].sprite = itemStack.item.sprite;
                    // find the slot, grab the first child which is the SlotText, then grab the Text component on that GameObject and set the text
                    slots[i].transform.GetChild(0).GetComponent<Text>().text = itemStack.stackSize.ToString();
                }
            } else {
                if (slots[i].color.a != 0) {
                    // remove the slot if it doesn't exist anymore
                    slots[i].color = new Color(1, 1, 1, 0);
                    slots[i].sprite = null;

                    // remove text when hiding
                    slots[i].transform.GetChild(0).GetComponent<Text>().text = "";
                }
            }
        }
    }

    private void RenderCursorStack() {
        if (cursorStack != null) {
            // this will set our cursorIcon position to the position of our mouse
            cursorIcon.transform.position = Input.mousePosition;

            // if it isn't visible but we have something selected
            if (cursorIcon.GetComponent<Image>().color.a != 1) {
                // make the icon visible
                cursorIcon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                // set the stack item that we have picked up to the cursor image
                cursorIcon.GetComponent<Image>().sprite = cursorStack.item.sprite;
                // set the text of how m any 
                cursorIcon.transform.GetChild(0).GetComponent<Text>().text = cursorStack.stackSize.ToString();
            }
        } else {
            // if we don't have a cursor and it is not hidden, hide it
            if (cursorIcon.GetComponent<Image>().color.a != 0) {
                // make the icon visible
                cursorIcon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                // set the stack item that we have picked up to the cursor image
                cursorIcon.GetComponent<Image>().sprite = null;
                // set the text of how m any 
                cursorIcon.transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }
    }

    public void ShowPlayerInventory(bool value) {
        playerInventory.gameObject.SetActive(value);
        isPlayerInventoryOpen = value;
    }

    public bool isMouseOverSlot(int slotIndex) {
        RectTransform rt = slots[slotIndex].GetComponent<RectTransform>();

        // this will check to see if our mouse is to the right of the left side of the slot and to the left of the right side of the slow, so in between the x edges
        if (Input.mousePosition.x > rt.position.x - rt.sizeDelta.x * 1.5f && Input.mousePosition.x < rt.position.x + rt.sizeDelta.x * 1.5f) {
            // checks the same but for the y (top, bottom) of the slot
            if (Input.mousePosition.y > rt.position.y - rt.sizeDelta.y * 1.5f && Input.mousePosition.y < rt.position.y + rt.sizeDelta.y * 1.5f) {
                return true;
            }
        }

        return false;
    }

}
