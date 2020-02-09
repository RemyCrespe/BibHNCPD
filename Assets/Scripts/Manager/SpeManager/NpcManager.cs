/*******************************************
** Aucouturier Romuald
** 13-01-2020
** 
** 17-01-2020
** 
** NpcManager
** Sert à gérer les npc séléctionner et leurs
** donner un ordre ou une direction à faire
*******************************************/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcManager : Singleton<NpcManager>
{
    [SerializeField]
    private LayerMask _maskNpc;
    [SerializeField]
    private GameObject _ContaintNpc;

    [SerializeField]
    private bool _moveAutoNpc = false;

    private List<AutoNPC> _npcList = new List<AutoNPC>();
    private List<AutoNPC> _npcListSelect = new List<AutoNPC>();

    [SerializeField]
    private List<ressourceToJobs> _tempRessourceToJobs;
    private Dictionary<ResourceType, NpcJobs> _ressourcesToJobsDico = new Dictionary<ResourceType, NpcJobs>();

    private bool _playerIsOnBase = true;
    private List<Resource> _inventory = new List<Resource>();

    // Start is called before the first frame update
    void Start()
    {
        Transform containtTransformNpc = _ContaintNpc.transform;
        for (int i = 0; i < containtTransformNpc.childCount; i++)
        {
            Transform transformNpc = containtTransformNpc.GetChild(i);

            AutoNPC nPC = transformNpc.GetComponent<AutoNPC>();
            if (nPC)
            {
                _npcList.Add(nPC);
            }
        }

        for (int i = 0; i < _npcList.Count; i++)
        {
            _npcList[i].P_id = i;
        }

        _npcListSelect.Clear();

        for (int i = 0; i < _tempRessourceToJobs.Count; i++)
        {
            _ressourcesToJobsDico.Add(_tempRessourceToJobs[i].P_ressourceType, _tempRessourceToJobs[i].P_npcJobs);
        }

        _tempRessourceToJobs = null;
    }

    [SerializeField] private string _tagEnnemy;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag(_tagEnnemy))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.transform.GetComponent<Ennemy>().EnnemiesDead();
                }
            }
            
        //    if (((1 << hit.transform.gameObject.layer) & _maskNpc) != 0)
        //    {
        //        AutoNPC npc = hit.transform.gameObject.GetComponent<AutoNPC>();
        //        if (Input.GetMouseButtonDown(0) && npc != null && _moveAutoNpc)
        //        {
        //            var id = npc.P_id;

        //            int pos = SuppNocSelect(id);
        //            if (pos != -1)
        //            {
        //                UnselectNpc(pos);
        //            }
        //            else
        //            {
        //                AddSelectNpc(id);
        //            }
        //        }
        //    }

        //    if (Input.GetMouseButton(1) && _moveAutoNpc)
        //    {
        //        PlayerGoToRecolt(hit.point);
        //    }
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    ResetNpcSelect();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _maskNpc) == 0)
        {
            return;
        }

        AutoNPC nPC = other.GetComponent<AutoNPC>();
        if (!nPC || nPC.P_inventory.GetCount() <= 0)
        {
            return;
        }

        Resource resources = nPC.P_inventory;

        int verif = VerifIfContaintRessource(resources);
        if (verif != -1)
        {
            _inventory[verif].GetResource(resources.GetCount());
        }
        else
        {
            Resource newResources = new Resource();

            newResources.SetOreType(resources.GetOreType());
            newResources.SetResourceType(resources.GetResourceType());
            newResources.GetResource(resources.GetCount());

            print(newResources.GetCount());

            _inventory.Add(newResources);
        }
    }

    public void ReturnHome(ResourceType ressourcesType)
    {
        foreach (var item in _npcList)
        {
            item.P_haveMision = false;
        }

        GoAfterThePlayer(_home.position, ressourcesType);
    }

    private int VerifIfContaintRessource(Resource resource)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].GetResourceType() != resource.GetResourceType())
            {
                continue;
            }
            
            if (_inventory[i].GetResourceType() != ResourceType.Ores)
            {
                return i;
            }

            if (_inventory[i].GetOreType() == resource.GetOreType())
            {
                return i;
            }
        }

        return -1;
    }

    private void UnselectNpc(int pos)
    {
        _npcListSelect[pos].UnSelect();
        _npcListSelect.RemoveAt(pos);
    }

    private int SuppNocSelect(int id)
    {
        for (int i = 0; i < _npcListSelect.Count; i++)
        {
            if (_npcListSelect[i].P_id == id)
            {
                return i;
            }
        }

        return -1;
    }

    public void GoAfterThePlayer(Vector3 position, ResourceType ressourcesType)
    {
        foreach (var t in _npcList)
        {
            if (t.P_npcJobs != _ressourcesToJobsDico[ressourcesType] || t.P_goHome)
            {
                continue;
            }
            
            Vector3 newDestination = GenerateApproximativePosition(position, _sizePosition);
            t.MoveNpc(newDestination);
        }
    }

    private void AddSelectNpc(int id)
    {
        foreach (var t in _npcList.Where(t => t.P_id == id))
        {
            t.Select();
            _npcListSelect.Add(t);
        }
    }

    [SerializeField]
    private Transform _home;
    [SerializeField]
    private float _sizeHome = 3;
    public Vector3 HomePosition()
    {
        return GenerateApproximativePosition(_home.position, _sizeHome);
    }

    private void ResetNpcSelect()
    {
        foreach (var t in _npcListSelect)
        {
            t.UnSelect();
        }

        _npcListSelect.Clear();
    }

    [SerializeField] private List<float> _coeffChangeSpeedWeather;
    public void ChangeSpeedByEvent(int id)
    {
        if (_coeffChangeSpeedWeather[id] == 1)
        {
            return;
        }
        
        foreach (var it in _npcList)
        {
            it.ChangeSpeed(_coeffChangeSpeedWeather[id], 31);
        }
    }

    [SerializeField]
    private float _distanceHelpPlayer = 10;
    public void PlayerGoToRecolt(Vector3 position)
    {
        foreach (var t in _npcList)
        {
            if (!(Vector3.Distance(t.transform.position, position) <= _distanceHelpPlayer))
            {
                continue;
            }

            Vector3 newDestination = GenerateApproximativePosition(position, _sizePosition);
            t.MoveNpc(newDestination);
        }
    }

    [SerializeField]
    private float _sizePosition = 0.5f;
    // Permet de générer une position aléatoire dans une zone p our ne pas que tout les pnj
    // soit au même endroit
    private Vector3 GenerateApproximativePosition(Vector3 position, float size)
    {
        Vector3 randomPos = new Vector3();

        float min = size * -1;
        float max = size;

        randomPos.x = position.x + Random.Range(min, max);
        randomPos.z = position.z + Random.Range(min, max);

        return randomPos;
    }
}

[System.Serializable]
struct ressourceToJobs
{
    public ResourceType P_ressourceType;
    public NpcJobs P_npcJobs;
}