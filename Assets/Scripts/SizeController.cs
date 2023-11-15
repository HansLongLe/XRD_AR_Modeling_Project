using System;
using System.Globalization;
using Lean.Common;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SizeControlller : MonoBehaviour
{
    private static GameObject targetObject;
    public Slider heightSlider;
    public Slider widthSlider;
    public Slider depthSlider;
    public TMP_InputField heightTMPInputField;
    public TMP_InputField widthTMPInputField;
    public TMP_InputField depthTMPInputField;

    private static Vector3 initialScale;
    private static bool modelChanged = false;

    private void Start()
    {
        TouchSelection.OnSelectionModel += SetCurrentSelectedModel;
        heightTMPInputField.onValueChanged.AddListener(OnHeightInputChange);
        widthTMPInputField.onValueChanged.AddListener(OnWidthInputChange);
        depthTMPInputField.onValueChanged.AddListener(OnDepthInputChange);
        heightSlider.onValueChanged.AddListener(OnHeightSliderUpdate);
        widthSlider.onValueChanged.AddListener(OnWidthSliderUpdate);
        depthSlider.onValueChanged.AddListener(OnDepthSliderUpdate);
    }
    
    private static void SetCurrentSelectedModel(GameObject model)
    {
        targetObject = model;
        if (targetObject)
        {
            initialScale = targetObject.transform.localScale;
        }
        else
        {
            initialScale = new Vector3(0, 0, 0);
        }
        
        modelChanged = true;
        
    }

    private void Update()
    {
        if (!modelChanged) return;
        heightTMPInputField.text = initialScale.y.ToString(CultureInfo.InvariantCulture);
        widthTMPInputField.text = initialScale.x.ToString(CultureInfo.InvariantCulture);
        depthTMPInputField.text = initialScale.z.ToString(CultureInfo.InvariantCulture);
        modelChanged = false;
    }

    private void OnHeightInputChange(string heightString)
    {
        if (!(float.TryParse(heightString, out var height) && targetObject != null)) return;
        var localScale = targetObject.transform.localScale;
        localScale = new Vector3(localScale.x, height, localScale.z);
        targetObject.transform.localScale = localScale;
        heightSlider.value = height;

    }
    private void OnWidthInputChange(string widthString)
    {
        if (!(float.TryParse(widthString, out var width) && targetObject != null)) return;
        var localScale = targetObject.transform.localScale;
        localScale = new Vector3(width, localScale.y, localScale.z);
        targetObject.transform.localScale = localScale;
        widthSlider.value = width;
    }
    private void OnDepthInputChange(string depthString)
    {
        if (!(float.TryParse(depthString, out var depth) && targetObject != null)) return;
        var localScale = targetObject.transform.localScale;
            localScale = new Vector3(localScale.x, localScale.y, depth);
            targetObject.transform.localScale = localScale;
            depthSlider.value = depth;
    }

    private void OnHeightSliderUpdate(float height)
    {
        heightTMPInputField.text = height.ToString(CultureInfo.InvariantCulture);
    }
    private void OnWidthSliderUpdate(float width)
    {
        widthTMPInputField.text = width.ToString(CultureInfo.InvariantCulture);
    }
    private void OnDepthSliderUpdate(float depth)
    {
        depthTMPInputField.text = depth.ToString(CultureInfo.InvariantCulture);
    }

    private void OnDestroy()
    {
        TouchSelection.OnSelectionModel -= SetCurrentSelectedModel;
        heightTMPInputField.onValueChanged.RemoveListener(OnHeightInputChange);
        widthTMPInputField.onValueChanged.RemoveListener(OnWidthInputChange);
        depthTMPInputField.onValueChanged.RemoveListener(OnDepthInputChange);
        heightSlider.onValueChanged.RemoveListener(OnHeightSliderUpdate);
        widthSlider.onValueChanged.RemoveListener(OnWidthSliderUpdate);
        depthSlider.onValueChanged.RemoveListener(OnDepthSliderUpdate);
    }
}
