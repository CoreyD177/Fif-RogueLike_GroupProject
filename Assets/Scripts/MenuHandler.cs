using UnityEngine; //Connect to unity engine
using UnityEngine.SceneManagement; //Allow changing of scenes

public class MenuHandler : MonoBehaviour
{
    #region Instance
    public static MenuHandler instance;

    public void Awake()
    {
        //On awake set this object to not destroy when new scene is loaded
        DontDestroyOnLoad(this);
        //If we already have an instance destroy this one, otherwise this is the instance
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion
    #region Functions
    public static void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    public static void ExitGame()
    {
        //If we are in Unity Editor quit play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        //Else quit the application
        Application.Quit();
    }
    #endregion
}
