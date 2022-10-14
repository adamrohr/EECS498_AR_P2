using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public AudioClip spawn_sound_effect;

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

        AudioSource.PlayClipAtPoint(spawn_sound_effect, Camera.main.transform.position);
    }

    public void SwitchModes()
    {
        SceneManager.LoadScene("exploration_scene");
    }
}



