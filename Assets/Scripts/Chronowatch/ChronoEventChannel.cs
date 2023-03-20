using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chrono Event Channel", menuName = "Chrono/Chrono Event Channel")]
public class ChronoEventChannel : ScriptableObject
{
    public event Action<bool> onChronoZoneDeploy;
    public event Action<bool> OnChronoZoneActive;

    public void ChronoZoneDeploy(bool isDeployed)
    {
        onChronoZoneDeploy?.Invoke(isDeployed);
    }

    public void ChronoZoneActive(bool isActive)
    {
        OnChronoZoneActive.Invoke(isActive);
    }
}
