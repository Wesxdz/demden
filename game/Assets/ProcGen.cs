using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ProcGen : MonoBehaviour
{
    private Mouth mouth;

    public int depth;

    Tilemap upperTiles, lowerTiles;

    public Tile background;
    public Tile surface;
    public Tile canal;
    public Tile[] smallTooth;

    private void Awake() 
    {
        mouth = GetComponent<Mouth>();
        upperTiles = mouth.upper.GetComponent<Tilemap>();
        lowerTiles = mouth.lower.GetComponent<Tilemap>();
        // lowerTiles.SetTilesBlock(new BoundsInt(-10, -15, 0, depth + 10, 10, 0), background);
        for (int x = -10; x < depth; x++)
        {
            float seed = Random.Range(0.0f, 100000.0f);
            float noise = Mathf.PerlinNoise(x + 0.1f, seed -5.01f);
            float threshold = 0.5f - (((x + 10)/1000.0f) * Mathf.PerlinNoise(0.1f, x + 0.8f));
            bool spawnTooth = noise > threshold;
            if (spawnTooth)
            {
                lowerTiles.SetTile(new Vector3Int(x, -4, 0), smallTooth[Mathf.Min(2, (int)(((noise + Random.Range(0.0f, 0.4f)) - threshold)/0.2f))]);
                lowerTiles.SetTile(new Vector3Int(x, -5, 0), canal);
            } 
            else
            {
                lowerTiles.SetTile(new Vector3Int(x, -5, 0), surface);
            }
            for (int y = -6; y >= -16; y--)
            {
                var pos = new Vector3Int(x, y, 0); 
                lowerTiles.SetTile(pos, background);
            }
        }

        for (int x = -10; x < depth; x++)
        {
            float seed = Random.Range(0.0f, 100000.0f);
            float noise = Mathf.PerlinNoise(x + 0.1f, 5.01f);
            float threshold = 0.5f - (((x + 10)/1000.0f) * Mathf.PerlinNoise(seed + 3252.1f, x + 530.8f));
            bool spawnTooth = noise > threshold;
            if (spawnTooth)
            {
                upperTiles.SetTile(new Vector3Int(x, -4, 0), smallTooth[Mathf.Min(2, (int)(((noise + Random.Range(0.0f, 0.4f)) - threshold)/0.2f))]);
                upperTiles.SetTile(new Vector3Int(x, -5, 0), canal);
            } 
            else
            {
                upperTiles.SetTile(new Vector3Int(x, -5, 0), surface);
            }
            for (int y = -6; y >= -16; y--)
            {
                var pos = new Vector3Int(x, y, 0); 
                upperTiles.SetTile(pos, background);
            }
        }
    }
}
