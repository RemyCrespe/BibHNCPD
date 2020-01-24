using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject _openInventoryMenu;
    [SerializeField]
    private GameObject _closeInventoryMenu;

    private Button _openInventory;
    private Button _closeInventory;

    private void Start()
    {
        _openInventory = _openInventoryMenu.GetComponent<Button>();
        _closeInventory = _closeInventoryMenu.GetComponent<Button>();

        _openInventory.onClick.AddListener(delegate { CloseInventory(); });
        _closeInventory.onClick.AddListener(delegate { OpenInventory(); });

        OpenInventory();
    }

    private void OpenInventory()
    {
        _openInventoryMenu.SetActive(true);

        _openInventoryMenu.GetComponent<Animator>().SetTrigger("Open");

        _closeInventoryMenu.SetActive(false);
    }

    private void CloseInventory()
    {
        _openInventoryMenu.SetActive(false);

        _openInventoryMenu.GetComponent<Animator>().SetTrigger("Close");

        _closeInventoryMenu.SetActive(true);
    }
}