using UnityEngine;

public class CanvasGroupController : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnEnter()
    {
        canvasGroup.alpha = 0.2f;
    }

    public void OnExit()
    {
        canvasGroup.alpha = 1f;
    }
}
