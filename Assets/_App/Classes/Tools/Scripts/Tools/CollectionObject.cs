using STIGRADOR;
using TMPro;
using UnityEngine;

public class CollectionObject : BasePoolObject
{
    [SerializeField] private TextMeshProUGUI _text;
    
    private string _data;
    
    public void Init(CollectionData data)
    {
        gameObject.SetActive(true);
        _data = data.info;
        _text.text = _data;
    }

    public override void Return()
    {
        base.Return();
        
        gameObject.SetActive(false);
    }
}
