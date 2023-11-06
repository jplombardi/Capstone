using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float obstacleProbability;
    public enum gridState {UNK, SAFE, OBSTACLE, FIRE };

    public Color unknownColor = Color.gray;
    public Color safeColor = Color.white;
    public Color obstacleColor = Color.black;
    public Color fireColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        _material = gameObject.GetComponent<Renderer>().material;
        _material.color = unknownColor;
    }

    // Update is called once per frame
    void Update()
    {
        //potentially put fire growth in here?
    }

    public void UpdateState(gridState state)
    {
        if (state == gridState.UNK) { _material.color = unknownColor; }
        if (state == gridState.SAFE) { _material.color = safeColor; }
        if (state == gridState.OBSTACLE) { _material.color = obstacleColor; }
        if (state == gridState.FIRE) { _material.color = fireColor; }
    }
}
