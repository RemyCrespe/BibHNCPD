using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOpenClose : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;
   
    public void OpenPanel()
    {
        bool isActive = _panel.activeSelf;
    }

    //Tentative de cacher le bouton d'inventaire fermé

    //public void CloseButton()
    //{
    //    EventSystem.current.currentSelectedGameObject.SetActive(false);
    //}
}
