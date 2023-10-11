using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int pages;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform levelPagesRect;
    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;
    private int currentPage;
    private Vector3 targetPos;
    private float dragThreshold;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }

    public void ObjectMenuPage()
    {
        if (currentPage == 2)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
        if (currentPage == 3)
        {
            currentPage = 1;
            targetPos -= (2 * pageStep);
            MovePage();
        }
    }

    public void SizePage()
    {
        if (currentPage == 3)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
        if (currentPage == 1)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void RotationPage()
    {
        if (currentPage == 2)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
        if (currentPage == 1)
        {
            currentPage = 3;
            targetPos += (2*pageStep);
            MovePage();
        }
    }

    public void Next()
    {
        if (currentPage < pages)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    public void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        }
        else
        {
            MovePage();
        }
    }
}
