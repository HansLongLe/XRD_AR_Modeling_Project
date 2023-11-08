using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int pages;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform levelPagesRect;
    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;
    [SerializeField] private int currentPage;
    private Vector3 targetPos;
    private float dragThreshold;
    [SerializeField] private bool isWindowUp = false;


    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }

    public void ChangePage(int targetPage)
    {
        if (!isWindowUp)
        {
            Up();
        }
        else if (targetPage == currentPage && isWindowUp)
        {
            Down();
        }

    
        while (targetPage < currentPage)
        {
            Previous();
        }
        while (targetPage > currentPage)
        {
            Next();
        }
        MovePage();
    }
    
 
    public void Next()
    {
        currentPage++;
        targetPos += new Vector3(pageStep.x, 0, 0);
    }

    public void Previous()
    {
        currentPage--;
        targetPos -= new Vector3(pageStep.x, 0, 0);
    }

    public void Up()
    {
        if (!isWindowUp)
        {
            isWindowUp = true;
            targetPos -= new Vector3(0, pageStep.y, 0);
        }
    }
    
    public void Down()
    {
        if (isWindowUp)
        {
            isWindowUp = false;
            targetPos += new Vector3(0, pageStep.y, 0);
        }
    }

    private void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float deltaX = eventData.position.x - eventData.pressPosition.x;
        float deltaY = eventData.position.y - eventData.pressPosition.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > dragThreshold) Previous();
            else if (deltaX < -dragThreshold) Next();
        }
        else
        {
            if (deltaY > dragThreshold) Up();
            else if (deltaY < -dragThreshold) Down();
        }
        MovePage();
    }
}
