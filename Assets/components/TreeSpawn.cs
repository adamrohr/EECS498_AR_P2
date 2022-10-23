using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using Mapbox.Unity.Location;

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

    private static int[] _treesSpawned = new int[]{0, 0, 0, 0, 0, 0 ,0 ,0 ,0, 0};
    private List<int> _treesOrder = new List<int>();
    private List<int> _squirrelTrees = new List<int>();
    private List<GameObject> _treeObjects = new List<GameObject>();

    [SerializeField] float spawnScale = .2f;
    [SerializeField] float squirrelScale = .3f;

    private static Vector2d playerLocation;
    private float timer = 0.0f;
    private bool spawnedTrees = false;

    private static List<Vector2d> treeLocations = new List<Vector2d>();
    private static List<int> plantOrder = new List<int>();
    
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
        int squirrelSpawnTree = Random.Range(0, 2000 * locations.Count);

        // Check if squirrelSpawn is a valid index in locations
        if(squirrelSpawnTree < _treesOrder.Count) {
            List<int> planted = inv.GetPlanted();
            int randX = Random.Range(-2, 2);
            int randZ = Random.Range(-2, 2);
            var squirrel = Instantiate(squirrelPrefab);
            squirrel.transform.parent = _treeObjects[squirrelSpawnTree].transform;
            // squirrel.transform.localPosition = map.GeoToWorldPosition(locations[squirrelSpawnTree], true);
            squirrel.transform.localScale = new Vector3(squirrelScale, squirrelScale, squirrelScale);
            squirrel.transform.localPosition += new Vector3(randX, 0, randZ);
            _squirrelTrees.Add(_treesOrder[squirrelSpawnTree]);
            DontDestroyOnLoad(squirrel);
        }

        if (seconds >= 10)
        {
            for (int i = 0; i < _treesSpawned.Length; ++i)
            {
                float currencyAdd = _treesSpawned[i] * (i+1);
                // still need to test this
                if(_squirrelTrees.Contains(i)) {
                    currencyAdd *= 0.1f;
                }
                currency.AddCurrency(currencyAdd);
                timer = 0.0f;
            }
        }
    }

    public void AddTreeLocation(Vector2d location)
    {
        treeLocations.Add(location);
    }

    public void CreateTrees()
    {
        for (int i = 0; i < treeLocations.Count; ++i)
        {
            var instance = Instantiate(treePrefabs[plantOrder[i]]);
            instance.transform.localPosition = map.GeoToWorldPosition(treeLocations[i], true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            instance.tag = "Tree";
        }
        
        List<int> planted = inv.GetPlanted();
        List<Vector2d> locations = objSpawner.getTreeLocations();
        for (int i = 0; i < planted.Count; ++i)
        {
            var instance = Instantiate(treePrefabs[planted[i]-1]);
            Vector2d pos = locations[i];
            
            instance.transform.localPosition = map.GeoToWorldPosition(pos, true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            instance.tag = "Tree";
            treeLocations.Add(pos);
            plantOrder.Add(planted[i]-1);
            _treesSpawned[planted[i]-1] += 1;
            _treesOrder.Add(planted[i] - 1);
            _treeObjects.Add(instance);
        }
        inv.ClearPlanted();
    }

    public void SetPlayerLocation(Vector2d location)
    {
        playerLocation = location;
    }
}
