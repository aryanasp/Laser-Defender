using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Laser" || other.gameObject.tag == "Enemy Laser")
        {
            Destroy(other.gameObject);
        }
    }
}
