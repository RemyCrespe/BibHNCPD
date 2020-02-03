/**************************************************************************
 ** Nathan Lefebvre
 **
 ** 22 - 01 - 2020
 **
 ** GameManager qui permet de changer de scene 
 *************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{
    public string[] arr_SceneName;
    private string _nameCurrentScene;
    private GameState _gameState = GameState.Menu;

    private uint p_nbScenes;
    public uint NbScenes { get { return p_nbScenes; } }    

    private List<string> p_list_Scenes = new List<string>();              

    public enum FIN_DE_PARTIE { GAGNE, PERDU };

    [SerializeField] private string _btUse;
    public int _GMnbRoom { get; set; }
    public bool _enterTrigger { get; set; }

    void Start()
    {
        _GMnbRoom = 0;
        _enterTrigger = false;
    }

    void Awake()
    {
        foreach (string _scene_name in arr_SceneName)
        {
            p_list_Scenes.Add(_scene_name);
            p_nbScenes++;
        }
    }

    public void ChangeGameState(GameState newGameState)
    {
        _gameState = newGameState;

        switch (_gameState)
        {
            case GameState.Running:
                break;
            case GameState.Pause:
                break;
            case GameState.Transition:
                break;
            case GameState.Menu:
                break;
            default:
                break;
        }
    }

    public void LoadScene(string __scene_name)
    {
        SceneManager.LoadScene(p_list_Scenes[p_list_Scenes.IndexOf(__scene_name)]);
    }

    // Si est à coté de la porte du DJ
    private void OnTriggerStay(Collider other)
    {
        _enterTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _enterTrigger = false;
        Debug.Log("exit");
    }

    public bool IsbtUse_down()
    {
        if (Input.GetKey(_btUse))
        {
            return true;
        }
        return false;
    }
}

    public enum GameState
{
    Running,
    Pause,
    Transition,
    Menu
}