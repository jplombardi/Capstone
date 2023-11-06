using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorProcessor : MonoBehaviour
{
    [SerializeField] LidarController _lidar;
    [SerializeField] RadarController _radar;
    [SerializeField] ThermalController _thermal;
    [SerializeField] VisibleController _camera;
    [SerializeField] BuildOccupancyGrid _grid;
    [SerializeField] float processorFrequency = 4;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ProcessSensors", 1 / processorFrequency, 1 / processorFrequency);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void ProcessSensors()
    {
        //Here we build a representation of the environment, and update the grid! :)

        //LIDAR heres goes nothing
        Vector3[] LIDAR_PTS = _lidar.getDataPoints();

        for(int i = 0; i < LIDAR_PTS.Length - 1; i++) //Cool! I can update obstacles now. How about showing what is free space?
        {
            _grid.UpdateGridSpaceVector3(LIDAR_PTS[i], GridController.gridState.OBSTACLE);
            //Possibly here, iterate along line from this transform to the grid spot that is an obstacle. 
        }


        //Finally, the grid gets updated with the data we have.
        
    }
}
