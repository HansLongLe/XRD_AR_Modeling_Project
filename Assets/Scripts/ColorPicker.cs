using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPicker : MonoBehaviour
{
    public float currentHue, currentSat, currentVal;

    [SerializeField] private RawImage hueImage, satValImage, outputImage;

    [SerializeField] private Slider hueSlider;

    [SerializeField] private TMP_InputField hexInputField,  redInputField, greenInputField, blueInputField;

    private Texture2D hueTexture, svTexture, outputTexture;

    [SerializeField] private MeshRenderer changeThisColor;

    private void Start()
    {
        CreateHueImage();
        CreateSVImage();
        CreateOutputImage();
        UpdateOutputImage();
    }

    private void CreateHueImage()
    {
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++)
        {
            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 1f));
        }
        
        hueTexture.Apply();
        currentHue = 0;
        hueImage.texture = hueTexture;
    }

    private void CreateSVImage()
    {
        svTexture = new Texture2D(16, 16);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SatValTexture";

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x/ svTexture.width, (float)y / svTexture.height));
            }
        }
        
        svTexture.Apply();
        currentSat = 0;
        currentVal = 0;
        satValImage.texture = svTexture;
    }

    private void CreateOutputImage()
    {
        outputTexture = new Texture2D(1, 16);
        outputTexture.wrapMode = TextureWrapMode.Clamp;
        outputTexture.name = "OutputTexture";

        Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColor);
        }
        
        outputTexture.Apply();
        outputImage.texture = outputTexture;
    }

    private void UpdateOutputImage()
    {
        Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0,i, currentColor);
        }
        
        outputTexture.Apply();

        hexInputField.text = ColorUtility.ToHtmlStringRGB(currentColor);
        redInputField.text = Mathf.FloorToInt(currentColor.r * 255).ToString();
        greenInputField.text = Mathf.FloorToInt(currentColor.g * 255).ToString();
        blueInputField.text = Mathf.FloorToInt(currentColor.b * 255).ToString();
      
        changeThisColor.GetComponent<MeshRenderer>().material.color = currentColor;
    }

    public void SetSV(float S, float V)
    {
        currentSat = S;
        currentVal = V;
        
        UpdateOutputImage();
    }

    public void UpdateSVImage()
    {
        currentHue = hueSlider.value;

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x/svTexture.width, (float)y/ svTexture.height));
            }
        }
        
        svTexture.Apply();
        
        UpdateOutputImage();
    }

    public void OnHexInput()
    {
        if(hexInputField.text.Length < 6) {return;}

        Color newCol;

        if (ColorUtility.TryParseHtmlString("#" + hexInputField.text, out newCol))
        {
            Color.RGBToHSV(newCol, out currentHue, out currentSat, out currentVal);
        }

        hueSlider.value = currentHue;
        UpdateOutputImage();
    }
    
    public void UpdateRGBImage()
    {
        float r, g, b;

        if (float.TryParse(redInputField.text, out r) &&
            float.TryParse(greenInputField.text, out g) &&
            float.TryParse(blueInputField.text, out b))
        {
      
            r = Mathf.Clamp01(r / 255f);
            g = Mathf.Clamp01(g / 255f);
            b = Mathf.Clamp01(b / 255f);

            Color newColor = new Color(r, g, b);
            Color.RGBToHSV(newColor, out currentHue, out currentSat, out currentVal);

            hueSlider.value = currentHue;

            UpdateOutputImage();
        }
    }
}
