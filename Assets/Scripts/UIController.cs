using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField]
    private CanvasGroup startBlock;
    [SerializeField]
    private CanvasGroup gameBlock;
    [SerializeField]
    private CanvasGroup finishBlock;

    [Header("Texts")]
    [SerializeField]
    private TMP_Text score;

    public static UIController Instance;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        ShowStart();

        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ShowStart()
    {
        startBlock.alpha = 1;
        startBlock.blocksRaycasts = true;

        gameBlock.alpha = 0;
        gameBlock.blocksRaycasts = false;
        finishBlock.alpha = 0;
        finishBlock.blocksRaycasts = false;
    }

    public void ShowGame()
    {
        gameBlock.alpha = 1;
        gameBlock.blocksRaycasts = true;

        startBlock.alpha = 0;
        startBlock.blocksRaycasts = false;
        finishBlock.alpha = 0;
        finishBlock.blocksRaycasts = false;
    }

    public void ShowFinish()
    {
        startBlock.alpha = 0;
        startBlock.blocksRaycasts = false;

        gameBlock.alpha = 1;
        gameBlock.blocksRaycasts = true;
        finishBlock.alpha = 1;
        finishBlock.blocksRaycasts = true;
    }

    public void UpdateScore(int currentScore)
    {
        score.text = $"{currentScore}";
    }
}