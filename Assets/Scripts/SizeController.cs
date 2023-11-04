using System;
using System.Globalization;
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
        ARChangeModelOnSelection.OnSendSelectedModel += SetCurrentSelectedModel;
        
    }
    
    private static void SetCurrentSelectedModel(GameObject selectedModel)
    {
        targetObject = selectedModel;
        if (!targetObject) return;
        initialScale = selectedModel.gameObject.transform.localScale;
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
        if (!(float.TryParse(heightString, out var height) && targetObject)) return;
        var localScale = targetObject.transform.localScale;
        localScale = new Vector3(localScale.x, height, localScale.z);
        targetObject.transform.localScale = localScale;
        heightSlider.value = height;

    }
    private void OnWidthInputChange(string widthString)
    {
        if (!(float.TryParse(widthString, out var width) && targetObject)) return;
        var localScale = targetObject.transform.localScale;
        localScale = new Vector3(width, localScale.y, localScale.z);
        targetObject.transform.localScale = localScale;
        widthSlider.value = width;
    }
    private void OnDepthInputChange(string depthString)
    {
        if (!(float.TryParse(depthString, out var depth) && targetObject)) return;
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
        ARChangeModelOnSelection.OnSendSelectedModel -= SetCurrentSelectedModel;
        heightTMPInputField.onValueChanged.RemoveListener(OnHeightInputChange);
        widthTMPInputField.onValueChanged.RemoveListener(OnWidthInputChange);
        depthTMPInputField.onValueChanged.RemoveListener(OnDepthInputChange);
        heightSlider.onValueChanged.RemoveListener(OnHeightSliderUpdate);
        widthSlider.onValueChanged.RemoveListener(OnWidthSliderUpdate);
        depthSlider.onValueChanged.RemoveListener(OnDepthSliderUpdate);
    }
}
