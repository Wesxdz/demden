using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavity : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer sprite;
    private Mouth mouth;
    public float visiblilityDistance = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>().transform;
        mouth = FindObjectOfType<Mouth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            sprite.enabled = player.transform.position.y < -4.1f && Vector3.Magnitude(transform.position - player.transform.position) < visiblilityDistance && (mouth.teethSwapped ? transform.parent.gameObject == mouth.upper : transform.parent.gameObject == mouth.lower);
        }
    }
}
