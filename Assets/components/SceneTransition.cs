using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneTransition : MonoBehaviour
{
    private static bool isExplore = true;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource _squirrelAttackMusic;
    private static GameObject treeToLoad = null;
    private static bool squirrelAttacking = false;
    private static List<GameObject> trees = new List<GameObject>();
    private List<int> treeGrowths = new List<int>();
    private bool notGrounded = true;
    private float timer = 6f;

    private void Start() {
        print("Start Squirrel Attacking: " + squirrelAttacking);
        if(squirrelAttacking && !isExplore) {
            _squirrelAttackMusic.Play();
        } else {
            _squirrelAttackMusic.Stop();
        }
    }

    private void Update()
    {
        if (!isExplore && notGrounded && treeToLoad)
        {
            treeToLoad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1f;
            RaycastHit hit;
            Vector3 dir = new Vector3(0, -1, 0);
            if (Physics.Raycast(treeToLoad.transform.position, dir, out hit, Mathf.Infinity))
            {
                treeToLoad.transform.position = hit.point;
                notGrounded = false;
            }
        }
        timer += Time.deltaTime;
        if (!isExplore && squirrelAttacking && timer >= 6f)
        {
            foreach (Transform child in treeToLoad.transform)
            {
                var randX = Random.Range(-1, 1);
                var randZ = Random.Range(-1, 1);
                var pos = treeToLoad.transform.position + new Vector3(randX, 0, randZ);
                StartCoroutine(MoveSquirrel(child, pos));
            }
            timer = 0f;
        }
        
    }

    IEnumerator MoveSquirrel(Transform child, Vector3 targetPosition)
    {
        float elapsedTime = 0;
        Vector3 startPosition = child.position;
        while (elapsedTime < 5)
        {
            child.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / 5);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        child.position = targetPosition;
    }
    public void SwitchModes() {
        if(!isExplore)
        {
            foreach (GameObject obj in trees)
            {
                obj.SetActive(true);
            }
            treeToLoad = null;
            SceneManager.LoadScene("exploration_scene", LoadSceneMode.Single);
        } else {
            //GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            float shortestDist = Mathf.Infinity;
            GameObject closestObj = null;
            foreach (GameObject obj in trees)
            {
                float dist = Mathf.Abs(Vector3.Distance(obj.transform.position, player.transform.position));
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    closestObj = obj;
                }
                obj.SetActive(false);
            }

            if (closestObj != null && shortestDist < 10)
            {
                treeToLoad = closestObj;
                treeToLoad.transform.localScale = 0.05f * treeToLoad.transform.localScale;
                treeToLoad.SetActive(true);
                
                // Only children of the tree will be squirrels
                squirrelAttacking = treeToLoad.transform.childCount > 0;
                // Keep squirrels of closest tree between scenes
                if(squirrelAttacking) {
                    foreach(Transform child in treeToLoad.transform) {
                        DontDestroyOnLoad(child);
                    }
                }
            }
            SceneManager.LoadScene("interaction_scene", LoadSceneMode.Single);
        }

        isExplore = !isExplore;
    }

    public void UpdateTrees(List<GameObject> objs, List<int> growths)
    {
        trees = objs;
        treeGrowths = growths;
    }
}
