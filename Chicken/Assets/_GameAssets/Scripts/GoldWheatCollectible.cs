using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private float increaseMoveSpeed;
    [SerializeField] private float duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController = other.gameObject?.GetComponent<PlayerController>();
        }
    }

    public void Collect()
    {
        playerController.SetMovementSpeed(increaseMoveSpeed, duration);
        Destroy(gameObject);
    }
}
