using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public Button rightButton;
    public Button leftButton;

    public Rigidbody2D paddleRigidbody { get; private set; }

    public Vector2 direction { get; private set; }

    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private void Awake()
    {
        this.paddleRigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnLeftButton()
    {
        direction = Vector2.left;
    }

    public void OnRightButton()
    {
        direction = Vector2.right;
    }

    public void OnButtonUp()
    {
        direction = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (this.direction != Vector2.zero)
        {
            this.paddleRigidbody.AddForce(this.direction * this.speed);
        }
    }

    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.paddleRigidbody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            AudioManager.instance.Play("Jump");
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.GetComponent<Rigidbody2D>().velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.up * ball.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
    }
}
