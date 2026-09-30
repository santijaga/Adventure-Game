using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectableClip;
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null && controller.health < controller.maxHealth)
        {
            controller.ChangeHealth(1);
            controller.PlaySound(collectableClip);
            Destroy(gameObject);
        }
    }
}
