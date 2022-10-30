using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornInteractions : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "GoldAcorn") {
            // Remove all squirrels attached to the tree
            foreach(GameObject child in c.gameObject.transform.parent) {
                Destroy(child);
            }
        } else if(c.gameObject.tag == "RedAcorn") {
            // TODO: Add Currency to the user
            Destroy(gameObject);
        } else if(c.gameObject.tag == "Acorn") {
            Destroy(gameObject);
        }

        Destroy(c.gameObject);
    }
}
