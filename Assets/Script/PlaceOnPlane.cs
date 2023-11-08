using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject placePrefeb;
    GameObject spawnObject;
    private bool isTouch = false;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        

        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon) && !isTouch)
        {
            var hitPose = hits[0].pose;
            if (spawnObject == null)
            {
                spawnObject = Instantiate(placePrefeb, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnObject.transform.position = hitPose.position;
                spawnObject.transform.rotation = hitPose.rotation;
                isTouch = true;

            }
        }
    }
}
