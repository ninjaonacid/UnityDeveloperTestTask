using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public Action WayPointReached;
    private List<Vector3> _wayPoints;
    private Vector3 _currentWayPoint;
    private bool _isWayPointReached = false;
    public bool IsWayPointReached
    {
        get { return _isWayPointReached; }
        set
        {
            if(!_isWayPointReached && value == true)
            {
                WayPointReached?.Invoke();
            }
            _isWayPointReached = value;
        }
    }

    private float _speed = 3;
    private float maxSpeed = 50;
    private float minSpeed = 5;
    public float Speed
    {
        get { return _speed; }
        set 
        { 
            _speed = value;
            if(_speed > maxSpeed)
            {
                _speed = maxSpeed;
            } else if(_speed < minSpeed)
            {
                _speed = minSpeed;
            }
        }
    }

    private void Start()
    {
        _wayPoints = new List<Vector3>();
        LinePath.wayPointPosition.Add(transform.position);
        _wayPoints.Clear();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
           Vector3 _mousePos2d = Input.mousePosition;
           Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(
                                          _mousePos2d.x, 
                                          _mousePos2d.y,
                                          -Camera.main.transform.position.z));
            AddWayPoint(target);
            
        }

        WayPointSwitch();

        MovementLogic(_currentWayPoint);

        
    }
    private void AddWayPoint(Vector3 target)
    {
        _wayPoints.Add(target);
        LinePath.wayPointPosition.Add(target);

    }

    private void WayPointSwitch()
    {
        while (_isWayPointReached && _wayPoints.Count > 0)
        {
            int currentWayPoint = 0;
            _currentWayPoint = _wayPoints[currentWayPoint];
            _wayPoints.RemoveAt(currentWayPoint);
            break;
        }
    }

    private void MovementLogic(Vector3 currentWayPoint)
    {
        float move = _speed * Time.deltaTime;
        if (transform.position != currentWayPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                     currentWayPoint, move);
            IsWayPointReached = false;

        }
        else if (transform.position == currentWayPoint)
        {
            IsWayPointReached = true;
        }
        
    }

    
}
