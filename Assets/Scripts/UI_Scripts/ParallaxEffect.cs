using UnityEngine;
using UnityEngine.UI;

public class ParallaxEffect : MonoBehaviour
{    
    [SerializeField] float modifier;
    [SerializeField] float time;
    [SerializeField] Camera mainCamera;

    private Vector2 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, startPos.x + (mousePos.x * modifier), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, startPos.y + (mousePos.y * modifier), 2f * Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);
    }
}
