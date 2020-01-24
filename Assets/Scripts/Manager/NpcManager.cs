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

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
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
        //}

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    ResetNpcSelect();
        //}
    }

    public void AddTaskForAutoNpc(List<CollectPoint> resourceList)
    {
        for (int i = 0; i < _npcList.Count; i++)
        {
            AutoNPC nPC = _npcList[i];
            if (!nPC.P_goHome)
            {
                int takeRessource = Random.Range(0, resourceList.Count - 1);
                if (nPC.GetCapacity() <= resourceList[takeRessource].GetValidRessource())
                {
                    nPC.MoveNpc(resourceList[takeRessource].transform.position);
                    resourceList[takeRessource].AddResourceTake(nPC.GetCapacity());
                }
                else
                {
                    for (int y = 0; y < resourceList.Count; y++)
                    {
                        if (nPC.GetCapacity() <= resourceList[y].GetValidRessource())
                        {
                            nPC.MoveNpc(resourceList[y].transform.position);
                            resourceList[y].AddResourceTake(nPC.GetCapacity());
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _maskNpc) != 0)
        {
            AutoNPC nPC = other.GetComponent<AutoNPC>();
            if (nPC && nPC.P_inventory.GetCount() > 0)
            {
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
        }
    }

    public void ReturnHome(ResourceType ressourcesType)
    {
        GoAfterThePlayer(_home.position, ressourcesType);
    }

    private int VerifIfContaintRessource(Resource resource)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].GetResourceType() == resource.GetResourceType())
            {
                if (_inventory[i].GetResourceType() != ResourceType.Ores)
                {
                    return i;
                }

                if (_inventory[i].GetOreType() == resource.GetOreType())
                {
                    return i;
                }
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
        for (int i = 0; i < _npcList.Count; i++)
        {
            if (_npcList[i].P_npcJobs == _ressourcesToJobsDico[ressourcesType])
            {
                Vector3 newDestination = GenerateApproximativePosition(position, _sizePosition);
                _npcList[i].MoveNpc(newDestination);
            }
        }
    }

    private void AddSelectNpc(int id)
    {
        for (int i = 0; i < _npcList.Count; i++)
        {
            if (_npcList[i].P_id == id)
            {
                _npcList[i].Select();
                _npcListSelect.Add(_npcList[i]);
            }
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
        for (int i = 0; i < _npcListSelect.Count; i++)
        {
            _npcListSelect[i].UnSelect();
        }

        _npcListSelect.Clear();
    }

    [SerializeField]
    private float _distanceHelpPlayer = 10;

    public void PlayerGoToRecolt(Vector3 position)
    {
        for (int i = 0; i < _npcList.Count; i++)
        {
            if (Vector3.Distance(_npcList[i].transform.position, position) <= _distanceHelpPlayer)
            {
                Vector3 newDestination = GenerateApproximativePosition(position, _sizePosition);
                _npcList[i].MoveNpc(newDestination);
            }
        }
    }

    private bool NpcHaveTheGoodJob(NpcJobs jobs, ResourceType ressourcesType)
    {
        if (ressourcesType == ResourceType.Ores)
        {
            return jobs == NpcJobs.Miner;
        }

        if (ressourcesType == ResourceType.Wood)
        {
            return jobs == NpcJobs.Forester;
        }


        return false;
    }

    [SerializeField]
    private float _sizePosition = 0.5f;
    private Vector3 GenerateApproximativePosition(Vector3 position, float size)
    {
        Vector3 randomPos = new Vector3();

        float min = size * -1;
        float max = size;

        randomPos.x = position.x + Random.Range(min, max);
        randomPos.z = position.z + Random.Range(min, max);

        return randomPos;
    }


    public void AddNpc(AutoNPC npc)
    {
        _npcList.Add(npc);
    }
}

[System.Serializable]
struct ressourceToJobs
{
    public ResourceType P_ressourceType;
    public NpcJobs P_npcJobs;
}