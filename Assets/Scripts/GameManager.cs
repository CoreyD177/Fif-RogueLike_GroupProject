using UnityEngine; //Connect to Unity Engine
using UnityEngine.EventSystems; //Allow us to manually deselect a button so theres no bugs on reopening pause menu
using UnityEngine.SceneManagement; //Allow for scene management

public class GameManager : MonoBehaviour
{
    #region Variables
    //Public enum to set our available game states
    public enum GameState
    {
        Menu,
        InGame,
        Safe,
        Paused,
        LevelEnd,
        PostGame
    }
    //A reference to the current state we are in so we can change game options accordingly
    public static GameState state;
    //A reference to the state the player was in before pausing
    public static GameState currentState;
    //[Header("References")]
    //References to the menus in the scene will actually grab the background elements so we can activate and hide them. Cannot grab inactive elements easily by code.
    public static GameObject pauseMenu;
    public static GameObject endMenu;
    public static GameObject classMenu;
    public static GameObject headsUpDisplay;
    public static GameObject levelEndMenu;
    //Int to store maximum allowable room spawns per level
    public int roomLimit = 10;
    #endregion
    #region Setup
    private void Start()
    {
        //If our references are empty fill them from the scene
        if (pauseMenu == null || endMenu == null || classMenu == null || headsUpDisplay == null || levelEndMenu == null)
        {
            pauseMenu = GameObject.Find("PauseMenu").transform.GetChild(0).gameObject;
            endMenu = GameObject.Find("EndMenu").transform.GetChild(0).gameObject;
            classMenu = GameObject.Find("ClassMenu").transform.GetChild(0).gameObject;
            headsUpDisplay = GameObject.Find("HUD").transform.GetChild(0).gameObject;
            levelEndMenu = GameObject.Find("LevelEndMenu").transform.GetChild(0).gameObject;
        }
        //Set the current state to in game and run the function to change the options
        state = GameState.Menu;
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
                headsUpDisplay.SetActive(false);
                classMenu.SetActive(true);
                levelEndMenu.SetActive(false);
                break; 
            case GameState.InGame:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                endMenu.SetActive(false);
                pauseMenu.SetActive(false);
                headsUpDisplay.SetActive(true);
                classMenu.SetActive(false);
                levelEndMenu.SetActive(false);
                break;
            case GameState.Safe:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                endMenu.SetActive(false);
                pauseMenu.SetActive(false);
                headsUpDisplay.SetActive(true);
                classMenu.SetActive(false);
                levelEndMenu.SetActive(false);
                break;
            case GameState.Paused:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endMenu.SetActive(false);
                pauseMenu.SetActive(true);
                headsUpDisplay.SetActive(false);
                classMenu.SetActive(false);
                levelEndMenu.SetActive(false);
                break;
            case GameState.LevelEnd:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endMenu.SetActive(false);
                pauseMenu.SetActive(false);
                headsUpDisplay.SetActive(false);
                classMenu.SetActive(false);
                levelEndMenu.SetActive(true);
                break;
            case GameState.PostGame:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endMenu.SetActive(true);
                pauseMenu.SetActive(false);
                headsUpDisplay.SetActive(false);
                classMenu.SetActive(false);
                levelEndMenu.SetActive(false);
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
        state = currentState;
        ChangeState();
    }
    public void BeginGame()
    {
        //If we hit the start button from the class selection menu change state to safe as we start in a corrider where enemies can't get us
        state = GameState.Safe;
        ChangeState();
    }
    public void DeselectButton(EventSystem events)
    {
        events.SetSelectedGameObject(null);
    }
    #endregion
}
