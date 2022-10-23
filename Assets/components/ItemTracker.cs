using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemTracker : MonoBehaviour
{
    // Start is called before the first frame update
    private static int[] _items = new int[11]{10, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    private int[] _itemPrices = new int[11] {5, 5, 7, 10, 12, 15, 17, 20, 25, 30 ,50};
    private static bool[] _itemsEnabled = new bool[11] { true, false, false, false, false, false, false, false, false, false, false};
    private static List<int> _seedsPlanted = new List<int>();
    
    [SerializeField] private Currency currency;
    [SerializeField] private TextMeshProUGUI acorns;
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
    
    private TextMeshProUGUI[] _itemTexts = new TextMeshProUGUI[11];
    private void Start()
    {
        _itemTexts[0] = acorns;
        _itemTexts[1] = mySeeds0;
        _itemTexts[2] = mySeeds1;
        _itemTexts[3] = mySeeds2;
        _itemTexts[4] = mySeeds3;
        _itemTexts[5] = mySeeds4;
        _itemTexts[6] = mySeeds5;
        _itemTexts[7] = mySeeds6;
        _itemTexts[8] = mySeeds7;
        _itemTexts[9] = mySeeds8;
        _itemTexts[10] = mySeeds9;
        UpdateSeedsCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubtractItem(int from)
    {
        _items[from] = Mathf.Max(_items[from] - 1, 0);
        _itemTexts[from].text = _items[from].ToString();
        
        if (from > 0)
        {
            _seedsPlanted.Add(from);
        }
    }

    public void AddItem(int from)
    {
        if (_itemsEnabled[from] && currency.GetCurrency() >= _itemPrices[from] && _items[from] < 10)
        {
            _items[from] += 1;
            _itemTexts[from].text = _items[from].ToString();
            currency.SubtractCurrency(_itemPrices[from]);
        }
    }

    public List<int> GetPlanted()
    {
        return _seedsPlanted;
    }

    public void ClearPlanted()
    {
        _seedsPlanted = new List<int>();
    }
    
    private void UpdateSeedsCount()
    {
        for (int i = 0; i < _items.Length; ++i)
        {
            _itemTexts[i].text = _items[i].ToString();
        }
    }

    public void UnlockSeed(int item)
    {
        if (!_itemsEnabled[item])
        {
            _itemsEnabled[item] = true;
        }
    }
}
