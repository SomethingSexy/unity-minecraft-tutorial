using UnityEngine;
using System.Collections;

[System.Serializable]
public class Block {
    public string displayName;

    public byte id;

    public Sprite sprite;

    public bool isSolid = true;

    public Drop[] drops;
}
