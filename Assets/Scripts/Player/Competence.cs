/*******************************************
** Aucouturier Romuald
** 04-02-2020
** 
** 04-02-2020
** 
** Competence
**     Cette classe est la classe abstraite pour la
**     création des competences que le robot va
**     pouvoir gagner dans le jeu en fonction des minis-jeux
**     réussi avec la phrase de prise de conscience
*******************************************/

using System;
using UnityEngine;

public abstract class Competence : MonoBehaviour
{
    [Header("Toute les information de la compétence")]
    [SerializeField] protected string _name = "competence";
    [SerializeField] protected string _useDescription = "press space to use... :3";
    [SerializeField] protected Sprite _sprite;

    [TextArea(5, 10)]
    [SerializeField] protected string _awareness;

    [SerializeField] private bool _isOk = false;

    public void TheCapacityIsLearn() => _isOk = true;
    
    public Sprite GetImage() => _sprite;
    public string GetName() => _name;
    public string GetUseDescription() => _useDescription;

    public string GetAwareness() => _awareness;
}