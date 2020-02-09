/********************
     * LEBLOND Antoine
     * 28/01/2020
     * LEBLOND Antoine
     * Stock les quantités de ressource, possibilité de les modifier
     * Nom de la ressource, quantité de celle-ci
     * ******************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ressource
{
    [SerializeField]
    private string _ressourceName;
    [SerializeField]
    private int _ressourceQuantity;

    public Ressource(string name, int quantity)
    {
        _ressourceName = name;
        _ressourceQuantity = quantity;
    }

    public bool CompareName(string name)
    {
        return _ressourceName == name;
    }

    public int GetQuantity()
    {
        return _ressourceQuantity;
    }

    public void SetQuantity(int quantity)
    {
        _ressourceQuantity = quantity;
    }

    public void AddQuantity(int amount)
    {
        _ressourceQuantity += amount;
    }
}
