/********************
 * LEBLOND Antoine
 * 20/01/2020
 * LEBLOND Antoine
 * Stock les quantités de ressource et les modifient
 * Quantité de minerais et quantité de bois
 * ******************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int _nbPinkOre = 0;
    public int _nbIron = 0;

    public void ModifyRessourceAmount(int ressourceIndex) //Add a random value in a selected ressource type
    {
        switch (ressourceIndex)
        {
            case 1:
                _nbPinkOre += Random.Range(1, 5);
                break;
            case 2:
                _nbIron += Random.Range(1, 5);
                break;
        }
    }
}
