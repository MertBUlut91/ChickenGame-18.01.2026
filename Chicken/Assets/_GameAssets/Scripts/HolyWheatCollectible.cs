using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WheatDesignSO wheatDesignSO;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController = other.gameObject?.GetComponent<PlayerController>();
        }
    }


    public void Collect()
    {
        playerController.SetMovementSpeed(wheatDesignSO.increaseDecreaseMultiplier, wheatDesignSO.resetBoostDuration);
        playerController.SetJumpForce(wheatDesignSO.increaseDecreaseMultiplier, wheatDesignSO.resetBoostDuration);
        Destroy(gameObject);
    }
}
