using Lean.Touch;
using UnityEngine;

public class ARChangeModelOnSelection : MonoBehaviour
{
    [SerializeField] private string modelName;

    private LeanSelectableByFinger selectableByFinger;

    private Outline outline;

    private GameObject selectedModel;

    private Rigidbody rigidbody;

    private Vector3 planePosition;

    

    // Start is called before the first frame update
    private void Start()
    {
        TouchSelection.OnSelectionModel += ModelSelection;
        TouchSelection.OnPlanePosition += SetPlanePosition;
        outline = gameObject.transform.Find(modelName).GetComponent<Outline>();
        rigidbody = GetComponent<Rigidbody>();
        outline.enabled = true;
        selectableByFinger = GetComponent<LeanSelectableByFinger>();
    }

    private void Update()
    {
        if (planePosition != null)
        {
            if (transform.position.y < planePosition.y)
            {
                transform.position = new Vector3(transform.position.x, planePosition.y, transform.position.z);
            }
        }
    }

    private void ModelSelection(GameObject model)
    {
        if (model && model == gameObject)
        {
            outline.enabled = true;
            rigidbody.isKinematic = true;
            selectableByFinger.enabled = true;

        }
        else
        {
            outline.enabled = false;
            rigidbody.isKinematic = false;
            selectableByFinger.enabled = false;
        }
    }

    private void SetPlanePosition(Vector3 position)
    {
        planePosition = position;
    }
    

    private void OnDestroy()
    {
        TouchSelection.OnSelectionModel -= ModelSelection;
        TouchSelection.OnPlanePosition -= SetPlanePosition;
    }
}
