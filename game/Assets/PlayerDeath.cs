using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathVFX;
    public Smasher smasher;
    public GameObject pickaxe;
    public GameObject lantern;

    public UnityEvent OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {       
        print(smasher.mouthState); 
        if (smasher.mouthState == Smasher.MouthState.Closing && 
        other.gameObject.layer == LayerMask.NameToLayer("TopTeeth"))
        {
            print("Eaten by {demon}");
            Die();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            print("Died to hazard");
            Die();
        }
    }

    public void Die()
    {
        FindObjectOfType<ContractData>().failed = true;
        GameObject.Instantiate(deathVFX, transform.position, transform.rotation);
        OnDeath.Invoke();
        lantern.transform.parent = transform.parent;
        lantern.GetComponent<Rigidbody2D>().simulated = true;
        pickaxe.transform.parent = transform.parent;
        pickaxe.GetComponent<Rigidbody2D>().simulated = true;
        Destroy(gameObject);
    }
}
