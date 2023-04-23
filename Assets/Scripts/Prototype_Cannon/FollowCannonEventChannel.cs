using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Follow Cannon Zone Event Channel", menuName = "Cannnon/Cannon Zone Event Channel")]

public class FollowCannonEventChannel : ScriptableObject
{
    public event Action<bool, Transform> OnEnterZoneFollow;

    public void EnterZoneFollow(bool _isFollowing, Transform target)
    {
        OnEnterZoneFollow?.Invoke(_isFollowing, target);
    }
}
