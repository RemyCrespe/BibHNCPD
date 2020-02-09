/*******************************************
** Aucouturier Romuald
** 20-01-2020
** 
** 24-01-2020
** 
** CollectPoint
**     Sert à gérer les points de récoltes comme pour
**     le bois, les minerais ou autre avec une quantité de ressources prédéfinie
**     et au bout d'un temps aléatoire entre 2 valeurs
**     la ressources réaparé si tout à était pris par les pnj
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPoint : MonoBehaviour
{
    [SerializeField]
    private Resource _resource;

    [SerializeField]
    private float _minBeforeRespawn = 3f;
    [SerializeField]
    private float _maxBeforeRespawn = 5f;
    private float _timeBeforeRespawn;
    private float _timer;
    private bool _isDestroy = false;

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    private int _resourceTake = 0;

    private void Start()
    {
        // Recuperation des composants nécessaire
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    public int GetValidRessource()
    {
        // retourne la quantité de ressources qui ne sont pas réservé ou disponible
        return _resource.GetCount() - _resourceTake;
    }

    public void AddResourceTake(int take)
    {
        // rajoute des ressources réservé
        _resourceTake += take;
    }

    public int GetRessource(int size)
    {
        // retourne la quantité de ressource que le joueur peut prendre ou recois de la ressource
        if (_resource.GetCount() > size)
        {
            _resource.GetResource(size);
            return size;
        }

        var returnValue = _resource.GetCount();
        _resource.GetResource(returnValue);
        return returnValue;
    }

    public void VerifIsNotEmpty()
    {
        // verifie si la ressource n'est pas vide
        if (_resource.GetCount() > 0)
        {
            return; // si oui met un temp aléatoire avant la possibiliter de reprendre la ressource
        }
        
        _timeBeforeRespawn = Random.Range(_minBeforeRespawn, _maxBeforeRespawn);
        _isDestroy = true;
        ChangeVisibility(false);
    }

    private void ChangeVisibility(bool visibility)
    {
        // change la visibilité du gameObject
        _meshRenderer.enabled = visibility;
        _collider.enabled = visibility;
    }

    private void Update()
    {
        // si le point de collect est vide et que le temps avant réaparition n'est pas atteint
        // incrément le temps
        if (_isDestroy && _timeBeforeRespawn > _timer)
        {
            _timer += Time.deltaTime;
        }
        // sinon reset l'object et l'affiche de nouveau
        else if (_isDestroy && _timeBeforeRespawn < _timer)
        {
            _isDestroy = false;
            ChangeVisibility(true);
            _resource.Reset();
            _resourceTake = 0;
        }
    }
}   
