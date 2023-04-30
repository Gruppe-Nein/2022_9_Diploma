using System.Collections;
using UnityEngine;

public class RoseMovement : MonoBehaviour
{
    private RoseBrain _rb;
    public bool reached = true;

    private void Awake()
    {
        _rb = GetComponent<RoseBrain>();
    }

    public void MoveTo(float speed, Vector3 piggyBank, bool isMovingToPiggy)
    {
        Vector2 target = piggyBank;
        Vector2 newPos = Vector2.MoveTowards(_rb.rb.position, target, speed * Time.deltaTime);
        _rb.rb.MovePosition(newPos);

        if (_rb.transform.position == piggyBank && !_rb.CandyEaten && isMovingToPiggy && reached)
        {
            Debug.Log("ddx");
            StartCoroutine(EatCandy());
            reached = false;
        }
    }

    private IEnumerator EatCandy()
    {
        yield return new WaitForSeconds(5);
        _rb.CandyEaten = true;
    }
}
