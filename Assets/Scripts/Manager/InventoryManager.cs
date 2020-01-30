/*******************************************
** Aucouturier Romuald
** 20-01-2020
** 
** 24-01-2020
** 
** InventoryManager
**  Sert à gérer l'affichage du joueur et de ce qu'il posséde
**
** Resource
**  Sert à la gestion d'une seul ressource
**  
** enum
*******************************************/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // permet d'update la case ou le changement de visibiliter est à faire
    public void CaseUpdate(int position, string name)
    {
        _caseInventory[position].sprite = GetSpriteByName(name);
    }

    // permet d'avoir la bonne image en fonction du nom donner
    private Sprite GetSpriteByName(string name)
    {
        foreach (var t in _listSprite.Where(t => t.P_name == name))
        {
            return t.P_sprite;
        }

        return _empty;
    }
}

// Cette classe va permettre d'associer un sprite à un nom
[System.Serializable]
public class SpriteName
{
    public string P_name;
    public Sprite P_sprite;
}

// la classe qui permet de gérer une resource
// avec un quantité et un type de resource
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
    public void GetResource(int size) { _count -= size; }
    public void ChangeCount(int nbr) { _count = nbr; } 
    public int GetCapacity() { return _capacity; } // Retourne la capacité de minerais maximal que la ressource peut stocker
    public void Reset() { _count = _capacity; } // reset le nombre de minerais pourvant être recuperer


    // recuperer les types de resources
    public ResourceType GetResourceType() { return _type; }
    public OresType GetOreType() { return _oreType; }


    // les fonctions pour changer les type de ressources et leurs genre précis
    public void SetResourceType(ResourceType resourceType) { _type = resourceType; }
    public void SetOreType(OresType oresType) { _oreType = oresType; }
}

// Les enum pour le type de ressource
public enum ResourceType
{
    Ores,
    Wood,
    Plan
}

// Enum pour les types plus precis (minerais)
public enum OresType
{
    Charbon,
    Iron,
    Wood,
    Lapis
}