using UnityEngine;

[CreateAssetMenu(menuName = "Chrono Data")]
public class ChronoData : ScriptableObject
{
    [Header("Chrono Projectile Speed")]
    public float projectileSpeed;

    [Space(5)]
    [Header("Chrono Zone Active Time")]
    public float chronoZoneActiveTime;
}