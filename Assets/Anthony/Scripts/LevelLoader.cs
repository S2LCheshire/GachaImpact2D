using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 0f;
    [SerializeField] float startMenuDelayInSeconds = 4f;
    Scene currentScene;

    public static LevelLoader current;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {

        currentScene = SceneManager.GetActiveScene();
        /*
        if  (currentScene.buildIndex == 0)
        {
            StartCoroutine(StartMenu(startMenuDelayInSeconds));
        }
        */
    }
    public void LoadStartMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Level Selector");
        //FindObjectOfType<GameSession>().ResetScore();
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(level + 1);
        //FindObjectOfType<GameSession>().ResetScore();
    }
    public void LoadNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public IEnumerator LoadHomeWithDelay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GachaGame");
        yield return new WaitForSeconds(delayInSeconds);
    }
    public IEnumerator LoadTypingGameWithDelay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("typingGame");
        yield return new WaitForSeconds(delayInSeconds);
    }

    public void LoadTypingGame()
    {
        Time.timeScale = 1;
        InventoryManager.current.SaveInventory();
        SceneManager.LoadScene("typingGame");
    }
    public void LoadHome()
    {
        Time.timeScale = 1;
        InventoryManager.current.SaveInventory();
        SceneManager.LoadScene("GachaGame");
        
    }

    IEnumerator StartMenu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoadStartMenu();
    }
    IEnumerator GameOver(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Game Over");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
