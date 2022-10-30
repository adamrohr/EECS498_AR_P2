using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpecialAcorns : MonoBehaviour
{
    [SerializeField] private GameObject goldenAcornPrefab;
    [SerializeField] private GameObject redAcornPrefab;
    [SerializeField] private float acornScale;
    // Update is called once per frame
    void Update()
    {
        int randX = Random.Range(-5, 5);
        int randZ = Random.Range(-5, 5);
        int randSpawn = Random.Range(0, 20000);
        if(randSpawn < 10) {
            print("RandSpawn: " + randSpawn);
        }
        if(randSpawn >= 2 && randSpawn <= 6) {
            var redAcorn = Instantiate(redAcornPrefab);
            redAcorn.transform.localPosition = transform.position + new Vector3(randX + 5, 1, randZ + 5);
            redAcorn.transform.localScale = new Vector3(acornScale, acornScale, acornScale);
        } else if(randSpawn <= 1) {
            var goldAcorn = Instantiate(goldenAcornPrefab);
            goldAcorn.transform.localPosition = transform.position + new Vector3(randX, 1, randZ);
            goldAcorn.transform.localScale = new Vector3(acornScale, acornScale, acornScale);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "GoldAcorn") {
            // TODO: Deal with inventory here
            print("Touched Gold");
        } else if(c.gameObject.tag == "RedAcorn") {
            // TODO: And here
            print("Touched Red");
        }

        Destroy(c.gameObject);
    }
}
