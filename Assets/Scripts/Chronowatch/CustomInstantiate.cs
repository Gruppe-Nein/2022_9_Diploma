using System;
using UnityEngine;

public class CustomInstantiator<T>
{
    private CWatchProjectile _gameObject;
    private Action<T> _customInstantiator;

    public CustomInstantiator(Action<T> customInstantiator, CWatchProjectile gameobject)
    {
        _gameObject = gameobject;
        _customInstantiator = customInstantiator;
    }

    public CWatchProjectile Instantiate(Vector3 position, Quaternion rotation)
    {
        CWatchProjectile instance = GameObject.Instantiate(_gameObject, position, rotation);
        _customInstantiator(instance.GetComponent<T>());
        return instance;
    }
}
