using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AcornInteractions : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "GoldAcorn") {
            // Remove all squirrels attached to the tree
            foreach(GameObject child in other.gameObject.transform.parent) {
                Destroy(child);
            }
        } 
        else if(gameObject.tag == "Acorn") {
            Destroy(other.gameObject);
        }
        
        /*else if(c.gameObject.tag == "RedAcorn") {
            // TODO: Add Currency to the user
            curr.AddCurrency(10);
            Destroy(gameObject);
        }*/

        Destroy(gameObject);
    }
}
