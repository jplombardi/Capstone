using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LidarController : MonoBehaviour
{
    public Boolean debug = true; // turn on or off the white debug lines
    public Boolean _enabled = true;
    // Serialize field is used to force private variables to show in the Unity environment
    [SerializeField] private float angularResolution = 1.0f; // angular resolution 360 / resolution = num samples
    [SerializeField] private float rangeMeters = 30.0f; // Range of the Lidar sensor in meters
    [SerializeField] private float frequencyOfMeasurement = 10; // Full 360 samples/second
    [SerializeField] private GameObject parent; // use to get the transform (position & rotation) of parent object
    private float[] dataPoints; // stores distance taken at angle = resolution*[i]
    private int numShots; // used to iterate through dataPoints
    private int layerMask = 0; //use this for later. Smoke layer will cause collision!
    void Start()
    {
        numShots = (int)(360 / angularResolution); // as above in variable declaration
        dataPoints = new float[numShots]; //instantiate the empty array
        //parent = GetComponentInParent<GameObject>();
        InvokeRepeating("FireLIDAR", 1 / frequencyOfMeasurement, 1 / frequencyOfMeasurement); //call the "FireLIDAR" function at frequency specified.
    }
    private void FireLIDAR()
    {
        if (enabled) //If we're enabled, start shooting those lasers!
        {
            for (int i = 0; i < numShots; i++) //iterate through the full 360 at specified resolution
            {
                Vector3 dir = Quaternion.AngleAxis(-i * angularResolution, parent.transform.up) * parent.transform.forward; // direction of next ray
                Ray lidarLaser = new Ray(parent.transform.position, dir); //definition of the specific ray to be cast. From current position at angle of 'dir'.
                RaycastHit hit; // define the struct for storing the points that hit.
                if (Physics.Raycast(lidarLaser, out hit, rangeMeters)) // if hit, then we store the distance the hit was at/
                {
                    dataPoints[i] = hit.distance; //store the hit distance.
                                                  // If we're debugging, show what exactly is going on.
                    if (debug) { Debug.DrawRay(parent.transform.position, hit.point - parent.transform.position, Color.white, 1 / frequencyOfMeasurement); }
                }
                else //otherwise, we've got maximum range, and that's a good thing!
                {
                    dataPoints[i] = rangeMeters;
                }
            }
        }
    }

    // We will call this from the fusion algorithm to get the data needed. 
    public float[] getDataPoints()
    {
        return dataPoints;
    }
}
