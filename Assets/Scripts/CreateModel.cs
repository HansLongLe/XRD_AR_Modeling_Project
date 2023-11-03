using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateModel : MonoBehaviour
{
    [SerializeField]
    private GameObject modelPrefab;
    
    private Vector3 spawnPosition;

    private void Start()
    {
        var mainCamera = Camera.main;
        if (mainCamera != null) spawnPosition = mainCamera.ViewportToWorldPoint((new Vector3(0.5f, 0.7f,  900f)));
    }

    public void Create(){ 
        var instantiatedModel = Instantiate(modelPrefab, spawnPosition, Quaternion.identity);
        instantiatedModel.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
