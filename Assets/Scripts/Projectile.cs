using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //When the projectile collides with something, for now make it dissapear,
        //later will will add an explosion effect.
        Debug.Log("Destroying Projectile!");
        Destroy(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
