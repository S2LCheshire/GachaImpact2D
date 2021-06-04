using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class InputController : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI gameOverExplanationText;
    [SerializeField] TextMeshProUGUI finalCoinText;
    [SerializeField] TextMeshProUGUI coinCurrentText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI correctAnswerText;
    [SerializeField] DisplayMoveController display;
    [SerializeField] Sprite coinSprite;
    TMP_InputField inputText;
    TextMeshProUGUI displayText;
    AudioManager audioManager;
    bool isSelected = false;
    bool isCaseSensitive = false;
    bool inGame = false;
    bool inPaused= false;
    float maxTime = 10f;
    public float currentTimer;
    // Time Function: 1/(0.01*(x-(-25))) + 1
    float timeA = 0.01f;
    float timeB = -25f;
    float timeC = 1f;
    float totalCoinGained = 0f;
    int wordCountDisplay;
    int correctAnswer = 0;
    float coinModifier = 1.2f;
    GameController gameController;

    public delegate void CustomEvent();
    public static event CustomEvent WrongAnswer;
    public static event CustomEvent GameStart;

    public static InputController current;

    private void Awake()
    {
        current = this;
        gameController = FindObjectOfType<GameController>();
        audioManager = FindObjectOfType<AudioManager>();

        inputText = GetComponent<TMP_InputField>();
        currentTimer = GetMaxTime(0);
        timerText.text = "Time: " + currentTimer.ToString("0.00");
        correctAnswerText.text = "Correct Answer: 0";
        coinCurrentText.text = "Coin: 0";
    }

    private void Start()
    {
        GetNextRandomWord();

    }

    readonly System.Random rnd = new System.Random();

    private void Update()
    {
        if (inGame)
        {
            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;

                if (isSelected & (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && inputText.text.Trim().Length != 0)
                {
                    currentTimer = GetMaxTime(correctAnswer);
                    CheckAnswer();
                }
            }
            else
            {
                currentTimer = 0;
                WrongAnswer?.Invoke();
                PopUp.Create(display.transform.position, "TIME'S UP!!!!!!!", new Color(1, 0, 0, 1));
                ////Debug.Log("Time's Up");
                currentTimer = GetMaxTime(correctAnswer);
                GetNextRandomWord();
                NavigateToInputField();
                audioManager.Play("OpenGachaCommon");

            }

            if (inPaused == false && Input.GetKeyDown(KeyCode.Escape))
            {
                gameController.PauseGame("pause");
                inPaused = true;
            }
            else if (inPaused == true && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)))
            {
                gameController.PauseGame("continue");
                inPaused = false;
                NavigateToInputField();
            }


            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Backspace))
                {
                    inputText.text = "";
                }

            InGameCalculation();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Return)) 
        {
            StartGame();
            gameController.InGame();
        }
    }

    public void OnSelectInputField()
    {
        isSelected = true;
    }
    public void OnDeselectInputField()
    {
        isSelected = false;
    }

    public void StartGame()
    {
        inGame = true;
        inputText.text = "";
        NavigateToInputField();
        OnSelectInputField();
        GameStart?.Invoke();
    }

    public void StopGame()
    {
        inGame = false;
        EndGameCoinCalculation("");
        OnDeselectInputField();
        audioManager.Play("GameOver");
    }
    public void StopGame(string explain)
    {
        inGame = false;
        EndGameCoinCalculation(explain);
        OnDeselectInputField();
    }

    private void CheckAnswer()
    {
        if (inputText.text.Trim() == display.GetText())
        {
            ////Debug.Log("Correct");
            correctAnswer += 1;
            CoinCalculation();
            PopUp.Create(coinCurrentText.transform.position, "+"+CoinGain().ToString("0"), new Color(1, 0.92f, 0.016f, 1));
            audioManager.Play("CoinGain");

        }
        else if (inputText.text.Trim().ToLower() == display.GetText().ToLower() && isCaseSensitive == false)
        {
            ////Debug.Log("Correct");
            correctAnswer += 1;
            CoinCalculation();
            PopUp.Create(coinCurrentText.transform.position,"+" + CoinGain().ToString("0"), new Color(1, 0.92f, 0.016f, 1));
            audioManager.Play("CoinGain");
        }
        else
        {
            ////Debug.Log("Stoopid || (" + display.GetText() + ") ----- (" + inputText.text + ")");
            WrongAnswer?.Invoke();

            PopUp.Create(display.transform.position, "WRONG!!!!!!!", new Color(1, 0, 0, 1));
            audioManager.Play("OpenGachaCommon");

        }

        GetNextRandomWord();
        NavigateToInputField();
    }

    private void EndGameCoinCalculation(string gameOverText)
    {
        float total;
        total = InventoryManager.current.GetGachaCoin() + totalCoinGained + ExtraCoinFromEquipment();
        gameOverExplanationText.text = gameOverText;
        string space = "".PadRight(10);
        finalCoinText.text = InventoryManager.current.GetGachaCoin() + "\n" +  totalCoinGained.ToString("0") + "\n" + ExtraCoinFromEquipment() + "\n------------\n" + total ;
        InventoryManager.current.AddGachaCoin((int)totalCoinGained);
        InventoryManager.current.SaveInventory();
    }

    private int ExtraCoinFromEquipment()
    {
        int extraCoin = 0;
        if (InventoryManager.current.GetEquippedPet() != null)
        {
            extraCoin += (int)(InventoryManager.current.GetEquippedPet().GetCoinMultiplier() * totalCoinGained);
            extraCoin += InventoryManager.current.GetEquippedPet().GetExtraCoin();
        }

        return extraCoin;
    }

    private void GetNextRandomWord()
    {
        int wIndex = rnd.Next(Words.WordCollection.Length);
        display.SetText(Words.WordCollection[wIndex], currentTimer, correctAnswer);
        inputText.text = "";
    }

    private float GetMaxTime(float x)
    {
        maxTime = 1 / (timeA * (x - timeB)) + timeC;
        return maxTime;

    }

    private void CoinCalculation()
    {
        totalCoinGained += CoinGain();
    }

    private int CoinGain()
    {
        int gain = (int)Math.Round(10 * display.GetText().Length / GetMaxTime(correctAnswer));
        ////Debug.Log("Gain: " + gain);

        float modifierCount = display.CountModifier();
        gain = modifierCount == 0 ? gain : (int)(modifierCount * coinModifier * gain);
        ////Debug.Log("ModGain: " + gain);

        return gain;
    }

    private void NavigateToInputField()
    {
        inputText.Select();
        inputText.ActivateInputField();
    }

    private void InGameCalculation()
    {
        timerText.text = "Time: " + currentTimer.ToString("0.00");
        correctAnswerText.text = "Correct Answer: " + correctAnswer;
        coinCurrentText.text = "Coin: " + totalCoinGained.ToString("0");
    }

}
