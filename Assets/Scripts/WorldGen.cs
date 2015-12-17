using UnityEngine;
using System.Collections;

public class WorldGen : MonoBehaviour {

    // this gets added in unity as a prefab
    public GameObject player;
    public float heightModifier = 20;

    private BlockManager blockManager;

    private int width = 64;
    private int height = 64;

    private Block[,] blocks;

    private void Start() {
        blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        blocks = new Block[width, height];

        GenerateBlocks();
        SpawnBlocks();
    }

    private void GenerateBlocks() {

        int playerSpawnX = Random.Range(0, width);

        // rows
        for (int x = 0; x < width; x++) {
            // we are just taking a strip of the perlin noise that gets generated to start
            // making them smaller values, should make the terrain more dramatic
            float pValue = Mathf.PerlinNoise(x * 0.05f, 5 * 0.05f);
            // how high the terrain is going to be for each row tile
            // the 10f changes how dramatic it is
            float pHeight = pValue * 10f + heightModifier;
            // cols
            for (int y = 0; y < height; y++) {
                // given our random height, if y is less than it, generate stone
                if (y < pHeight) {
                    blocks[x, y] = blockManager.FindBlock(2);
                } else {
                    // leave as air block
                    blocks[x, y] = blockManager.FindBlock(0);
                }
            }

            // seems weird we spawn the player here
            if (x == playerSpawnX) {
                SpawnPlayer(x, pHeight + 3);
            }
        }
    }

    private void SpawnBlocks() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                // grab the block set for this location
                Block block = blocks[x, y];
                // because we set air block to 0 and our FindBlock method ruturns null this works
                // kind of hacky, fix later
                if(block != null) {
                    // create a new game object
                    GameObject blockGO = new GameObject();

                    // add a sprite renderer component to our new game object
                    SpriteRenderer sr = blockGO.AddComponent<SpriteRenderer>();

                    // grab the sprint from the block we are gonna render and set it
                    sr.sprite = block.sprite;

                    // apparently this is all you need to add collisions to the tiles we are creating
                    BoxCollider2D col = blockGO.AddComponent<BoxCollider2D>();

                    // set the name of our game block to the name of our block for now
                    blockGO.name = block.displayName;

                    // set the position on the block to the position set in our block 2d array, this will appropriately render it
                    blockGO.transform.position = new Vector3(x, y);
                }

            }
        }
    }

    private void SpawnPlayer(float x, float y) {
        // now create the player prefab that we set in unity
        GameObject.Instantiate(player, new Vector3(x, y), Quaternion.identity);
    }
}
