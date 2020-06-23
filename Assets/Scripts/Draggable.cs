using UnityEngine;

public class Draggable : MonoBehaviour
{
    public Transform destination;
    public Vector2 initialPosition;
    public bool locked;

    private Collider2D current;
    private float dX, dY; // Delta offset from touch to transform position

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.touchCount > 0 && !locked)
        {
            Touch touch = Input.touches[0];
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Retrieve the offset position from touch to transform object
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition))
                    {
                        dX = touchPosition.x - transform.position.x;
                        dY = touchPosition.y - transform.position.y;
                        current = GetComponent<Collider2D>();
                    } else
                    {
                        current = null;
                    }
                    break;
                case TouchPhase.Moved:
                    if (current)
                    {
                        current.GetComponent<Transform>().transform.position = new Vector2(touchPosition.x - dX, touchPosition.y - dY);
                    }
                    break;
                case TouchPhase.Ended:
                    if (Mathf.Abs(transform.position.x - destination.position.x) <= 2f && Mathf.Abs(transform.position.y - destination.position.y) <= 2f)
                    {
                        transform.position = destination.position;
                        locked = true;
                    }
                    else
                    {
                        transform.position = initialPosition;
                        current = null;
                    }
                    break;
            }
        }
    }
}
