using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameImage : MonoBehaviour
{
    [SerializeField]
    private int _index;

    public int GetIndex()
    {
        return _index;
    }
}
