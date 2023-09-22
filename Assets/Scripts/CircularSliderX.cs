using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CircularSliderX : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public Transform objectToRotate;
    public Image circularImage;
    public TextMeshProUGUI rotationText;
    public int rotationAxis = 0; // 0 for X, 1 for Y, 2 for Z

    private Vector2 center;
    private Vector2 startDragPosition;
    private Quaternion initialRotation;
    private bool isDragging = false;

    private void Start()
    {
        center = circularImage.rectTransform.position;
        initialRotation = objectToRotate.rotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        Vector2 touchPosition = eventData.position;
        Vector2 offset = touchPosition - center;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        // Reverse the angle to make it clockwise
        angle = -angle;

        Vector3 eulerRotation = initialRotation.eulerAngles;

        if (rotationAxis == 0) // X-axis rotation
        {
            eulerRotation.x += angle;
        }
        else if (rotationAxis == 1) // Y-axis rotation
        {
            eulerRotation.y += angle;
        }
        else if (rotationAxis == 2) // Z-axis rotation
        {
            eulerRotation.z += angle;
        }

        objectToRotate.rotation = Quaternion.Euler(eulerRotation);

        // Update the drag start position for the next frame
        startDragPosition = offset.normalized;

        float fillAmount = eulerRotation[rotationAxis] / 360f;
        circularImage.fillAmount = fillAmount;

        rotationText.text = Mathf.Round(eulerRotation[rotationAxis]) + "°";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        Vector2 touchPosition = eventData.position;
        Vector2 offset = touchPosition - center;
        startDragPosition = offset.normalized;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }
}
