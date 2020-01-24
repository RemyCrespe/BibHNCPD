using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPoint : MonoBehaviour
{
    [SerializeField]
    private Resource _resource;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private float _timeMining;

    private int _resourceTake = 0;

    public int GetValidRessource()
    {
        return _resource.GetCount() - _resourceTake;
    }

    public void AddResourceTake(int take)
    {
        _resourceTake += take;
    }

    public int GetRessource(int size)
    {
        if (_resource.GetCount() >= size)
        {
            _resource.GetResource(-size);
            return size;
        }

        int _return = _resource.GetCount();
        _resource.GetResource(_return);
        return _return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layerMask) != 0)
        {
            AutoNPC npc = other.gameObject.GetComponent<AutoNPC>();

            npc.MoveNpc(other.transform.position);
            npc.StartMining(_timeMining, this);
        }
    }
}   
