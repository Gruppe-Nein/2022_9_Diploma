using UnityEngine;

/// <summary>
/// Interface to group objects that can use teleports
/// </summary>
public interface ITeleportable
{
    public void Teleport(Vector3 position);
}
