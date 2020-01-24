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
    public GameObject P_player;

    public Sprite P_pickaxeSprite;
    public Sprite P_baseSprite;
    public Sprite P_dialogueSprite;

    public GameObject P_interactiveImage;

    public GameObject P_miningCooldown;

    public Text P_oreInventory;
    public Text P_woodInventory;

    Ray ray;
    RaycastHit _hit;

    void Update()
    {
        if (P_player.GetComponent<PlayerController>().P_isPicking) //If the player is picking, display the cooldown
        {
            P_miningCooldown.SetActive(true);
            P_miningCooldown.GetComponent<Slider>().value += 1 / P_player.GetComponent<PlayerController>().P_pickupSpeed * Time.deltaTime; //Increment slider value with time and mining speed
        }
        else //If not, don't display the cooldown
        {
            P_miningCooldown.SetActive(false); 
            P_miningCooldown.GetComponent<Slider>().value = 0;
        }

        P_oreInventory.text = "Pink Iron : " + P_player.GetComponent<Inventory>()._nbPinkOre.ToString(); //Simple inventory UI for ores
        P_woodInventory.text = "Iron : " + P_player.GetComponent<Inventory>()._nbIron.ToString(); //Simple inventory UI for wood

        Vector2 _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Check the mouse position

        P_interactiveImage.transform.position = _mousePosition; //Display tool image at the mouse position

        ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Return the ray going from the camera to the mouse position

        if (Physics.Raycast(ray, out _hit))
        {
            if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("Ressource") || _hit.transform.gameObject.layer == LayerMask.NameToLayer("Base") || _hit.transform.gameObject.layer == LayerMask.NameToLayer("NPC"))
            {
                switch (_hit.collider.name) //Change image depending on the object name
                {
                    case "PinkIron":
                        P_interactiveImage.GetComponent<Image>().sprite = P_pickaxeSprite;
                        break;
                    case "Iron":
                        P_interactiveImage.GetComponent<Image>().sprite = P_pickaxeSprite;
                        break;
                    case "Base":
                        P_interactiveImage.GetComponent<Image>().sprite = P_baseSprite;
                        break;
                    case "NPCRobot":
                        P_interactiveImage.GetComponent<Image>().sprite = P_dialogueSprite;
                        break;
                }
                P_interactiveImage.SetActive(true);
                if (P_player.GetComponent<PlayerController>().IsCloseToEntity(_hit)) //If the player is close to the target he is pointing
                {
                    P_interactiveImage.GetComponent<Image>().color = new Color32(0, 0, 0, 255); //Change the alpha
                    if (Input.GetMouseButtonDown(1))
                    {
                        switch (_hit.transform.gameObject.layer) //Depending on the layer of the target, do something
                        {
                            case 8: //Layer Base
                                P_player.GetComponent<PlayerController>().DropRessourceToTarget(_hit);
                                break;
                            case 11: //Layer Ressource
                                P_player.GetComponent<PlayerController>().PickupRessource(_hit);
                                break;
                            case 9: //Layer PNJ
                                _hit.transform.gameObject.GetComponent<NPC>().Dialogue();
                                break;
                        }
                    }
                }
                else
                {
                    P_interactiveImage.GetComponent<Image>().color = new Color32(0, 0, 0, 100); //If the player is too far, change alpha
                }
            }
            else
            {
                P_interactiveImage.SetActive(false);
            }
        }
        else
        {
            P_interactiveImage.SetActive(false);
        }
    }
}