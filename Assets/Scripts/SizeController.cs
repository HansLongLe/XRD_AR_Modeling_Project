using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SizeControlller : MonoBehaviour
{
    public GameObject targetObject;
    public Slider heightSlider;
    public Slider widthSlider;
    public Slider depthSlider;
    public TMP_InputField heightTMPInputField;
    public TMP_InputField widthTMPInputField;
    public TMP_InputField depthTMPInputField;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = targetObject.transform.localScale;

        // Initialize TMP InputField values with current slider values
        heightTMPInputField.text = initialScale.y.ToString();
        widthTMPInputField.text = initialScale.x.ToString();
        depthTMPInputField.text = initialScale.z.ToString();
    }

    private void Update()
    {
        // Parse and update object scale from TMP InputFields
        if (float.TryParse(heightTMPInputField.text, out float height))
        {
            targetObject.transform.localScale = new Vector3(targetObject.transform.localScale.x, height, targetObject.transform.localScale.z);
            heightSlider.value = height - initialScale.y;
        }

        if (float.TryParse(widthTMPInputField.text, out float width))
        {
            targetObject.transform.localScale = new Vector3(width, targetObject.transform.localScale.y, targetObject.transform.localScale.z);
            widthSlider.value = width - initialScale.x;
        }

        if (float.TryParse(depthTMPInputField.text, out float depth))
        {
            targetObject.transform.localScale = new Vector3(targetObject.transform.localScale.x, targetObject.transform.localScale.y, depth);
            depthSlider.value = depth - initialScale.z;
        }
    }
}
