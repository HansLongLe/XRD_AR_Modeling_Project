using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lean.Touch;
using UnityEngine;
using UnityEngine.EventSystems;

public class ARChangeModelOnSelection : MonoBehaviour
{
    [SerializeField] private string modelName;
    private Outline outline;
    private GameObject childGameObject;

    private Camera mainCamera;
    private LeanSelectableByFinger selectableByFinger;

    private Rigidbody rigidbody;

    
    public delegate void ARChangeModelOnSelectionWithSelectedModel(GameObject selectedGameObject);
    
    public static event ARChangeModelOnSelectionWithSelectedModel OnSendSelectedModel;

    // Start is called before the first frame update
    private void Start()
    {
        LeanTouch.OnFingerTap += HandleFingerTap;
        mainCamera = Camera.main;
        outline = gameObject.transform.Find(modelName).GetComponent<Outline>();
        rigidbody = GetComponent<Rigidbody>();
        childGameObject = gameObject.transform.Find(modelName).gameObject;
        selectableByFinger = gameObject.GetComponent<LeanSelectableByFinger>();
        outline.enabled = true;
        OnSendSelectedModel?.Invoke(gameObject);
    }

    private void Update()
    {
        if (!(Input.touchCount > 0)) return;
        var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return;
            var ray = mainCamera.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject == childGameObject)
                {
                    OnSendSelectedModel?.Invoke(gameObject);
                }
                else
                {
                    OnSendSelectedModel?.Invoke(null);
                }
                
            }


    }

    private void HandleFingerTap(LeanFinger finger)
    {
        selectableByFinger.enabled = !finger.IsOverGui;

    }

    private void OnDestroy()
    {
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }
}
