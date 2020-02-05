/*******************************************
** Aucouturier Romuald
** 04-02-2020
** 
** 04-02-2020
** 
** PlayerManager
**     Cette classe va permettre de gerer les
**     competences que le robot va pouvoir gagné
**     et utiliser dans le reste du jeu
*******************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private List<Competence> _competencesList;
    private bool[] _competenceIsOk;
    private int _lenght = 0;
    
    private void Start()
    {
        _lenght = _competencesList.Count;
        _competenceIsOk = new bool[_lenght];

        for (int i = 0; i < _lenght; i++)
        {
            _competenceIsOk[i] = false;
        }
    }

    // permet d'activer une competence pour le joueur
    public Competence AddCompetence()
    {
        for (int i = 0; i < _lenght; i++)
        {
            if (_competenceIsOk[i])
            {
                continue;
            }

            _competenceIsOk[i] = true;
            var cp = _competencesList[i];
            cp.TheCapacityIsLearn();
            return cp;
        }

        return null;
    }
}