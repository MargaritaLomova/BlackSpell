using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Image image;

    private void Start()
    {
        StartCoroutine(IsNeedToDestroy());
    }

    public void Set(Sprite newSprite)
    {
        image.sprite = newSprite;
    }

    private bool IsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    private IEnumerator IsNeedToDestroy()
    {
        yield return new WaitUntil(() => IsVisible());

        while(GameController.Instance.IsGame && IsVisible())
        {
            yield return new WaitForSeconds(0.5f);
        }
        GameController.Instance.PlusScore();
        Destroy(gameObject);
    }
}