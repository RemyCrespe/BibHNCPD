/*******************************************
** Aucouturier Romuald
** 30-01-2020
** 
** 30-01-2020
** 
** MiniGame
** La classe primaire pour les mini jeux
**     possede 2 fonction:
**         1 pour simuler la fonction "update" (fonction lancer par le MiniGameManager)
**         Et une autre qui permet de verifier que le joueur à réussie à réaliser le mini jeux
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    // variable pour verifier que le mini jeu est lancer
    protected bool _isStart = false;
    
    // la fonction qui sera la pour permettre au joueur de réaliser le mini jeux
    public virtual void ToUpdate() { }

    // la fonctio qui  permet de verifier si le joueur à réussie
    public virtual bool Verif() => false;
    
    // changement du statue du mini jeux avec son inverse
    public virtual void GameStatue() { }
}
