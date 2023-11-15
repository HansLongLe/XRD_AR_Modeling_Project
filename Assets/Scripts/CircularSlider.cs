using System;
using System.Globalization;
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

    [SerializeField] private TMP_InputField valueX;
    [SerializeField] private TMP_InputField valueY;
    [SerializeField] private TMP_InputField valueZ;
    private float floatValue;

    private static GameObject targetObject; // Reference to the object you want to rotate
    private static bool modelChanged = false;

    private Vector3 mousePos;

    private void Start()
    {
        TouchSelection.OnSelectionModel += SetCurrentSelectedModel;
    }

    private static void SetCurrentSelectedModel(GameObject model)
    {
        targetObject = model;
        modelChanged = true;
        
    }

    private void Update()
    {
        if (!modelChanged || !targetObject) return;
        var modelRotation = targetObject.transform.transform.eulerAngles;
        
        var checkedValueX = Mathf.Round(360 - modelRotation.x);
        checkedValueX = Math.Abs(Mathf.Round(360 - modelRotation.x) - 360f) < 0.001f ? 0f : checkedValueX;
        
        var checkedValueY = Mathf.Round(360 - modelRotation.y);
        checkedValueY = Math.Abs(Mathf.Round(360 - modelRotation.y) - 360f) < 0.001f ? 0f : checkedValueY;
        
        var checkedValueZ = Mathf.Round(360 - modelRotation.z);
        checkedValueZ = Math.Abs(Mathf.Round(360 - modelRotation.z) - 360f) < 0.001f ? 0f : checkedValueZ;
        
        valueX.text = checkedValueX.ToString(CultureInfo.CurrentCulture);
        valueY.text = checkedValueY.ToString(CultureInfo.CurrentCulture);
        valueZ.text = checkedValueZ.ToString(CultureInfo.CurrentCulture);
        fillX.fillAmount = Math.Abs(1f - (modelRotation.x / 360f) - 1) < 0.001f ? 0 : 1f - (modelRotation.x / 360f);
        fillY.fillAmount = Math.Abs(1f - (modelRotation.y / 360f) - 1) < 0.001f ? 0 : 1f - (modelRotation.y / 360f);
        fillZ.fillAmount = Math.Abs(1f - (modelRotation.z / 360f) - 1) < 0.001f ? 0 : 1f - (modelRotation.z / 360f);
        modelChanged = false;
    }

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

    private void UpdateRotation(Transform handle, Image fill, TMP_InputField value, Vector3 axis)
    {
        mousePos = Input.mousePosition;
        Vector2 direction = mousePos - handle.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;

        if (targetObject != null)
        {
            var rotation = Quaternion.AngleAxis(angle, axis);
            targetObject.transform.rotation = rotation;
        }

        angle = ((angle >= 360) ? (angle - 360) : angle);
        fill.fillAmount = 1f - (angle / 360f);
        value.text = Mathf.Round(fill.fillAmount * 360).ToString(CultureInfo.CurrentCulture);
    }

    public void UpdateThroughInputX()
    {
        UpdateThroughInput(fillX,valueX, "x");
    }
    
    public void UpdateThroughInputY()
    {
        UpdateThroughInput(fillY,valueY, "y");
    }
    public void UpdateThroughInputZ()
    {
        UpdateThroughInput(fillZ,valueZ, "z");
    }
    private void UpdateThroughInput(Image fill, TMP_InputField value, string axis)
    {
        if (float.TryParse(value.text, out floatValue))
        {
            var remainder = floatValue % 360;
            var normalizedValue = remainder / 360f;

            fill.fillAmount = Mathf.Clamp(normalizedValue, 0f, 1f);
            value.text = Mathf.Round(fill.fillAmount * 360).ToString(CultureInfo.CurrentCulture);

            var rotationAmount = Mathf.Lerp(0f, 360f, normalizedValue);
            var objectEuler = targetObject.transform.eulerAngles;
            switch (axis)
            {
                case "x":
                    targetObject.transform.rotation = Quaternion.Euler(rotationAmount, objectEuler.y, objectEuler.z);
                    break;
                case "y":
                    targetObject.transform.rotation = Quaternion.Euler(objectEuler.x, rotationAmount, objectEuler.z);
                    break;
                case  "z":
                    targetObject.transform.rotation = Quaternion.Euler(objectEuler.x, objectEuler.y, rotationAmount);
                    break;
            }


        }
        else
        {
            Debug.LogError("Invalid input: " + valueX.text);
        }
    }


    private void OnDestroy()
    {
        TouchSelection.OnSelectionModel -= SetCurrentSelectedModel;
    }
}