using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public SpriteRenderer brickSprite { get; private set; }
    public Sprite[] brickStatesSprite;

    public int health { get; private set; }
    public int points = 100;
    public bool unbreakable;

    private void Awake()
    {
        this.brickSprite = GetComponent<SpriteRenderer>();
        ResetBrick();
    }

    private void Hit()
    {
        if (this.unbreakable)
        {
            return;
        }

        this.health--;

        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.brickSprite.sprite = this.brickStatesSprite[health - 1];
        }

        FindAnyObjectByType<GameManager>().Hit(this);
    }

    public void ResetBrick()
    {
        this.gameObject.SetActive(true);

        if (this.unbreakable)
        {
            this.health = this.brickStatesSprite.Length;
            this.brickSprite.sprite = this.brickStatesSprite[this.health - 1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            AudioManager.instance.Play("Kick");
            Hit();
        }
    }
}
