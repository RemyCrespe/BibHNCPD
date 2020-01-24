using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> _caseInventory;

    [SerializeField]
    private List<SpriteName> _listSprite;

    [SerializeField]
    private Sprite _empty;

    // Start is called before the first frame update
    void Start()
    {
        CaseUpdate(0, "coal");
    }

    public void CaseUpdate(int position, string name)
    {
        _caseInventory[position].sprite = GetSpriteByName(name);
    }

    private Sprite GetSpriteByName(string name)
    {
        for (int i = 0; i < _listSprite.Count; i++)
        {
            if (_listSprite[i].P_name == name)
            {
                return _listSprite[i].P_sprite;
            }
        }

        return _empty;
    }
}

[System.Serializable]
public class SpriteName
{
    public string P_name;
    public Sprite P_sprite;
}

[System.Serializable]
public class Resource
{
    [SerializeField]
    private ResourceType _type;
    [SerializeField]
    private OresType _oreType;

    [SerializeField]
    private int _count;
    [SerializeField]
    private int _capacity;

    public int GetCount() { return _count; }
    public void GetResource(int size) { _count += size; }
    public void ChangeCount(int nbr) { _count = nbr; }
    public int GetCapacity() { return _capacity; }

    public ResourceType GetResourceType() { return _type; }
    public OresType GetOreType() { return _oreType; }

    public void SetResourceType(ResourceType resourceType) { _type = resourceType; }
    public void SetOreType(OresType oresType) { _oreType = oresType; }
}

public enum ResourceType
{
    Ores,
    Wood,
    Plan
}

public enum OresType
{
    Charbon,
    Iron,
    Wood,
    Lapis
}