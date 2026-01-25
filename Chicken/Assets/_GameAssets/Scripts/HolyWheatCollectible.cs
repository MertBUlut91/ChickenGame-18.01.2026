using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private float increaseMoveSpeed;
    [SerializeField] private float increaseJumpForce;
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
        playerController.SetJumpForce(increaseJumpForce, duration);
        Destroy(gameObject);
    }
}
