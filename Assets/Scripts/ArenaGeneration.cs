using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGeneration : MonoBehaviour
{
    public Vector2Int maxArenaSize = new Vector2Int(10, 10);
    public Vector2Int emptyBlockChance = new Vector2Int(18, 24);
    public Vector2Int wallChance = new Vector2Int(2, 24);

    public List<Block> blocks = new List<Block>();
    public List<GameObject> walls = new List<GameObject>();
    public LayerMask blockLayer;

    public int outerRimFraction = 8;
    public int outerRimChance = 2;
    public int cornerChance = 5;

    public Player player;

    void Start()
    {
        StartCoroutine("GenerateArena");
    }

    IEnumerator GenerateArena()
    {
        int lowRimX = maxArenaSize.x / outerRimFraction;
        int highRimX = maxArenaSize.x - lowRimX;

        int lowRimZ = maxArenaSize.y / outerRimFraction;
        int highRimZ = maxArenaSize.y - lowRimZ;

        int chance = emptyBlockChance.x;


        for (int z = 0; z < maxArenaSize.y; z++)
        {
            for (int x = 0; x < maxArenaSize.x; x++)
            {
                //Corners
                if ((x < lowRimX || x > highRimX) && (z < lowRimZ || z > highRimZ))
                {
                    chance = (emptyBlockChance.x / cornerChance);
                }
                // Edges
                if ((x < lowRimX || x > highRimX) || (z < lowRimZ || z > highRimZ))
                {
                    chance = (emptyBlockChance.x / outerRimChance);
                }
                // Centre
                else
                {
                    chance = emptyBlockChance.x;
                }

                transform.position = new Vector3(x, 0, z);

                int randomNumber = Random.Range(0, emptyBlockChance.y);
                if (randomNumber < chance)
                {
                    if (blocks != null)
                    {
                        Instantiate(blocks[Random.Range(0, blocks.Count)], transform.position, Quaternion.identity);
                    }
                }                
            }
            yield return null;
        }

        for (int z = 0; z < maxArenaSize.y; z++)
        {
            for (int x = 0; x < maxArenaSize.x; x++)
            {
                transform.position = new Vector3(x, 1, z);
                int randomNumber = Random.Range(0, wallChance.y);
                if (randomNumber < chance)
                {
                    Instantiate(walls[Random.Range(0, walls.Count)], transform.position, Quaternion.identity);

                }                
            }
            yield return null;
        }

        Instantiate(player,new Vector3(maxArenaSize.x/2,1,maxArenaSize.y/2),Quaternion.identity);
    }    
}