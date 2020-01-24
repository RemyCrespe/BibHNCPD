/*******************************************
** Aucouturier Romuald
** 16-01-2020
** 
** 17-01-2020
** 
** Resources
** la class abstrait pour créer tout les types
** de ressources
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField]
    private ResourceType _ressourcesType;

    [SerializeField]
    private LayerMask _layerMask;

    private List<CollectPoint> _resourceList = new List<CollectPoint>();
    [SerializeField]
    private bool _playerGoOnSource = false;


    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            CollectPoint childRessources = child.GetComponent<CollectPoint>();
            if (childRessources)
            {
                _resourceList.Add(childRessources);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            _playerGoOnSource = !_playerGoOnSource;

            if (_playerGoOnSource)
            {
                NpcManager.P_instance.GoAfterThePlayer(transform.position, _ressourcesType);
            }
            else
            {
                NpcManager.P_instance.ReturnHome(_ressourcesType);
            }
        }

        if (((1 << other.gameObject.layer) & _layerMask) != 0)
        {
            AutoNPC nPC = other.GetComponent<AutoNPC>();
            if (!nPC.P_goHome)
            {
                int takeRessource = Random.Range(0, _resourceList.Count - 1);
                if (nPC.GetCapacity() <= _resourceList[takeRessource].GetValidRessource())
                {
                    nPC.MoveNpc(_resourceList[takeRessource].transform.position);
                    _resourceList[takeRessource].AddResourceTake(nPC.GetCapacity());
                }
                else
                {
                    for (int y = 0; y < _resourceList.Count; y++)
                    {
                        if (nPC.GetCapacity() <= _resourceList[y].GetValidRessource())
                        {
                            nPC.MoveNpc(_resourceList[y].transform.position);
                            _resourceList[y].AddResourceTake(nPC.GetCapacity());
                        }
                    }
                }
            }
        }
    }

    // add possibility to verif the destination of player leave
}
