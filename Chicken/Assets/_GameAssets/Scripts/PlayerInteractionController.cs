using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Consts.WheatType.GOLD_WHEAT))
        {
            other.gameObject?.GetComponent<GoldWheatCollectible>().Collect();
        }

        if (other.gameObject.CompareTag(Consts.WheatType.HOLY_WHEAT))
        {
            other.gameObject?.GetComponent<HolyWheatCollectible>().Collect();
        }

        if (other.gameObject.CompareTag(Consts.WheatType.ROTTEN_WHEAT))
        {
            other.gameObject?.GetComponent<RottenWheatCollectible>().Collect();
        }
    }
}
