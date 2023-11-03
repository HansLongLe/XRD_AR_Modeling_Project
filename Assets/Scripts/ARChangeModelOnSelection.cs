using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARChangeModelOnSelection : MonoBehaviour
{
    [SerializeField] private string modelName;
    private Outline outline;
    private GameObject childGameObject ;

    private Camera mainCamera;
    
    public delegate void ARChangeModelOnSelectionWithSelectedModel(GameObject selectedGameObject);

    public static event ARChangeModelOnSelectionWithSelectedModel OnSendSelectedModel;

    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
        outline = gameObject.transform.Find(modelName).GetComponent<Outline>();
        childGameObject = gameObject.transform.Find(modelName).gameObject;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        var ray = mainCamera.ScreenPointToRay((Input.mousePosition));
        if (!Physics.Raycast(ray, out var hit)) return;
        if(hit.collider.gameObject == childGameObject)
        {
            outline.enabled = true;
            OnSendSelectedModel?.Invoke(gameObject);
        }
        else
        {
            outline.enabled = false;
        }

    }
}
