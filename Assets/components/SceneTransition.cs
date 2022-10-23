using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private static bool isExplore = true;
    [SerializeField] private GameObject player;
    private static GameObject treeToLoad = null;

    public void SwitchModes() {
        if(!isExplore)
        {
            //treeToLoad = null;
            SceneManager.LoadScene("exploration_scene", LoadSceneMode.Single);
        } else {
            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            float shortestDist = Mathf.Infinity;
            //This won't work since closestObj will become null when the scenes are switched
            GameObject closestObj = null;
            foreach (GameObject obj in trees)
            {
                float dist = Mathf.Abs(Vector3.Distance(obj.transform.position, player.transform.position));
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    closestObj = obj;
                }
            }

            if (shortestDist < 10)
            {
                treeToLoad = closestObj;
            }
            SceneManager.LoadScene("interaction_scene", LoadSceneMode.Single);
        }

        isExplore = !isExplore;
    }
}
