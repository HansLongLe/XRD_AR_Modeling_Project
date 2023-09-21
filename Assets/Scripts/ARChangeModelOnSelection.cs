using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARChangeModelOnSelection : MonoBehaviour
{
    private Outline outline;
    // Start is called before the first frame update
    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void OnSelected()
    {
        outline.enabled = true;
    }

    public void OnDeselected()
    {
        outline.enabled = false;
    }
}
