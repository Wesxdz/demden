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

    public List<PlayerSkill> availableShopkeepers;

    public Tile[] mined;
    public GameObject shopkeeperPrefab;

    private void Awake() 
    {
        var data = FindObjectOfType<ContractData>();
        mouth = GetComponent<Mouth>();
        upperTiles = mouth.upper.GetComponent<Tilemap>();
        lowerTiles = mouth.lower.GetComponent<Tilemap>();

        // lowerTiles.SetTilesBlock(new BoundsInt(-10, -15, 0, depth + 10, 10, 0), background);

        bool spawnLower = Random.Range(0, 2) > 0;
        int spawnKeeperX = data.terms.depth + Random.Range(-data.terms.depth/4, data.terms.depth/2);
        var go = GameObject.Instantiate(shopkeeperPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, spawnLower ? mouth.lower.transform : mouth.upper.transform);
        go.transform.GetChild(0).GetComponent<Shopkeeper>().spawnedLower = spawnLower;
        go.transform.localPosition = new Vector3(spawnKeeperX + 0.5f, -4.0f, 0.0f);
        // TODO: Spawn one shopkeeper per contract just past the depth of the contract
        // Modify surrounding tiles for 'room'
        // xx
        // xxx
        List<Vector3Int> lowerReduced = new List<Vector3Int>();
        List<Vector3Int> upperReduced = new List<Vector3Int>();
        if (spawnLower)
        {
            for (int i = 0; i < 3; i++)
            {
                lowerReduced.Add(new Vector3Int(spawnKeeperX - 1 + i, -5, 0));
            }
            for (int i = 0; i < 2; i++)
            {
                upperReduced.Add(new Vector3Int(spawnKeeperX + i, -5, 0));
            }
        } 
        else
        {
            for (int i = 0; i < 3; i++)
            {
                upperReduced.Add(new Vector3Int(spawnKeeperX - 1 + i, -5, 0));
            }
            for (int i = 0; i < 2; i++)
            {
                lowerReduced.Add(new Vector3Int(spawnKeeperX - 1 + i, -5, 0));
            }
        }

        float seed = Random.Range(0.0f, 100000.0f);
        for (int x = -10; x < depth; x++)
        {
            if (lowerReduced.Contains(new Vector3Int(x, -5, 0)))
            {

            } else
            {
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
            }
            for (int y = -6; y >= -16; y--)
            {
                var pos = new Vector3Int(x, y, 0); 
                lowerTiles.SetTile(pos, background);
            }
        }

        seed = Random.Range(0.0f, 100000.0f);
        for (int x = -10; x < depth; x++)
        {
            if (upperReduced.Contains(new Vector3Int(x, -5, 0)))
            {

            } else
            {
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
            }
            for (int y = -6; y >= -16; y--)
            {
                var pos = new Vector3Int(x, y, 0); 
                upperTiles.SetTile(pos, background);
            }
        }

        foreach (var pos in lowerReduced)
        {
            int index = 0;
            if (lowerReduced.Contains(new Vector3Int(pos.x - 1, pos.y, pos.z)))
            {
                index += 1;
            }
            if (lowerReduced.Contains(new Vector3Int(pos.x + 1, pos.y, pos.z)))
            {
                index += 2;
            };
            lowerTiles.SetTile(pos, mined[index]);
        }

        foreach (var pos in upperReduced)
        {
            int index = 0;
            if (upperReduced.Contains(new Vector3Int(pos.x - 1, pos.y, pos.z)))
            {
                index += 1;
            }
            if (upperReduced.Contains(new Vector3Int(pos.x + 1, pos.y, pos.z)))
            {
                index += 2;
            };
            upperTiles.SetTile(pos, mined[index]);
        }
    }
}
