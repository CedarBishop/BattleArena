using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGeneration : MonoBehaviour
{
    public Vector3Int maxArenaSize = new Vector3Int(10,3,10);
    public Vector2Int emptyBlockChance = new Vector2Int(18,24);

    public List<Block> blocks = new List<Block>();
    public LayerMask blockLayer;

    public int outerRimFraction = 8;
    public int outerRimChance = 2;
    public int cornerChance = 5;
    
    void Start()
    {
        StartCoroutine("GenerateArena");
    }

    IEnumerator GenerateArena ()
    {
        int lowRimX = maxArenaSize.x / outerRimFraction;
        int highRimX = maxArenaSize.x - lowRimX;

        int lowRimZ = maxArenaSize.z / outerRimFraction;
        int highRimZ = maxArenaSize.z - lowRimZ;

        int chance = emptyBlockChance.x;
        

        for (float y = 0; y < maxArenaSize.y; y += 0.5f)
        {
            for (int z = 0; z < maxArenaSize.z; z++)
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
                    
                    transform.position = new Vector3(x,y,z);
                    
                    int randomNumber = Random.Range(0,emptyBlockChance.y);
                    if (randomNumber < chance)
                    {
                        if (blocks != null)
                        {
                            if (Physics.Raycast(transform.position,Vector3.down,1f,blockLayer) || y == 0)
                            {
                                Instantiate(blocks[Random.Range(0, blocks.Count)], transform.position, Quaternion.identity);                           

                            }                                            
                        }
                    }
                }
                    yield return null;
            }
            
            emptyBlockChance = new Vector2Int(emptyBlockChance.x - 2 ,emptyBlockChance.y);
        }
    }
}
