/********************
     * LEBLOND Antoine
     * 24/01/2020
     * LEBLOND Antoine
     * Stock les quantités de ressource avec une liste, possibilité de récupérer la quantité, de modifier la quantité
     * Liste contenant les ressources, leur index représentant un type de ressource
     * ******************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Ressource> _ressourceTab = new List<Ressource>();

    public int GetRessourceTabSize()
    {
        return _ressourceTab.Count;
    }

    public int GetRessourceQuantity(int index)
    {
        return _ressourceTab[index].GetQuantity();
    }

    public void AddRessourceQuantity(int index, int amount)
    {
        _ressourceTab[index].AddQuantity(amount);
    }

    public void SetRessourceQuantity(int index, int amount)
    {
        _ressourceTab[index].SetQuantity(amount);
    }
}
