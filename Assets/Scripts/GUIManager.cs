using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
       

    // this gets set via unity
    public UnityEngine.UI.Image playerInventory;
    public bool isPlayerInventoryOpen = false;

    public void ShowPlayerInventory(bool value) {
        playerInventory.gameObject.SetActive(value);
        isPlayerInventoryOpen = value;
    }

}
