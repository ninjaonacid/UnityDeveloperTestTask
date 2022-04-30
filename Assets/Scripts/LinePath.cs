using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePath : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Movement _movement;
    private int cornerVertices = 6;
    private int capvertices = 6;

    public static List<Vector3> wayPointPosition;
    private void Awake()
    {
        wayPointPosition = new List<Vector3>();   
        wayPointPosition.Clear();
        _lineRenderer = GetComponent<LineRenderer>();
        _movement = GetComponent<Movement>();
        _movement.WayPointReached += DeleteLastPath;
    }

    private void OnDisable()
    {
        _movement.WayPointReached -= DeleteLastPath;
    }

    private void Update()
    {
        SetPath();
    }
    private void SetPath()
    {
        if (_lineRenderer != null )
        {
            _lineRenderer.numCornerVertices = cornerVertices;
            _lineRenderer.numCapVertices = capvertices;
            _lineRenderer.positionCount = wayPointPosition.Count;
            _lineRenderer.SetPositions(wayPointPosition.ToArray());
        } 

    } 

    private void DeleteLastPath()
    {
      if(wayPointPosition.Count > 1 )
        {
            
           wayPointPosition.RemoveAt(0);
        }
        Debug.Log("trigger");
    }

}
