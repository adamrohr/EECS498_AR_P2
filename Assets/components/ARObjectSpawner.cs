using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARObjectSpawner : MonoBehaviour
{
    public Transform cursor;
    [SerializeField] private GameObject gameobject_to_spawn_0;
    [SerializeField] private GameObject gameobject_to_spawn_1;
    [SerializeField] private GameObject gameobject_to_spawn_2;
    [SerializeField] private GameObject gameobject_to_spawn_3;
    [SerializeField] private GameObject gameobject_to_spawn_4;
    [SerializeField] private GameObject gameobject_to_spawn_5;
    [SerializeField] private GameObject gameobject_to_spawn_6;
    [SerializeField] private GameObject gameobject_to_spawn_7;
    [SerializeField] private GameObject gameobject_to_spawn_8;
    [SerializeField] private GameObject gameobject_to_spawn_9;

    private GameObject[] trees = new GameObject[10];
    private static List<Vector2d> treeLocations = new List<Vector2d>();

    public AudioClip spawn_sound_effect;
    public float spawnScale = 0.2f;

    private void Start()
    {
        trees[0] = gameobject_to_spawn_0;
        trees[1] = gameobject_to_spawn_1;
        trees[2] = gameobject_to_spawn_2;
        trees[3] = gameobject_to_spawn_3;
        trees[4] = gameobject_to_spawn_4;
        trees[5] = gameobject_to_spawn_5;
        trees[6] = gameobject_to_spawn_6;
        trees[7] = gameobject_to_spawn_7;
        trees[8] = gameobject_to_spawn_8;
        trees[9] = gameobject_to_spawn_9;

    }

    public void SpawnGameobjectAtCursor(int objectNumber)
    {
        GameObject new_object = Instantiate(trees[objectNumber]);
        new_object.transform.SetPositionAndRotation(cursor.position, cursor.rotation);
        new_object.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
        // Vector2d treeLoc = new Vector2d(cursor.position.x*0.000001, cursor.position.y*0.000001);
        Vector2d treeLoc = new Vector2d(42.291609, -83.715868);
        treeLocations.Add(treeLoc);
        for(int i = 0; i < treeLocations.Count; i++) {
            print("SpawnObj at cursor loop");
            print(treeLocations[i]);
        }
        // DontDestroyOnLoad(treeLocations);
        AudioSource.PlayClipAtPoint(spawn_sound_effect, Camera.main.transform.position);
    }

    public void SwitchModes()
    {
        SceneManager.LoadScene("exploration_scene");
    }

    public List<Vector2d> getTreeLocations() {
        return treeLocations;
    }
}



