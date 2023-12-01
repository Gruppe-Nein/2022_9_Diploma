using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BossPlatfrom : MonoBehaviour
{
    [HideInInspector] public List<GameObject> cogwheels = new List<GameObject>();
    [SerializeField] RoseBrain _rose;
    private bool _cogIsActive;

    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _rosePosition;
    private int _index;
    private bool _isMoving;

    void Start()
    {
        SetCogwheels();
        _cogIsActive = true;
        _isMoving = false;
    }

    private void Update()
    {
        if (_rose.checkState() == "IdleRoseState")
        {
            _cogIsActive = true;
        }

        if (_isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _points[_index].position, Time.deltaTime * 3f);

            if (Vector2.Distance(transform.position, _points[_index].position) <= 0.1f)
            {
                _isMoving = false;
            }
        }

        if (!_isMoving && cogwheels.Count == 0 && _rose.transform.position == _rosePosition.position)
        {
            MoveToPosition(0);
        }
    }

    public void MoveToPosition(int index)
    {
        _index = index;
        _isMoving = true;
    }

    private void SetCogwheels()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).CompareTag("BossCogwheel"))
                cogwheels.Add(transform.GetChild(i).gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone") && _rose.checkState() == "MoveToLeftState" || _rose.checkState() == "MoveToRightState")
        {
            DeactiveCogwheel();
        }
    }

    private void DeactiveCogwheel()
    {
        if (_cogIsActive)
        {
            _cogIsActive = false;
            var cogwheel = cogwheels.First();
            cogwheel.SetActive(false);
            cogwheels.Remove(cogwheel);
        }
    }
}
