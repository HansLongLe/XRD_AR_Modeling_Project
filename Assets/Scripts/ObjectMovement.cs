using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float speed = 500.0f;
    public GameObject gameObjectToMove;
    private Vector3 movementDirection = Vector3.zero;

    public void OnPointerDown(PointerEventData eventData)
    {
        string buttonName = gameObject.name;
        switch (buttonName)
        {
            case "ZForward":
                movementDirection = Vector3.forward;
                break;
            case "ZBackward":
                movementDirection = Vector3.back;
                break;
            case "XLeft":
                movementDirection = Vector3.left;
                break;
            case "XRight":
                movementDirection = Vector3.right;
                break;
            case "YUp":
                movementDirection = Vector3.up;
                break;
            case "YDown":
                movementDirection = Vector3.down;
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        movementDirection = Vector3.zero;
    }

    private void Update()
    {
        gameObjectToMove.transform.Translate(movementDirection.normalized * Time.deltaTime * speed);
    }
}