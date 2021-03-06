﻿/*******************************************
** Aucouturier Romuald
** 30-01-2020
** 
** 04-02-2020
** 
** MiniGameManager
**     Cette classe va permettre de faire la gestion de tout les Mini jeux en les instanciant
**     et en les visuellement pret pour le joueur
*******************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class MiniGameManager : Singleton<MiniGameManager>
{
    [Header("Les mini jeux avec les variantes")]
    [SerializeField] private List<ListMiniGame> _listMiniGame;

    [Header("La position dans la map pour l'instance")]
    [SerializeField] private Transform _positionInstanceMiniGame;
    
    private GameObject[] _finalList;
    private MiniGame _currentMiniGame;
    private GameObject _currentObject;

    private readonly float _timeDissolve = 2.1f;

    private RenderTexture _renderTextureCamera;

    [Header("La camera principal du jeu")]
    [SerializeField] private Camera _camera;

    [Header("La dissolution du prefab de vision de la vision du mini jeu")]
    [SerializeField] private MiniGameDissolve _dissolve;
    
    // Start is called before the first frame update
    private void Start()
    {
        // creation d'une nouvelle liste pour le choix final
        _finalList = new GameObject[_listMiniGame.Count];
        // stock la camera qui permet de voir le joueur dans une variable pour la remettre plus tard
        _camera = Camera.main;
        
        // prend 1 mini jeux de chaque list de mini jeux chacun est différent d'un autre
        for (int i = 0; i < _finalList.Length; i++)
        {
            _listMiniGame[i].Instance();
            _finalList[i] = _listMiniGame[i].GetGameObjectAle();
        }
    }

    private bool _isStart = false;
    private void Update()
    {
        // if (Input.GetMouseButtonDown(0) && !_currentObject)
        //     StartGame(0);
        //
        // if (Input.GetKey(KeyCode.E) && !_isStart)
        //     if (StartMiniGame())
        //         print("Start");
        if (Input.GetKeyDown(KeyCode.E) && _currentObject && !_isStart)
        {
            StartMiniGame();
        }

        // ne lance pas la suite si il n'y a pas de GameObject instancier ou de Mini jeu lancer
        if (!_currentMiniGame || !_currentObject || !_isStart)
        {
            return;
        }
        
        _currentMiniGame.ToUpdate(); // lance la function qui simule l'update dans les mini jeux

        if (!_currentMiniGame.Verif()) // lance la fonction ui permet de verifier si le joueur à réussi le mini jeux
        {
            return;
        }
        
        // rajouter une transiction acceptable pour le changement de camera

        // affiche dans la console que le joueur à gagner
        print("You win");
        Destroy(_currentObject); // détruit le gameObject du mini jeu
        _currentMiniGame = null; // met à null le script MiniGame et l'object
        _currentObject = null;

        _dissolve.p_dissolve = true; // lance la dissolution
        _dissolve.p_unDissolve = false;

        // afficher le nom de la competence ou du moins le nom
        var cpt = PlayerManager.P_instance.AddCompetence();
        // et afficher le message de prise de conscience avec une coroutine qui as la fin de la prise de conscience
        // affiche le nom de la compétence et ça description

        StartCoroutine(WriteTheAwareness(cpt, 0));

        StartMiniGameToSwitchCamera();
    }

    private Inventory _inventory;
    
    private void StartGame(int id)
    {
        var ressources = _listMiniGame[id];
        var nameRessource = ressources.GetNameRessourceNecessary();

        print("Enter on Start");
        
        var index = 1;
        if (!_inventory.GetPositionByName(nameRessource, out index))
        {
            return;
        }

        var quantityInventory = _inventory.GetRessourceQuantity(index);
        var quantityNecesary = ressources.GetQuantity();

        if (quantityInventory < quantityNecesary)
        {
            print(quantityNecesary + "    " + quantityInventory + "    " + nameRessource + "    " + index);
            print("Ressource non suffisante");
            return;
        }
        
        _inventory.SetRessourceQuantity(index, quantityInventory - quantityNecesary);
        CreateMiniGame(id);
    }

    private IEnumerator WriteTheAwareness(Competence cp, float time)
    {
        // afficher le message de prise de conscience
        // utiliser les animations ou déplacer l'object en fonction de la taille de l'écran
        // plus simple avec les animations
        
        // voir si on ne met pas le temps avec la competence
        // possibiliter de laisser plus longtemps en fonction du texte écrit
        yield return new WaitForSeconds(time);
        
        // ne plus afficher le message mais afficher la competence obtenu et comment l'utiliser
        // ajouter un effet de fondu pour l'apparition et la disparition pour la competence
    }

    // permet de commencer un mini en instanciant le prefab et en le mettant à la position voulu
    // et récupère le MiniGame script pour éviter de devoir le faire dans le update (gain de performance)
    private bool CreateMiniGame(int id)
    {
        // instantie le gameObject du mini jeu
        _currentObject = Instantiate(_finalList[id]);
        // Change ça position pour ne pas qu'il soit visible sur la map
        _currentObject.transform.position = _positionInstanceMiniGame.position;

        _dissolve.p_unDissolve = true;
        _dissolve.p_dissolve = false;
        
        print("dissolve => " + _dissolve);

        // retourne la verification si aucun des object n'est à null
        return _currentObject;
    }

    private bool StartMiniGame()
    {
        // recuperer le script qui herite de MiniGame
        _currentMiniGame = _currentObject.GetComponent<MiniGame>();

        Camera ca;
        if (ca = _currentObject.GetComponent<Camera>())
        {
            StartMiniGameToSwitchCamera(ca);
            return _currentMiniGame;
        }
        
        // parcour les enfants de l'object pour prendre la camera
        for (int i = 0; i < _currentObject.transform.childCount; i++)
        {
            // Si l'enfant n'a pas de camera continue le for en passant la suite
            if (!(ca = _currentObject.transform.GetChild(i).GetComponent<Camera>()))
            {
                continue;
            }

            StartMiniGameToSwitchCamera(ca);
            return _currentMiniGame;
        }

        _isStart = true;
        _currentMiniGame.GameStatue();
        return _currentMiniGame;
    }

    private void SwitchCamera(Camera ca = null)
    {
        if (ca)
        {
            // si la camera a une render texture la garde pour quand le joueur
            // va quitter le mini jeu et l'enleve de la camera
            if (ca.targetTexture)
            {
                _renderTextureCamera = ca.targetTexture;
                ca.targetTexture = null;
            }
            
            // desactive la camera courante pour ne pas avoir plusieur audio listener
            _camera.gameObject.SetActive(false);
            // change la camera courante et quitte la boucle for
            Camera.SetupCurrent(ca);
        }
        else
        {
            // desactive la camera courante pour ne pas avoir plusieur audio listener
            _camera.gameObject.SetActive(true);
            // change la camera courante et quitte la boucle for
            Camera.SetupCurrent(_camera);
        }
    }

    // voir comment on on va faire pour la transition de la camera
    private void StartMiniGameToSwitchCamera(Camera ca = null)
    {
        SwitchCamera(ca);
        _isStart = !_isStart;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        print("Enter stay");
        
        if (other.CompareTag("Player"))
        {
            
            print("It's player");
            var recolt = other.GetComponent<PlayerController>();

            _inventory = other.GetComponent<Inventory>();

            if (recolt)
            {
                StartGame(recolt.GetRecolt().GetId());
            }
        }
    }
}

// classe permettant de mettre les mini avec leurs variantes
[System.Serializable]
public class ListMiniGame
{
    // la liste qui stock les GameObject des mini jeux
    [SerializeField] private List<GameObject> _list;
    [SerializeField] private string _nameRessource;
    [SerializeField] private int _max, _min;

    private int _quantity;

    public void Instance() => _quantity = Random.Range(_min, _max);

    // retourne un gameobject pris aléatoirement par l'ordinateur
    public GameObject GetGameObjectAle() => _list[Random.Range(0, _list.Count)];
    public string GetNameRessourceNecessary() => _nameRessource;
    public int GetQuantity() => _quantity;
}