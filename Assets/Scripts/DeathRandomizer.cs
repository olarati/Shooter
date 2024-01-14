using UnityEngine;

public class DeathRandomizer : StateMachineBehaviour
{
    private const string DeathIdKey = "DeathId";
    private const int _animationCount = 3;

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger(DeathIdKey, Random.Range(0, _animationCount));
    }
}
