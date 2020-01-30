/*******************************************
** Aucouturier Romuald
** 13-01-2020
** 
** 24-01-2020
** 
** NPC
** Sert pour la création d'un pnj avec la possibilité de lui donner une
** competence
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// mettre ce require pour ne pas avoir de npc sans NavMeshAgent
[RequireComponent(typeof(NavMeshAgent))]
public class AutoNPC : MonoBehaviour
{
    [SerializeField]
    private Mesh[] _meshLevel;

    [SerializeField]
    private Material _normal;
    [SerializeField]
    private Material _select;

    [SerializeField]
    private int _resourceTake = 1;
    [SerializeField]
    private Image _miningTime;

    private float _initialSpeed;

    public NpcJobs P_npcJobs = NpcJobs.Miner;
    public bool P_goHome { get; private set; }
    public bool P_haveMision = false;

    private Animator _animator;
    private MeshRenderer _meshRenderer;
    private NavMeshAgent _agent;

    public Resource P_inventory { get; private set; }

    public int P_id;

    private void Start()
    {
        // creer une ressource
        P_inventory = new Resource();

        // recuperztion de tout les composants necessaire
        _animator = GetComponent<Animator>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _agent = GetComponent<NavMeshAgent>();

        // met le visuel pour voir le temps de minage à 0
        _miningTime.enabled = false;

        // dis que le pnj ne va pas à la base
        P_goHome = false;

        // prend la valeur de speed par defaut du npc
        _initialSpeed = _agent.speed;
    }

    private bool _isSpeedChange = false;
    public void ChangeSpeed(float multiple, float time)
    {
        if (multiple < 0 || multiple > 1 || _isSpeedChange)
        {
            return;
        }

        _agent.speed *= multiple;

        _isSpeedChange = true;
        Invoke("EndDebuff", time);
    }

    private void EndDebuff()
    {
        _agent.speed = _initialSpeed;
        _isSpeedChange = false;
    }

    public void Select()
    {
        // change le material pour mettre celui pour selectionner
        _meshRenderer.material = _select;
    }

    public void SetJobs(NpcJobs newJobs)
    {
        // possibiliter de changer le job du pnj
        P_npcJobs = newJobs;
    }

    public void UnSelect()
    {
        // change le material pour quand le robot n'est pas séléctionner
        _meshRenderer.material = _normal;
    }

    public int GetCapacity()
    {
        // retourne la capacite de ressource que le robot posséde 
        return _resourceTake;
    }
    
    public void MoveNpc(Vector3 newPosition)
    {
        // change la destination du joueur avec une nouvelle position
        _agent.SetDestination(newPosition);
    }

    
    private void Update()
    {
        // verifie si le joueur mine et qu'il lui reste du temps
        if (_timer < _time && _isMining)
        {
            _timer += Time.deltaTime;
            _miningTime.fillAmount = _timer / _time;
        }
        else if (_timer > _time && _isMining)
        { // si il mine mais que le temps et fini
            EndMining(); // à fini de miner
        }

        // Si le joueur peut miner le script va vérifier la distance entre les deux
        if (_goMining)
        {
            // si elle est plus petit lance le minage
            float distance = Vector3.Distance(transform.position, _oreDestination);
            if (distance <= _distanceMinerais)
            {
                StartMining(3); // lance le minage
                _agent.ResetPath(); // ne lui donne plus de destination
                _goMining = false; // ne bouge plus pour aller miner
            }
        }
    }

    [SerializeField]
    private float _distanceMinerais = 0.5f;
    private Vector3 _oreDestination;
    private bool _goMining = false;
    private CollectPoint _collectPoint;
    public void AddCollectPoint(CollectPoint collectPoint)
    { // recupere un collect point pour aller dans ça direction
        _goMining = true;
        _oreDestination = collectPoint.transform.position;
        _collectPoint = collectPoint;
        MoveNpc(_oreDestination);
    }
    
    private bool _isMining = false;
    private float _time = 0;
    private float _timer = 0;
    /* Commence le minage
     *      reset la position du joueur (ne lui donne plus de destination)
     *      affiche le visuel pour voir le temps qu'il reste au pnj
     */
    public void StartMining(float time)
    {
        _agent.ResetPath();
        _miningTime.enabled = true;

        _time = time;
        _isMining = true;
    }

    /* Permet de lancer la fin du minage 
     *      change la quantité dans l'inventaire du pnj
     *      ensuite le point de collect verify si il n'est pas vide
     *      retourne à la maison
     */
    public void EndMining()
    {
        _miningTime.enabled = false;

        P_inventory.ChangeCount(_collectPoint.GetRessource(_resourceTake));

        _collectPoint.VerifIsNotEmpty();

        _timer = 0;
        _isMining = false;

        MoveNpc(NpcManager.P_instance.HomePosition());
        P_goHome = true;
    }
}

// un enum pour la liste des jobs pour les auto npc
public enum NpcJobs
{
    Miner,
    Forester,
    Player
}