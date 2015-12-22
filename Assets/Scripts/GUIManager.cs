using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
       
    // this gets set via unity
    public Image playerInventory;
    public bool isPlayerInventoryOpen = false;
    public Image[] slots;

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
