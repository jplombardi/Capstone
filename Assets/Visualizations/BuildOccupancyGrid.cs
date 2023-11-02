using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOccupancyGrid : MonoBehaviour
{
    public GameObject gridSquare;
    public float gridSize = 0.1f;
    public int sizeX;
    public int sizeZ;
    public float gridOffset = 0.5f;
    public Vector3 gridOrigin = Vector3.zero;
    public GameObject[,] occupancyGrid;
    public Color unknownColor = Color.gray;
    public Color safeColor = Color.white;
    public Color obstacleColor = Color.black;
    public Color fireColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        occupancyGrid = new GameObject[sizeX, sizeZ];
        SpawnGrid();
    }


    void SpawnGrid()
    {
        for(int x = 0; x < sizeX; x++)
        {
            for(int z = 0; z < sizeZ; z++)
            {
                var spawnedTile = Instantiate(gridSquare, new Vector3(x*gridOffset, 0, z*gridOffset), Quaternion.identity);
                spawnedTile.name = $"Grid {x} {z}";
                occupancyGrid[x, z] = spawnedTile;
            }
        }
    }

    public void UpdateGridSpaceVector3(Vector3 coord)
    {
        //Given a vector3, find the associated grid space, change to color or and update status (danger value?)
        int xpos = Mathf.RoundToInt(coord.x / gridOffset); //Now it's normalized to the grid spacing.
        int zpos = Mathf.RoundToInt(coord.z / gridOffset);

        occupancyGrid[xpos, zpos].GetComponent<Material>().SetColor("_Color", fireColor);
        
    }

}
