/********************
 * LEBLOND Antoine
 * 22/01/2020
 * LEBLOND Antoine
 * Affiche une image en fonction de l'entité visée, affichage du cooldown minage si on récupère une ressource, interaction entre les entités
 * Image de la pioche, le slider du cooldown du minage, les textes des inventaires, le raycast et son point de collision
 * ******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Sprite _pickaxeSprite;

    [SerializeField]
    private Sprite _baseSprite;

    [SerializeField]
    private Sprite _dialogueSprite;

    [SerializeField]
    private GameObject _interactiveImage;

    [SerializeField]
    private GameObject _miningCooldown;

    [SerializeField]
    private Text _oreInventory;

    [SerializeField]
    private Text _woodInventory;

    PlayerController _playerController;
    Inventory _playerInventory;
    Slider _miningCooldownSlider;

    Ray ray;
    RaycastHit _hit;

    void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _playerInventory = _player.GetComponent<Inventory>();
        _miningCooldownSlider = _miningCooldown.GetComponent<Slider>();
    }

    void Update()
    {
        if (_playerController.P_isPicking)
        {
            _miningCooldown.SetActive(true);
            _miningCooldownSlider.value += 1 / _playerController.P_pickupSpeed * Time.deltaTime;
        }
        else
        {
            _miningCooldown.SetActive(false);
            _miningCooldownSlider.value = 0;
        }

        _oreInventory.text = "Pink Quartz : " + _playerInventory.GetNbRessource(1).ToString();
        _woodInventory.text = "Iron : " + _playerInventory.GetNbRessource(2).ToString();

        Vector2 _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        _interactiveImage.transform.position = _mousePosition;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit))
        {
            if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("Ressource") || _hit.transform.gameObject.layer == LayerMask.NameToLayer("Base") || _hit.transform.gameObject.layer == LayerMask.NameToLayer("NPC")) //Layer Ressource, NPC, Base
            {
                switch (_hit.collider.tag)
                {
                    case "Pink Quartz":
                    case "Iron":
                        _interactiveImage.GetComponent<Image>().sprite = _pickaxeSprite;
                        break;
                    case "Base":
                        _interactiveImage.GetComponent<Image>().sprite = _baseSprite;
                        break;
                    case "NPC":
                        _interactiveImage.GetComponent<Image>().sprite = _dialogueSprite;
                        break;
                }
                _interactiveImage.SetActive(true);
                if (_playerController.IsCloseToEntity(_hit))
                {
                    _interactiveImage.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
                    if (Input.GetMouseButtonDown(1))
                    {
                        switch (_hit.transform.gameObject.layer)
                        {
                            case 11: //Layer Ressource
                                _playerController.PickupRessource(_hit);
                                break;
                            case 9: //Layer NPC
                                _hit.transform.gameObject.GetComponent<NPC>().Dialogue();
                                break;
                            case 8: //Layer Base
                                _playerController.AddRessourceToTarget(_hit);
                                break;
                        }
                    }
                }
                else
                {
                    _interactiveImage.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                }
            }
            else
            {
                _interactiveImage.SetActive(false);
            }
        }
        else
        {
            _interactiveImage.SetActive(false);
        }
    }
}