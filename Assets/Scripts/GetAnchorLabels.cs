using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnchorLabels : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    // Update is called once per frame
    void Update()
    {
        Vector3 controlerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        Vector3 rayDirection = controllerRotation * Vector3.forward;

        if (Physics.Raycast(controlerPosition, rayDirection, out RaycastHit hit))
        {
            _lineRenderer.SetPosition(0, controlerPosition);

            OVRSemanticClassification
                anchor = hit.collider.gameObject.GetComponentInParent<OVRSemanticClassification>();

            if (anchor != null)
            {
                print($"Hit an anchor with the label: {string.Join(", ", anchor.Labels)}");
                Vector3 endPoint = anchor.transform.position;
                _lineRenderer.SetPosition(1, endPoint);
            }
            else
            {
                _lineRenderer.SetPosition(1, hit.point);
            }
        }
    }
}
