using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnModel : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    Slider slider;
    
    public TrackableType type;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    List<GameObject> objects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.PlaneWithinBounds))
            {
                /*
                float angVal = 0;
                try
                {
                    if ((angVal = Math.Abs(float.Parse(ang.text))) > 360)
                        throw new Exception();
                }
                catch (System.Exception)
                {
                    ang.text = "角度が不正です";
                    return;
                }
                */
                objects.Add(Instantiate(obj, hitResults[0].pose.position, Quaternion.identity));
            }
        }
    }
    public void OnSliderChanged()
    {
        objects.ForEach(item => { item.transform.localScale = new Vector3(slider.value, slider.value, slider.value); });
    }

    public void DestroyAll()
    {
        objects.ForEach(item => { Destroy(item); });
        objects.Clear();
    }
}
