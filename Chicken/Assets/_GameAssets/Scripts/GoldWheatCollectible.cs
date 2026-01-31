using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{

    [SerializeField] private WheatDesignSO wheatDesignSO;
    [SerializeField] private PlayerController playerController;

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
        Destroy(gameObject);
    }
}
