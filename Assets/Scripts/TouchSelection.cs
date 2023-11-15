using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Common;
using Lean.Touch;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchSelection : MonoBehaviour
{
    private GameObject currentModel;

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    public delegate void SelectedModelManagerWithPlanePosition(Vector3 planePosition);
    public static event SelectedModelManagerWithPlanePosition OnPlanePosition;
    
    public delegate void TouchSelectionWithSelectedModel(GameObject selectedModel);
    public static event TouchSelectionWithSelectedModel OnSelectionModel;

    private void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        LeanTouch.OnFingerTap += OnSelected;
    }

    private void OnSelected(LeanFinger leanFinger)
    {

        if (!(Input.touchCount > 0)) return;
        var touch = Input.GetTouch(0);
        var ray = Camera.main.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out var hit))
        {
            var selectable = hit.collider.transform.parent.GetComponent<LeanSelectableByFinger>();
            if (selectable != null)
            {
                OnSelectionModel?.Invoke(selectable.gameObject);
            }
            else if(!(EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0)))
            {
                OnSelectionModel?.Invoke(null);
            }
        }
        else if(!(EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0)))
        {
            OnSelectionModel?.Invoke(null);
        }
        
        if (!arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon)) return;
        var planeHit = hits[0];
        OnPlanePosition?.Invoke(arPlaneManager.GetPlane(planeHit.trackableId).transform.position);
    }
    

    private void OnDestroy()
    {
        LeanTouch.OnFingerTap -= OnSelected;
    }
}
