using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct DrillTarget
{
    public Tile tile;
    public Tile convert;
    public float hp;
}

public class PlayerMovement : MonoBehaviour
{
    public ClientTask client;
    public GameObject rootPrefab;
    public List<DrillTarget> drillTargets;
    public List<TileBase> mined;

    public TileBase smallRoot;
    public Mouth mouth;
    public float moveRate = 0.25f;
    public float moveStep = 0.5f;
    float timeToMove = 0.0f;

    public LayerMask mask;

    public int maxDrillDepth = 1;

    public SpriteRenderer avatar;

    public ParticleSystem smokeParticles;
    private Vector2 spawnPos;

    private Tilemap tilemap;
    private Vector3Int tileAbove;

    public float drillSpeed = 100.0f;

    private float drillProgress;

    public ProgressBar drillProgressbar;
    void Start()
    {
        spawnPos = new Vector2(transform.position.x, transform.position.y);
    }

    private void Update() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        avatar.GetComponent<Animator>().SetBool("IsRunning", horizontal != 0.0f);
        var emission = smokeParticles.emission;
        emission.rateOverTime = Mathf.Abs(horizontal) * 50.0f;
        var shape = smokeParticles.shape;
        if (horizontal > 0)
        {
            shape.angle = 175;
        } else
        {
            shape.angle = 5;
        }
        if (horizontal != 0.0f)
        {
            timeToMove += Mathf.Abs(horizontal) * Time.deltaTime;
            if (timeToMove >= moveRate)
            {
                if (horizontal > 0.0f)
                {
                    MoveRight();
                } else
                {
                    MoveLeft();
                }
                timeToMove -= moveRate;
            }
        } else
        {
            timeToMove = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            mouth.SwapTeeth();
        }
        Drill();
        SetHeight();  
    }

    void SetHeight()

    {
        var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1.0f), Vector2.down, 3.0f, mask);
        if (hit)
        {
            transform.position = new Vector3(transform.position.x, spawnPos.y - (spawnPos.y - hit.point.y), transform.position.z);
            tilemap = hit.collider.gameObject.GetComponent<Tilemap>();
            if (tilemap)
            {
                tileAbove = tilemap.WorldToCell(new Vector2(hit.point.x, hit.point.y));
                if (tilemap.GetTile(tileAbove) == null)
                {
                    tileAbove.y -= 1;
                }
                var above = new Vector3Int(tileAbove.x, tileAbove.y + 1, tileAbove.z);
                if (tilemap.GetTile(above))
                {
                    tileAbove = above;
                }
                if (tileAbove != tileAboveLast)
                {
                    OnMoveAboveNewTile();
                    tileAboveLast = tileAbove;
                }
            }
        }
    }

    void MoveRight()
    {
        avatar.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        transform.position = new Vector3(transform.position.x + moveStep, transform.position.y, transform.position.z);
        SetHeight();
    }

    void MoveLeft()
    {
        avatar.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        if (transform.position.x > spawnPos.x) // Don't let player leave until objectives are cleared
        {
            transform.position = new Vector3(transform.position.x - moveStep, transform.position.y, transform.position.z);
            SetHeight();
        } else if (client.objectivesComplete)
        {
            FindObjectOfType<ContractData>().completed = true;
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        }
    }

    public void HitPick()
    {
        drillProgress += 10.0f;
    }

    private Vector3Int tileAboveLast;

    void OnMoveAboveNewTile()
    {
        drillProgress = 0.0f;
    }

    void Drill()
    {
        drillProgressbar.gameObject.SetActive(false);
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsMining", false);
        if (Input.GetAxis("Vertical") < 0.0f && tilemap && tileAbove.y >= -4 - maxDrillDepth)
        {
            // drillProgress += Time.deltaTime * -Input.GetAxis("Vertical") * drillSpeed;
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMining", true);
            foreach (var target in drillTargets)
            {
                if (target.tile == tilemap.GetTile(tileAbove))
                {
                    drillProgressbar.gameObject.SetActive(true);
                    drillProgressbar.SetProgress(drillProgress/target.hp);
                    if (drillProgress >= target.hp)
                    {
                        // Cavity extraction
                        if (tilemap.GetTile(tileAbove) == smallRoot)
                        {
                            // Event if extracting cavity vs regular root
                            GameObject ground = mouth.teethSwapped ? mouth.upper : mouth.lower;
                            var cavity = ground.GetComponent<RootSpawner>().cavity;
                            if (cavity.ContainsKey(tileAbove))
                            {
                                Destroy(cavity[tileAbove]);
                                cavity.Remove(tileAbove);
                                client.GetObjective(ObjectiveType.ExtractSmallCavity);
                            }
                        }
                        // Tile rules for mined gums
                        if (mined.Contains(target.convert))
                        {
                            var left = new Vector3Int(tileAbove.x - 1, tileAbove.y, 0);
                            var right = new Vector3Int(tileAbove.x + 1, tileAbove.y, 0);
                            tilemap.SetTile(tileAbove, mined[MinedAdjacent(tileAbove)]);
                            if (mined.Contains(tilemap.GetTile(right)))
                            {
                                tilemap.SetTile(right, mined[MinedAdjacent(right)]);
                            }
                            if (mined.Contains(tilemap.GetTile(left)))
                            {
                                tilemap.SetTile(left, mined[MinedAdjacent(left)]);
                            }
                        } else
                        {
                            tilemap.SetTile(tileAbove, target.convert);
                        }
                        drillProgress = 0.0f;
                        break;
                    }
                }
            }
        } else
        {
            drillProgress = 0.0f;
        }
    }

    private int MinedAdjacent(Vector3Int cell)
    {
        int adjacent = 0;
        if (mined.Contains(tilemap.GetTile(new Vector3Int(cell.x - 1, cell.y, cell.z))))
        {
            adjacent += 2;
        }
        if (mined.Contains(tilemap.GetTile(new Vector3Int(cell.x + 1, cell.y, cell.z))))
        {
            adjacent += 1;
        }
        return adjacent;
    }
}
