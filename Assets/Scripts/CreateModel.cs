using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class CreateModel : MonoBehaviour
{
    [SerializeField]
    private GameObject modelPrefab;
    
    private Vector3 initialScale;
    private Vector3 targetScale;

    public delegate void CreateModelWithSelectedModel(CreateModel selectedGameObject);
    
    public static event CreateModelWithSelectedModel OnSendSelectedModel;

    private bool isSelected = false;


    private void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale * 1.1f;
    }
    
    public void Create()
    {
        OnSendSelectedModel?.Invoke(!isSelected ? this : null);
    }

    public IEnumerator ScaleObjectUp()
    {
        var elapsedTime = 0f;
        const float duration = 0.2f;
        isSelected = true;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var progress = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            yield return null;
        }
    }
    
    public IEnumerator ScaleObjectDown()
    {
        var elapsedTime = 0f;
        const float duration = 0.2f;
        isSelected = false;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var progress = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(targetScale, initialScale, progress);
            yield return null;
        }
    }

    public GameObject getPrefabModel()
    {
        return modelPrefab;
    }
}
