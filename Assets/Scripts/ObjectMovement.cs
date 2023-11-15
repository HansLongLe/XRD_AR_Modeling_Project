using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class ObjectMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float speed = 0.5f;
    private static GameObject gameObjectToMove;
    private Vector3 planePosition;
    private Vector3 movementDirection = Vector3.zero;

    private void Start()
    {
        TouchSelection.OnSelectionModel += SetCurrentSelectedModel;
        TouchSelection.OnPlanePosition += SetPlanePosition;
        var planeManager = FindObjectOfType<ARPlaneManager>();
    }
    
    private static void SetCurrentSelectedModel(GameObject model)
    {
        gameObjectToMove = model;
    }
    
    private void SetPlanePosition(Vector3 position)
    {
        planePosition = position;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        string buttonName = gameObject.name;
        switch (buttonName)
        {
            case "ZForward":
                movementDirection = Vector3.back;
                break;
            case "ZBackward":
                movementDirection = Vector3.forward;
                break;
            case "XLeft":
                movementDirection = Vector3.right;
                break;
            case "XRight":
                movementDirection = Vector3.left;
                break;
            case "YUp":
                movementDirection = Vector3.up;
                break;
            case "YDown":
                if (gameObjectToMove != null && gameObjectToMove.transform.position.y > planePosition.y)
                {
                    movementDirection = Vector3.down;
                }
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        movementDirection = Vector3.zero;
    }

    private void Update()
    {
        if (gameObjectToMove != null)
        {
            gameObjectToMove.transform.Translate(movementDirection.normalized * Time.deltaTime * speed);
        }
    }

    private void OnDestroy()
    {
        TouchSelection.OnSelectionModel -= SetCurrentSelectedModel;
        TouchSelection.OnPlanePosition -= SetPlanePosition;
    }
}