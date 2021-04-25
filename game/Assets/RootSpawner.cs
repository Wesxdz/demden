using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootSpawner : MonoBehaviour
{
    private Tilemap tilemap;
    public GameObject rootPrefab;
    public List<TileBase> teeth;
    public Vector2 spawnOffset;

    public Sprite cavitySprite;

    public Dictionary<Vector3Int, GameObject> regular = new Dictionary<Vector3Int, GameObject>();
    public Dictionary<Vector3Int, GameObject> cavity = new Dictionary<Vector3Int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        var data = FindObjectOfType<ContractData>();
        tilemap = GetComponent<Tilemap>();
        for(var x = tilemap.cellBounds.min.x; x< tilemap.cellBounds.max.x;x++)
        {
            for(var y= tilemap.cellBounds.min.y; y< tilemap.cellBounds.max.y;y++)
            {
                var tileCell = new Vector3Int(x,y,0);
                var tile = tilemap.GetTile(tileCell);
                if (teeth.Contains(tile))
                {
                    var root = GameObject.Instantiate(rootPrefab, new Vector3(x + spawnOffset.x, y + spawnOffset.y, 0.0f), Quaternion.identity, transform);
                    if (tileCell.x > -10 + data.terms.depth && Random.Range(0.0f, 100.0f) > 80.0f)
                    {
                        root.GetComponent<SpriteRenderer>().sprite = cavitySprite;
                        cavity.Add(new Vector3Int(x, y - 1, 0), root);
                    } 
                    else
                    {
                        regular.Add(tileCell, root);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
