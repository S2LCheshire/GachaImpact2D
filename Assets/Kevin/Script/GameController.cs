using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas StartGameCanvas;
    [SerializeField] Canvas PauseGameCanvas;
    int maxLife;
    int additionalLife;

    //ilanging serializefield
    [SerializeField] int life;

    public delegate void GameEvents(int i);
    public GameEvents lifeUpdate;


    private void Awake()
    {
        gameOverCanvas.gameObject.SetActive(false);
        StartGameCanvas.gameObject.SetActive(true);
        PauseGameCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    private void Start()
    {
        InputController.GameStart += InitializeMaxLife;
        InitializeMaxLife();
        InputController.WrongAnswer += DecreaseLife;

    }

    private void OnDestroy()
    {
        InputController.WrongAnswer -= DecreaseLife;
        InputController.GameStart -= InitializeMaxLife;
    }


    private void Update()
    {
        if (gameOverCanvas.gameObject.activeSelf && (Input.GetKeyDown(KeyCode.Tab)))
        {
            LevelLoader.current.RestartScene();
        }
    }

    public void EndGame()
    {
        gameOverCanvas.gameObject.SetActive(true);
        InputController.current.StopGame();

        // Time stop
        Time.timeScale = 0;
    }

    public void InGame()
    {
        StartGameCanvas.gameObject.SetActive(false);
    }

    public void PauseGame(string action)
    {
        if (action == "continue")
        {
            Time.timeScale = 1;
            PauseGameCanvas.gameObject.SetActive(false);
        }
        else if (action == "pause")
        {
            Time.timeScale = 0;
            PauseGameCanvas.gameObject.SetActive(true);
        }
    }

    public int GetMaxLife()
    {
        return maxLife;
    }

    public void LifeAdd(int additionalLife)
    {
        if(life+additionalLife <= maxLife)
        {
            life += additionalLife;
        }
        else
        {
            life = maxLife;
        }

        lifeUpdate?.Invoke(life);
        ////Debug.Log("life: " + life);

        if (life <= 0)
        {
            EndGame();
        }
    }

    private void DecreaseLife()
    {
        LifeAdd(-1);
    }

    private void InitializeMaxLife()
    {
        additionalLife = InventoryManager.current.GetEquippedHero() == null ? 0 : InventoryManager.current.GetEquippedHero().GetExtraLife();
        maxLife = 1 + additionalLife;
        life = maxLife;
    }
}
