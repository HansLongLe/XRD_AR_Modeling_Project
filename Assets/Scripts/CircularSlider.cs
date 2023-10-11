using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CircularSlider : MonoBehaviour
{
    [SerializeField] private Transform handleX;
    [SerializeField] private Transform handleY;
    [SerializeField] private Transform handleZ;

    [SerializeField] private Image fillX;
    [SerializeField] private Image fillY;
    [SerializeField] private Image fillZ;

    [SerializeField] private TextMeshProUGUI valueX;
    [SerializeField] private TextMeshProUGUI valueY;
    [SerializeField] private TextMeshProUGUI valueZ;

    [SerializeField] private Transform targetObject; // Reference to the object you want to rotate

    private Vector3 mousePos;

    public void OnHandleXDrag()
    {
        UpdateRotation(handleX, fillX, valueX, Vector3.right);
    }

    public void OnHandleYDrag()
    {
        UpdateRotation(handleY, fillY, valueY, Vector3.up);
    }

    public void OnHandleZDrag()
    {
        UpdateRotation(handleZ, fillZ, valueZ, Vector3.forward);
    }

    private void UpdateRotation(Transform handle, Image fill, TextMeshProUGUI value, Vector3 axis)
    {
        mousePos = Input.mousePosition;
        Vector2 direction = mousePos - handle.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;

        // Adjust the rotation of the targetObject around the specified axis
        Quaternion rotation = Quaternion.AngleAxis(angle + 135f, axis);
        targetObject.rotation = rotation;

        angle = ((angle >= 360) ? (angle - 360) : angle);
        fill.fillAmount = 1f - (angle / 360f);
        value.text = Mathf.Round(fill.fillAmount * 360).ToString() + "°";
    }
}