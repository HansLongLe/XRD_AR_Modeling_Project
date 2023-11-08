using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float speed = 500.0f;
    
    public GameObject gameObject;

    public void MoveForward()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    
    public void MoveBackward()
    {
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed);
    }
    
    public void MoveLeft()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
    
    public void MoveRight()
    {
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
    
    public void MoveUp()
    {
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
    
    public void MoveDown()
    {
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}