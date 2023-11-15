using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaneManager : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;

    private ARPlane activePlane;

    private void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        // Example: Raycast from the center of the screen
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        var hits = new List<ARRaycastHit>();

        if (!arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon)) return;
        var detectedPlane = arPlaneManager.GetPlane(hits[0].trackableId);

        // Check if the detected plane is different from the active plane
        if (detectedPlane == activePlane) return;
        // Disable the current active plane
        if (activePlane != null)
        {
            activePlane.gameObject.SetActive(false);
        }

        // Enable the detected plane
        detectedPlane.gameObject.SetActive(true);

        // Set the detected plane as the active plane
        activePlane = detectedPlane;
    }
}