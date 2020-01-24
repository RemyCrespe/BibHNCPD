/*******************************************
** Aucouturier Romuald
** 13-01-2020
** 
** 17-01-2020
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
    private int _resosurceTake = 1;
    [SerializeField]
    private Image _miningTime;

    public NpcJobs P_npcJobs = NpcJobs.Miner;
    public bool P_goHome { get; private set; }

    private Animator _animator;
    private MeshRenderer _meshRenderer;
    private NavMeshAgent _agent;

    public Resource P_inventory { get; private set; }

    public int P_id;

    private void Start()
    {
        P_inventory = new Resource();

        _animator = GetComponent<Animator>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _agent = GetComponent<NavMeshAgent>();

        _miningTime.enabled = false;

        P_goHome = false;

        NpcManager.P_instance.AddNpc(this);
    }

    public void Select()
    {
        _meshRenderer.material = _select;
    }

    public void SetJobs(NpcJobs newJobs)
    {
        P_npcJobs = newJobs;
    }

    public void UnSelect()
    {
        _meshRenderer.material = _normal;
    }

    public int GetCapacity()
    {
        return _resosurceTake;
    }

    public void MoveNpc(Vector3 newPosition)
    {
        _agent.SetDestination(newPosition);
    }

    
    private void Update()
    {
        if (_timer < _time && _isMining)
        {
            _timer += Time.deltaTime;
            _miningTime.fillAmount = _timer / _time;
        }
        else if (_timer > _time && _isMining)
        {
            EndMining();
        }
    }

    private CollectPoint _collectPoint;
    private bool _isMining = false;
    private float _time = 0;
    private float _timer = 0;
    public void StartMining(float time, CollectPoint collectPoint)
    {
        _agent.ResetPath();
        _miningTime.enabled = true;

        _collectPoint = collectPoint;
        _time = time;
        _isMining = true;
    }

    public void EndMining()
    {
        _miningTime.enabled = false;

        // stop the mining animation
        // _animator.SetBool("", false); // exemple

        P_inventory.ChangeCount(_collectPoint.GetRessource(P_inventory.GetCapacity()));

        _collectPoint.gameObject.SetActive(false);

        // _resources = null;
        _timer = 0;
        _isMining = false;

        MoveNpc(NpcManager.P_instance.HomePosition());
        print("return home");
        P_goHome = true;
    }
}

public enum NpcJobs
{
    Miner,
    Forester,
    Player
}