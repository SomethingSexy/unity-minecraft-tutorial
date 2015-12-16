using UnityEngine;
using System.Collections;

public class WorldGen : MonoBehaviour {

    private BlockManager blockManager;

    private int width = 64;
    private int height = 128;

    private Block[,] blocks;

    private void Start() {
        blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        blocks = new Block[width, height];

        GenerateBlocks();
        SpawnBlocks();
    }

    private void GenerateBlocks() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                // for each location in the game, set a block
                blocks[x, y] = blockManager.FindBlock(2);
            }
        }
    }

    private void SpawnBlocks() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                // grab the block set for this location
                Block block = blocks[x, y];

                // create a new game object
                GameObject blockGO = new GameObject();

                // add a sprite renderer component to our new game object
                SpriteRenderer sr = blockGO.AddComponent<SpriteRenderer>();

                // grab the sprint from the block we are gonna render and set it
                sr.sprite = block.sprite;

                // set the name of our game block to the name of our block for now
                blockGO.name = block.displayName;
                
                // set the position on the block to the position set in our block 2d array, this will appropriately render it
                blockGO.transform.position = new Vector3(x, y);
            }
        }
    }

}
