/*******************************************
** Aucouturier Romuald
** 16-01-2020
** 
** 24-01-2020
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

    private bool _playerGoOnSource = false;

    [SerializeField]
    private Verif[] _verifRaycast;

    private void Start()
    {
        // récuperer tout les enfants du gameObject et verifie si
        // c'est bien un point de collect si oui rajoute dans la liste de gestion
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            CollectPoint childRessources = child.GetComponent<CollectPoint>();
            if (childRessources)
            {
                _resourceList.Add(childRessources);
            }
        }

        // creer le ray pour chaque possibiliter
        foreach (var item in _verifRaycast)
        {
            item.CreateRay();
        }
    }

    private void Update()
    {
        int position = 0;
        if (_playerGoOnSource)
        {
            position = 1;
        }
        
        Ray ray = _verifRaycast[position].P_Ray;

        Debug.DrawRay(ray.origin, ray.direction * _verifRaycast[position].P_distance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction * _verifRaycast[position].P_distance, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                if (!_playerGoOnSource)
                {
                    NpcManager.P_instance.GoAfterThePlayer(transform.position, _ressourcesType);
                }
                else
                {
                    NpcManager.P_instance.ReturnHome(_ressourcesType);
                }

                _playerGoOnSource = !_playerGoOnSource;
            }
            else if (((1 << hit.transform.gameObject.layer) & _layerMask) != 0 && _playerGoOnSource)
            {
                AutoNPC nPC = hit.transform.GetComponent<AutoNPC>();
                if (!nPC.P_haveMision)
                {
                    int takeRessource = Random.Range(0, _resourceList.Count - 1);
                    if (nPC.GetCapacity() <= _resourceList[takeRessource].GetValidRessource())
                    {
                        nPC.AddCollectPoint(_resourceList[takeRessource]);
                        _resourceList[takeRessource].AddResourceTake(nPC.GetCapacity());
                    }
                    else
                    {
                        for (int y = 0; y < _resourceList.Count; y++)
                        {
                            if (nPC.GetCapacity() <= _resourceList[y].GetValidRessource())
                            {
                                nPC.AddCollectPoint(_resourceList[y]);
                            }
                        }
                    }

                    nPC.P_haveMision = true;
                }
            }
        }
    }
}

[System.Serializable]
public class Verif
{
    public string P_name;

    public Transform P_start;
    public Transform P_end;

    public float P_distance { get; private set; }
    public Ray P_Ray { get; private set; }

    // creer la distance pour la meme raison que le ray
    private void CreateDistance()
    {
        P_distance = Vector3.Distance(P_start.position, P_end.position);
    }

    // Créer le ray pour ne pas le recréer à chaque frame
    public void CreateRay()
    {
        P_Ray = new Ray(P_start.position, P_end.position - P_start.position);
        CreateDistance();
    }
}
