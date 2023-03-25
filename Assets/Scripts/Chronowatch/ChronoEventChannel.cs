using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Chrono Event Channel", menuName = "Chrono/Chrono Event Channel")]
public class ChronoEventChannel : ScriptableObject
{
    public event Action<bool> onWatchProjectileDeploy;
    public event Action<bool> onChronoZoneDeploy;

    public event Action<bool> onCheckPointRestore;    

    //public event Action<bool> OnChronoZoneActive;

    public void WatchProjectileDeploy(bool isDeployed)
    {
        onWatchProjectileDeploy?.Invoke(isDeployed);
    }

    public void ChronoZoneDeploy(bool isDeployed)
    {
        onChronoZoneDeploy?.Invoke(isDeployed);
    }

    public void CheckPointRestore(bool reset)
    {
        onCheckPointRestore?.Invoke(reset);
    }

    /*public void ChronoZoneActive(bool isActive)
    {
        OnChronoZoneActive.Invoke(isActive);
    }*/
}
