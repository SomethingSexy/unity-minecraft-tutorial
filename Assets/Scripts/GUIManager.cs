using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
       
    // this gets set via unity
    public Image playerInventory;
    public bool isPlayerInventoryOpen = false;
    public Image[] slots;

    private Inventory playerInventoryScript;

    private void Update() {
        // since the player is spawned in we need to set this once the player has been created.
        if (playerInventoryScript == null) {
            playerInventoryScript = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        }

        if (isPlayerInventoryOpen) {
            if (Input.GetMouseButtonDown(0)) {
                for (int i = 0; i < slots.Length; i++) {
                    if (isMouseOverSlot(i)) {
                        // check the item stack that matches with the one clicked
                        if (playerInventoryScript.itemStacks[i] != null) {

                        } else {

                        }
                    }
                }
            }

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
