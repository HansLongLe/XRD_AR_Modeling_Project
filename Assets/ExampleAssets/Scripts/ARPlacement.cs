using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{

    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose placementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    private bool isSpawnedObjectNull;
    private Camera camera1;

    private void Start()
    {
        camera1 = Camera.main;
        isSpawnedObjectNull = spawnedObject == null;
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // need to update placement indicator, placement pose and spawn 
    private void Update()
    {
        if(isSpawnedObjectNull && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();


    }

    private void UpdatePlacementIndicator()
    {
        if(isSpawnedObjectNull && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = camera1!.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    private void ARPlaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, placementPose.position, placementPose.rotation);
    }

}
