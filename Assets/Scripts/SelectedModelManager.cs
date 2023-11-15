using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SelectedModelManager : MonoBehaviour
{

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    private Camera mainCamera;
    private static CreateModel selectedModel;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        CreateModel.OnSendSelectedModel += SetModel;
    }

    private void Update()
    {
        // Check for touch input
        if (Input.touchCount <= 0) return;
        var touch = Input.GetTouch(0); 

        if (touch.phase == TouchPhase.Began)
        {
            FingerDown(touch);
        }
    }
    

    private void SetModel(CreateModel model)
    {
        if (selectedModel)
        {
            StartCoroutine(selectedModel.ScaleObjectDown());
        }

        if (model != null)
        {
            selectedModel = model;
            StartCoroutine(selectedModel.ScaleObjectUp());
        }
        selectedModel = model;
        
    }
    

    private void FingerDown(Touch touch)
    {
        if (selectedModel == null) return;
        var ray = mainCamera.ScreenPointToRay(touch.position);
        if (!arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon)) return;
        var hit = hits[0];
        var pose = hit.pose;
        var obj = Instantiate(selectedModel.getPrefabModel(), new Vector3(pose.position.x, pose.position.y + 0.05f, pose.position.z), pose.rotation);
        if (arPlaneManager.GetPlane(hit.trackableId).alignment == PlaneAlignment.HorizontalUp)
        {
            var position = obj.transform.position;
            position.y = 0f;
            var cameraPosition = mainCamera.transform.position;
            var direction = cameraPosition - position;
            obj.transform.LookAt(mainCamera.transform);
            obj.transform.rotation = Quaternion.Euler(0, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z);
        }
        StartCoroutine(selectedModel.ScaleObjectDown());
        selectedModel = null;
    }

    private void OnDestroy()
    {
        CreateModel.OnSendSelectedModel -= SetModel;
    }
}
