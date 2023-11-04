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

    [SerializeField] private TextMeshProUGUI valueX;
    [SerializeField] private TextMeshProUGUI valueY;
    [SerializeField] private TextMeshProUGUI valueZ;
    
    private static GameObject targetObject; // Reference to the object you want to rotate
    private static bool modelChanged = false;

    private Vector3 mousePos;

    private void Start()
    {
        ARChangeModelOnSelection.OnSendSelectedModel += SetCurrentSelectedModel;
    }
    
    private static void SetCurrentSelectedModel(GameObject selectedModel)
    {
        targetObject = selectedModel;
        if (selectedModel == null) return;
        modelChanged = true;

    }

    private void Update()
    {
        if(!modelChanged || !targetObject) return;
        var modelRotation = targetObject.transform.transform.eulerAngles;
        valueX.text = Mathf.Round(360 - modelRotation.x).ToString(CultureInfo.CurrentCulture);
        valueY.text = Mathf.Round(360 - modelRotation.y).ToString(CultureInfo.CurrentCulture);
        valueZ.text = Mathf.Round(360 - modelRotation.z).ToString(CultureInfo.CurrentCulture);
        fillX.fillAmount = 1f - (modelRotation.x / 360f);
        fillY.fillAmount = 1f - (modelRotation.y / 360f);
        fillZ.fillAmount = 1f - (modelRotation.z / 360f);
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

    private void UpdateRotation(Transform handle, Image fill, TMP_Text value, Vector3 axis)
    {
        mousePos = Input.mousePosition;
        Vector2 direction = mousePos - handle.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;
        
        // Adjust the rotation of the targetObject around the specified axis
        if(targetObject != null)
        {
            var rotation = Quaternion.AngleAxis(angle , axis);
            targetObject.transform.rotation = rotation;
        }

        angle = ((angle >= 360) ? (angle - 360) : angle);
        fill.fillAmount = 1f - (angle / 360f);
        value.text = Mathf.Round(fill.fillAmount * 360).ToString(CultureInfo.CurrentCulture) + "°";
    }

    private void OnDestroy()
    {
        ARChangeModelOnSelection.OnSendSelectedModel -= SetCurrentSelectedModel;
    }
}