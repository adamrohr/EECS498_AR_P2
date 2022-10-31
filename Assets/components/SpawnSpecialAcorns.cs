using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnSpecialAcorns : MonoBehaviour
{
    [SerializeField] private GameObject goldenAcornPrefab;
    [SerializeField] private GameObject redAcornPrefab;
    [SerializeField] private GameObject acornPrefab;
    [SerializeField] private float acornScale;
    [SerializeField] private GameObject player;

    [SerializeField] private ItemTracker inv;

    [SerializeField] private Currency curr;
    // Update is called once per frame
    void Update()
    {
        int randX = Random.Range(5, 20);
        int randZ = Random.Range(5, 20);
        int randNegX = Random.Range(0, 2);
        int randNegZ = Random.Range(0, 2);
        if (randNegX == 0)
        {
            randX *= -1;
        }

        if (randNegZ == 0)
        {
            randZ *= -1;
        }
        int randSpawn = Random.Range(0, 20000);
        if(randSpawn < 10) {
            print("RandSpawn: " + randSpawn);
        }

        if (randSpawn >= 4 && randSpawn <= 6)
        {
            var acorn = Instantiate(acornPrefab);
            acorn.transform.localPosition = player.transform.position + new Vector3(randX, 1, randZ);
            acorn.transform.localScale = new Vector3(acornScale, acornScale, acornScale);
        }
        else if(randSpawn >= 2 && randSpawn <= 3) {
            var redAcorn = Instantiate(redAcornPrefab);
            redAcorn.transform.localPosition = player.transform.position + new Vector3(randX, 1, randZ);
            redAcorn.transform.localScale = new Vector3(acornScale, acornScale, acornScale);
        } else if(randSpawn <= 1) {
            var goldAcorn = Instantiate(goldenAcornPrefab);
            goldAcorn.transform.localPosition = player.transform.position + new Vector3(randX, 1, randZ);
            goldAcorn.transform.localScale = new Vector3(acornScale, acornScale, acornScale);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "GoldAcorn") {
            inv.AddFreeItem(11);
            print("Touched Gold");
        } else if(c.gameObject.tag == "RedAcorn") {
            curr.AddCurrency(10);
            print("Touched Red");
        }
        else if (c.GameObject().tag == "Acorn")
        {
            inv.AddFreeItem(0);
            print("Touched Acorn");
        }

        Destroy(c.gameObject);
    }
}
