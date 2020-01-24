using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string _nameCurrentScene;
    private GameState _gameState = GameState.Menu;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
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
}

public enum GameState
{
    Running,
    Pause,
    Transition,
    Menu
}