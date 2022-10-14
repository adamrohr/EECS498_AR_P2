using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemTracker : MonoBehaviour
{
    // Start is called before the first frame update
    private static int[] _seeds = new int[10]{3, 3, 3, 3, 3, 3, 3, 3, 3, 3};
    private static List<int> _seedsPlanted = new List<int>();

    [SerializeField] private TextMeshProUGUI mySeeds0;
    [SerializeField] private TextMeshProUGUI mySeeds1;
    [SerializeField] private TextMeshProUGUI mySeeds2;
    [SerializeField] private TextMeshProUGUI mySeeds3;
    [SerializeField] private TextMeshProUGUI mySeeds4;
    [SerializeField] private TextMeshProUGUI mySeeds5;
    [SerializeField] private TextMeshProUGUI mySeeds6;
    [SerializeField] private TextMeshProUGUI mySeeds7;
    [SerializeField] private TextMeshProUGUI mySeeds8;
    [SerializeField] private TextMeshProUGUI mySeeds9;
    
    private TextMeshProUGUI[] _seedTexts = new TextMeshProUGUI[10];
    private void Start()
    {
        _seedTexts[0] = mySeeds0;
        _seedTexts[1] = mySeeds1;
        _seedTexts[2] = mySeeds2;
        _seedTexts[3] = mySeeds3;
        _seedTexts[4] = mySeeds4;
        _seedTexts[5] = mySeeds5;
        _seedTexts[6] = mySeeds6;
        _seedTexts[7] = mySeeds7;
        _seedTexts[8] = mySeeds8;
        _seedTexts[9] = mySeeds9;
        UpdateSeedsCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubtractSeed(int from)
    {
        _seeds[from] = Mathf.Max(_seeds[from] - 1, 0);
        _seedTexts[from].text = _seeds[from].ToString();
        _seedsPlanted.Add(from);
        print(_seedsPlanted);
    }

    public void AddSeed(int from)
    {
        _seeds[from] = Mathf.Min(_seeds[from] + 1, 9);
        _seedTexts[from].text = _seeds[from].ToString();
    }

    public List<int> GetPlanted()
    {
        print(_seedsPlanted);
        return _seedsPlanted;
    }

    public void ClearPlanted()
    {
        _seedsPlanted = new List<int>();
    }
    
    private void UpdateSeedsCount()
    {
        for (int i = 0; i < _seeds.Length; ++i)
        {
            _seedTexts[i].text = _seeds[i].ToString();
        }
    }
}
