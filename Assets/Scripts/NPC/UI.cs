/********************
     * LEBLOND Antoine
     * 22/01/2020
     * LEBLOND Antoine
     * Affiche une image en fonction de l'entité visée, affichage du cooldown minage si on récupère une ressource, interaction entre les entités
     * Image de la pioche, hache, le slider du cooldown du minage, les textes des inventaires, le raycast et son point de collision
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
    private Text _pinkQuartzInventory;

    [SerializeField]
    private Text _ironInventory;

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

        _pinkQuartzInventory.text = "Pink Quartz : " + _playerInventory.GetRessourceQuantity(0).ToString();
        _ironInventory.text = "Iron : " + _playerInventory.GetRessourceQuantity(1).ToString();

        Vector2 _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        _interactiveImage.transform.position = _mousePosition;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit))
        {
            if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("Ressource") ||
                _hit.transform.gameObject.layer == LayerMask.NameToLayer("NPC") ||
                _hit.transform.gameObject.layer == LayerMask.NameToLayer("Base"))
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
                        switch (_hit.transform.gameObject.tag)
                        {
                            case "Pink Quartz":
                            case "Iron":
                                _playerController.PickupRessource(_hit);
                                break;
                            case "NPC":
                                NPC npc = _hit.transform.gameObject.GetComponent<NPC>();
                                npc.Dialogue();
                                break;
                            case "Base":
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