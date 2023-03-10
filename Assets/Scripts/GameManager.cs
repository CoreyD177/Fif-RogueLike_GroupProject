using System.Collections;
using System.Collections.Generic;
using UnityEngine; //Connect to Unity Engine
using UnityEngine.SceneManagement;
using UnityEngine.UI; //Enable changing of canvas UI elements

public class GameManager : MonoBehaviour
{
    #region Variables
    //Public enum to set our available game states
    public enum GameState
    {
        Menu,
        InGame,
        Paused,
        PostGame
    }
    //A reference to the current state we are in so we can change game options accordingly
    public static GameState state;
    //[Header("References")]
    //References to the PauseMenu and EndMenu in the scene will actually grab the background elements so we can activate and hide them. Cannot grab inactive elements easily by code.
    public static GameObject pauseMenu;
    public static GameObject endMenu;
    #endregion
    #region Setup
    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        //If our references are empty fill them from the scene
        if ((pauseMenu == null || endMenu == null) && SceneManager.GetActiveScene().buildIndex < 2)
        {
            pauseMenu = GameObject.Find("PauseMenu").transform.GetChild(0).gameObject;
            endMenu = GameObject.Find("EndMenu").transform.GetChild(0).gameObject;
        }
        //Set the current state to in game and run the function to change the options
        state = GameState.InGame;
        if (SceneManager.GetActiveScene().buildIndex < 2) ChangeState();        
    }
    #endregion
    #region Functions
    public static void ChangeState()
    {
        //Apply our cursror and menu options based on the state we are in
        switch (state)
        {
            case GameState.Menu:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endMenu.SetActive(false);
                pauseMenu.SetActive(false);
                break; 
            case GameState.InGame:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                endMenu.SetActive(false);
                pauseMenu.SetActive(false);
                break;
            case GameState.Paused:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endMenu.SetActive(false);
                pauseMenu.SetActive(true);
                break;
            case GameState.PostGame:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endMenu.SetActive(true);
                pauseMenu.SetActive(false);
                break;
            default:
                break;
        }
    }
    //Functions to allow buttons to access functions from MenuHandler
    public void ChangeScene(int sceneID)
    {
        MenuHandler.ChangeScene(sceneID);
    }
    public void QuitGame()
    {
        MenuHandler.ExitGame();
    }
    public void UnPause()
    {
        //If we hit the resume button change state to ingame and run function to change options
        state = GameState.InGame;
        ChangeState();
    }
    #endregion

    public void Test()
    {
        CharacterStatistics player = FindObjectOfType<PlayerMovement>().GetComponent<CharacterStatistics>();

        player.maxHealth += Mathf.RoundToInt(Random.Range(1, 5));
        player.defense += Mathf.RoundToInt(Random.Range(1, 5));
        player.damage += Mathf.RoundToInt(Random.Range(1, 5));
        player.crit += Mathf.RoundToInt(Random.Range(1, 2));
        player.critChance += Mathf.RoundToInt(Random.Range(1, 5));
        player.immunity += Random.Range(0.1f, 0.5f);
        player.maxMoveSpeed += Mathf.RoundToInt(Random.Range(1, 5));
        player.rollSpeed += Mathf.RoundToInt(Random.Range(1, 5));
    }
}
