using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /********************
     * LEBLOND Antoine
     * 20/01/2020
     * LEBLOND Antoine
     * Stock les quantités de ressource et les modifient
     * Quantité de minerais et quantité de bois
     * ******************/

    private int _nbPinkQuartz = 0;
    private int _nbIron = 0;

    public int GetNbRessource(int ressourceIndex)
    {
        switch (ressourceIndex)
        {
            default:
                return 0;
            case 1:
                return _nbPinkQuartz;
            case 2:
                return _nbIron;
        }
    }

    public void PickupRessource(int ressourceIndex) //Add a random value in a selected ressource type
    {
        switch (ressourceIndex)
        {
            case 1:
                _nbPinkQuartz += Random.Range(1, 5);
                break;
            case 2:
                _nbIron += Random.Range(1, 5);
                break;
        }
    }

    public void AddRessource(int ressourceIndex, int amount)
    {
        switch (ressourceIndex)
        {
            case 1:
                _nbPinkQuartz += amount;
                break;
            case 2:
                _nbIron += amount;
                break;
        }
    }

    public void SetRessource(int ressourceIndex, int value)
    {
        switch (ressourceIndex)
        {
            case 1:
                _nbPinkQuartz = value;
                break;
            case 2:
                _nbIron = value;
                break;
        }
    }
}
