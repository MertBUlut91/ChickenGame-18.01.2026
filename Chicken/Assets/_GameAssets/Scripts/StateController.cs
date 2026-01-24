using UnityEngine;

public class StateController : MonoBehaviour
{

    private PlayerState currentPlayerState = PlayerState.Idle;

    void Start()
    {
        ChanceState(PlayerState.Idle);
    }

    private void Update()
    {
        Debug.Log(currentPlayerState.ToString());
    }

    public void ChanceState(PlayerState newPlayerstate)
    {
        if (currentPlayerState == newPlayerstate) return;

        currentPlayerState = newPlayerstate;
    }

    public PlayerState GetCurrentState()
    {
        return currentPlayerState;
    }

}
