using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private RectTransform rectTransform;

    [Header("Variables")]
    [SerializeField]
    private float offset;
    [SerializeField]
    private float speed;

    public static CharacterController Instance;

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = rectTransform.anchoredPosition;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<ObjectController>() != null)
        {
            GameController.Instance.FinishGame();
        }
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = startPosition;
    }

    public IEnumerator MoveControl()
    {
        while (GameController.Instance.IsGame)
        {
            if (InputController.Instance.IsNeedToMove)
            {
                InputController.Instance.IsNeedToMove = false;

                Invoke(nameof(Move), 0.15f);
            }

            yield return null;
        }
    }

    private void Move()
    {
        if (InputController.Instance.IsMoveToRight && rectTransform.anchoredPosition.x < startPosition.x + offset)
        {
            StartCoroutine(MoveRightAnimation(new Vector2(rectTransform.anchoredPosition.x + offset, rectTransform.anchoredPosition.y)));
        }
        else if (!InputController.Instance.IsMoveToRight && rectTransform.anchoredPosition.x > startPosition.x - offset)
        {
            StartCoroutine(MoveLeftAnimation(new Vector2(rectTransform.anchoredPosition.x - offset, rectTransform.anchoredPosition.y)));
        }
    }

    private IEnumerator MoveRightAnimation(Vector2 newPosition)
    {
        while (rectTransform.anchoredPosition.x < newPosition.x)
        {
            rectTransform.anchoredPosition += new Vector2(Time.deltaTime * speed, 0);

            yield return null;
        }
    }

    private IEnumerator MoveLeftAnimation(Vector2 newPosition)
    {
        while (rectTransform.anchoredPosition.x > newPosition.x)
        {
            rectTransform.anchoredPosition -= new Vector2(Time.deltaTime * speed, 0);

            yield return null;
        }
    }
}