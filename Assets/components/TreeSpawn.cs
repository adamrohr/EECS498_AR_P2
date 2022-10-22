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
    [SerializeField] private GameObject squirrelPrefab;

    private GameObject[] treePrefabs = new GameObject[10];

    private int[] _treesSpawned = new int[]{0, 0, 0, 0, 0, 0 ,0 ,0 ,0, 0};
    private List<int> _squirrelTreesOrder = new List<int>();

    [SerializeField] float spawnScale = .2f;
    [SerializeField] float squirrelScale = .3f;

    private static Vector2d playerLocation;
    private float timer = 0.0f;
    private bool spawnedTrees = false;
    
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
        
        // CreateTrees();
    }

    // Update is called once per frame
    void Update()
    {
        if(map.InitialZoom != 0 && !spawnedTrees) {
            CreateTrees();
            spawnedTrees = true;
        }

        timer += Time.deltaTime;
        int seconds = (int)timer % 60;
        List<Vector2d> locations = objSpawner.getTreeLocations();
        int squirrelSpawnTree = Random.Range(0, 4000 * locations.Count);

        if (seconds >= 10)
        {
            for (int i = 0; i < _treesSpawned.Length; ++i)
            {
                float currencyAdd = _treesSpawned[i] * i * 2;
                // still need to test this
                if(_squirrelTreesOrder.Contains(i)) {
                    currencyAdd *= 0.1f;
                }
                currency.AddCurrency(currencyAdd);
            }

            timer = 0.0f;
        }

        // Check if squirrelSpawn is a valid index in locations
        if(squirrelSpawnTree < locations.Count) {
            int randX = Random.Range(-7, 7);
            int randZ = Random.Range(-7, 7);
            var squirrel = Instantiate(squirrelPrefab);
            squirrel.transform.localPosition = map.GeoToWorldPosition(locations[squirrelSpawnTree], true);
            squirrel.transform.localScale = new Vector3(squirrelScale, squirrelScale, squirrelScale);
            squirrel.transform.localPosition += new Vector3(randX, 0, randZ);
            DontDestroyOnLoad(squirrel);
        }
    }

    public void CreateTrees()
    {
        List<int> planted = inv.GetPlanted();
        List<Vector2d> locations = objSpawner.getTreeLocations();
        for (int i = 0; i < planted.Count; ++i)
        {
            var instance = Instantiate(treePrefabs[planted[i]]);
            instance.transform.localPosition = map.GeoToWorldPosition(locations[i], true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            DontDestroyOnLoad(instance);
            _treesSpawned[planted[i]] += 1;
            _squirrelTreesOrder.Add(planted[i]);
        }
        inv.ClearPlanted();
    }

    public void SetPlayerLocation(Vector2d location)
    {
        playerLocation = location;
    }
}
