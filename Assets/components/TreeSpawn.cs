using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;

public class TreeSpawn : MonoBehaviour
{
    
    [SerializeField]
    private AbstractMap map;
    [SerializeField] private ItemTracker inv;
    [SerializeField] private Currency currency;
    [SerializeField] private ARObjectSpawner objSpawner;

    [SerializeField] private GameObject tree0;
    [SerializeField] private GameObject tree1;
    [SerializeField] private GameObject tree2;
    [SerializeField] private GameObject tree3;
    [SerializeField] private GameObject tree4;
    [SerializeField] private GameObject tree5;
    [SerializeField] private GameObject tree6;
    [SerializeField] private GameObject tree7;
    [SerializeField] private GameObject tree8;
    [SerializeField] private GameObject tree9;

    private GameObject[] treePrefabs = new GameObject[10];

    private int[] _treesSpawned = new int[]{0, 0, 0, 0, 0, 0 ,0 ,0 ,0, 0};

    [SerializeField]
    float spawnScale = .2f;

    private static Vector2d playerLocation;

    private float timer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        treePrefabs[0] = tree0;
        treePrefabs[1] = tree1;
        treePrefabs[2] = tree2;
        treePrefabs[3] = tree3;
        treePrefabs[4] = tree4;
        treePrefabs[5] = tree5;
        treePrefabs[6] = tree6;
        treePrefabs[7] = tree7;
        treePrefabs[8] = tree8;
        treePrefabs[9] = tree9;
        
        CreateTrees();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;

        if (seconds >= 10)
        {
            for (int i = 0; i < _treesSpawned.Length; ++i)
            {
                currency.AddCurrency(_treesSpawned[i] * i * 2);
            }

            timer = 0.0f;
        }
    }

    public void CreateTrees()
    {
        List<int> planted = inv.GetPlanted();
        List<Vector2d> locations = objSpawner.getTreeLocations();
        for (int i = 0; i < planted.Count; ++i)
        {
            print(locations[i]);
            var instance = Instantiate(treePrefabs[planted[i]]);
            instance.transform.localPosition = map.GeoToWorldPosition(locations[i], true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            DontDestroyOnLoad(instance);
            _treesSpawned[planted[i]] += 1;
        }
        inv.ClearPlanted();
    }

    public void SetPlayerLocation(Vector2d location)
    {
        playerLocation = location;
    }
}
