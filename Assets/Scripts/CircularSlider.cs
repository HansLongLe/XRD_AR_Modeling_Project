using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // Import the TextMesh Pro namespace

public class CircularSlider : MonoBehaviour, IDragHandler
{
    public Transform objectToRotate;
    public Image[] circularImages; // Array of circular sliders
    public TextMeshProUGUI[] rotationTexts; // Array of TextMesh Pro Text components
    private Vector2[] centers; // Array to store the centers of circular sliders
    private Vector2[] startDragPositions; // Array to store start drag positions
    private Quaternion initialRotation;

    private void Start()
    {
        int numSliders = circularImages.Length;
        centers = new Vector2[numSliders];
        startDragPositions = new Vector2[numSliders];

        for (int i = 0; i < numSliders; i++)
        {
            centers[i] = circularImages[i].rectTransform.position;
            startDragPositions[i] = Vector2.zero;
        }

        initialRotation = objectToRotate.rotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        int numSliders = circularImages.Length;

        for (int i = 0; i < numSliders; i++)
        {
            Vector2 touchPosition = eventData.position;
            Vector2 offset = touchPosition - centers[i];

            // Calculate the angle between the drag start position and the current touch position
            float angle = Vector2.SignedAngle(startDragPositions[i], offset);

            // Reverse the angle to make it clockwise
            angle = -angle;

            // Update the object's rotation based on the calculated angle
            Vector3 eulerRotation = objectToRotate.rotation.eulerAngles;

            if (i == 0) // X-axis rotation
            {
                eulerRotation.x += angle;
            }
            else if (i == 1) // Y-axis rotation
            {
                eulerRotation.y += angle;
            }
            else if (i == 2) // Z-axis rotation
            {
                eulerRotation.z += angle;
            }

            objectToRotate.rotation = Quaternion.Euler(eulerRotation);

            // Update the drag start position for the next frame
            startDragPositions[i] = offset.normalized;

            // Update the fillAmount of the circularImage to show progress
            float fillAmount = eulerRotation[i] / 360f;
            circularImages[i].fillAmount = fillAmount;

            // Update the TextMesh Pro Text component with the rotation value
            rotationTexts[i].text = Mathf.Round(eulerRotation[i]) + "°";
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        int numSliders = circularImages.Length;

        for (int i = 0; i < numSliders; i++)
        {
            Vector2 touchPosition = eventData.position;
            Vector2 offset = touchPosition - centers[i];

            // Calculate the angle between the initial touch position and the center of the circle
            startDragPositions[i] = offset.normalized;
        }
    }
}
