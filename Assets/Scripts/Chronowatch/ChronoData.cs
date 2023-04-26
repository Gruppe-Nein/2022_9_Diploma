using UnityEngine;

[CreateAssetMenu(fileName = "Chrono Data", menuName = "Chrono/Chrono Data")]
public class ChronoData : ScriptableObject
{
    [Header("Chrono Projectile Speed")]
    public float projectileSpeed = 10f;

    [Header("Chrono Projectile Speed")]
    public float projectileReturnSpeed = 500f;

    [Space(5)]
    [Header("Chronowatch original position")]
    public Vector3 offsetPosition = new Vector3(-0.65f, 0.65f, 0);
    [Header("Diactivation threshold distance")]
    public float thresholdDistance = 6f;

    [Space(5)]
    [Header("Chrono Zone Active Time")]
    public float chronoZoneActiveTime = 5f;

    [Space(5)]
    [Header("Activation timeout")]
    public float activTimeOut = 5f;

    [Space(5)]
    [Header("Velocity Stopping factor")]
    public float velocityFactor = 0.25f;
}