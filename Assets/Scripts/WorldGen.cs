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

    private ItemDatabase itemDatabase;

    public void DestroyBlock(GameObject block) {
        // Destroy reference in blocks array
        int x = (int)block.transform.position.x;
        int y = (int)block.transform.position.y;

        foreach(Drop drop in blocks[x,y].drops) {
            if(drop.DropChanceSuccess()) {
                // create drops
                GameObject dropObject = new GameObject();
                dropObject.transform.position = block.transform.position;
                // set the scale to be slightly smaller than the block so it looks like it was dropped
                dropObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                dropObject.AddComponent<SpriteRenderer>().sprite = itemDatabase.FindItem(drop.itemName).sprite;
                // add a collider box around our dropped item
                dropObject.AddComponent<PolygonCollider2D>();
                // add physics to the box
                dropObject.AddComponent<Rigidbody2D>();
                // in unity we added a layer to the player called "Drop" under layer 9, set that here
                dropObject.layer = 9;
            }
        }

        // set to air block, which is basically just removing it
        blocks[x, y] = blockManager.FindBlock(0);

        // Destroy GameObject
        GameObject.Destroy(block);
    }

    private void Start() {
        blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
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
            int pHeight = Mathf.RoundToInt(pValue * 10f + heightModifier);
            // cols
            for (int y = 0; y < height; y++) {
                // given our random height, if y is less than how high we want the column to be
                if (y <= pHeight) {
                    // this will grab the top level of the world we are generating
                    if (y == pHeight - 1) {
                        // grass
                        blocks[x, y] = blockManager.FindBlock(1);
                    } else if (y == pHeight) {
                        // add flowers
                        // if 1/20 (5% chance) of the blocks will have a flower.
                        if(Random.value < 0.05f) {
                            blocks[x, y] = blockManager.FindBlock(4);
                        }
                    } else if (y == pHeight - 2 || y == pHeight - 3 || y == pHeight - 4) {
                        // add layers of dirt layers below grass
                        blocks[x, y] = blockManager.FindBlock(3);
                    } else {
                        // stone
                        blocks[x, y] = blockManager.FindBlock(2);
                    }
                    
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
                    // create a new game object, similar to how you do it in unity but this how you do it in code
                    GameObject blockGO = new GameObject();

                    // set the name of our game block to the name of our block for now
                    blockGO.name = block.displayName;

                    // using tag to indicate that this is block
                    blockGO.tag = "Block";

                    // add a sprite renderer component to our new game object
                    SpriteRenderer sr = blockGO.AddComponent<SpriteRenderer>();

                    // grab the sprint from the block we are gonna render and set it
                    sr.sprite = block.sprite;

                    if(block.isSolid) {
                        // apparently this is all you need to add collisions to the tiles we are creating
                        BoxCollider2D col = blockGO.AddComponent<BoxCollider2D>();
                    }

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
