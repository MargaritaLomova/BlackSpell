using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Objects From Scene")]
    [SerializeField]
    private RectTransform game;

    [Header("Prefabs")]
    [SerializeField]
    private ObjectController objectPrefab;

    [Header("Variables")]
    [SerializeField]
    private List<Sprite> objectsSprites = new List<Sprite>();

    [Header("Buttons")]
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button tryAgainButton;

    public bool IsGame { get; private set; }
    public static GameController Instance;

    private int currentScore;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        tryAgainButton.onClick.AddListener(StartGame);

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

    public void PlusScore()
    {
        currentScore++;
        UIController.Instance.UpdateScore(currentScore);
    }

    public void FinishGame()
    {
        IsGame = false;

        UIController.Instance.ShowFinish();
    }

    private void StartGame()
    {
        IsGame = true;

        currentScore = 0;
        UIController.Instance.UpdateScore(currentScore);

        CharacterController.Instance.ResetPosition();

        UIController.Instance.ShowGame();

        StartCoroutine(CharacterController.Instance.MoveControl());
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while(IsGame)
        {
            var newObject = Instantiate(objectPrefab, game);
            newObject.Set(objectsSprites[Random.Range(0, objectsSprites.Count)]);

            Vector2 worldPosition = new Vector2(Random.Range(newObject.transform.localScale.x, Screen.width - newObject.GetComponent<RectTransform>().sizeDelta.x), 0);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(game, worldPosition, Camera.main, out Vector2 localPosition);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(localPosition.x, 0);

            yield return new WaitForSeconds(1.5f);
        }
    }
}