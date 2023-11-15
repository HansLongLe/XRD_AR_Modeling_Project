using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lean.Common;
using Lean.Touch;
using UnityEngine;

public class DeleteModel : MonoBehaviour
{

    private static GameObject targetObject;
    
    private void Start()
    {
        TouchSelection.OnSelectionModel += SetCurrentSelectedModel;
    }
    
    private static void SetCurrentSelectedModel(GameObject model)
    {
        targetObject = model;
    }

    public void DeleteSelectedModel()
    {
        if (targetObject)
        {
            Destroy(targetObject);
        }
    }
}
