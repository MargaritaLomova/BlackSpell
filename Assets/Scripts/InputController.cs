using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public bool IsMoveToRight { get; private set; }
    public bool IsNeedToMove { get; set; }
    public static InputController Instance;

    private Vector2 startPosition;

    private void Start()
    {
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsNeedToMove = true;

#if UNITY_EDITOR

        startPosition = Input.mousePosition;

#elif UNITY_ANDROID || UNITY_IOS

        startPosition = Input.GetTouch(0).position;

#endif

        StartCoroutine(CheckSideToMove());
    }

    public void OnDrag(PointerEventData eventData) { }

    private IEnumerator CheckSideToMove()
    {
        yield return new WaitForSeconds(0.1f);

#if UNITY_EDITOR

        if (startPosition.x - Input.mousePosition.x > 0)
        {
            IsMoveToRight = false;
        }
        else if (startPosition.x - Input.mousePosition.x < 0)
        {
            IsMoveToRight = true;
        }

#elif UNITY_ANDROID || UNITY_IOS

        if (startPosition.x - Input.GetTouch(0).position.x > 0)
        {
            IsMoveToRight = false;
        }
        else if (startPosition.x - Input.GetTouch(0).position.x < 0)
        {
            IsMoveToRight = true;
        }

#endif
    }
}