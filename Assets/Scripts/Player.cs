using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = -9.8f;
    public float strength = 5.0f;
    public Sprite[] sprites;

    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.tag == "ScoreZone")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
