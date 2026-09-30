using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public bool vertical = false;
    public float changeTime = 3.0f;

    bool broken = true;

    float timer;
    int direction = 1;

    Rigidbody2D rigidbody;
    Animator animator;

    AudioSource audioSource;

    public ParticleSystem smokeParticleEffect;

    public bool isBroken { get { return broken; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        if (rigidbody != null)
        {
            Vector2 position = rigidbody.position;

            if (vertical)
            {
                position.y += direction * speed * Time.deltaTime;
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", direction);
            }
            else
            {
                position.x += direction * speed * Time.deltaTime;
                animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);
            }
                
            rigidbody.MovePosition(position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody.simulated = false;
        animator.SetTrigger("Fixed");
        audioSource.Stop();
        smokeParticleEffect.Stop();
    }
}
