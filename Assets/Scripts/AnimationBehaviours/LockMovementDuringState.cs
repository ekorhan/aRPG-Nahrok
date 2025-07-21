using UnityEngine;

public class LockMovementDuringState : StateMachineBehaviour
{
    // Bu metot, Animator bu duruma (state) GİRDİĞİNDE bir kere çalışır.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Animator'ün bağlı olduğu nesneden PlayerCharacter script'ini bul ve hareketini kilitle.
        PlayerCharacter player = animator.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.LockMovement();
        }
    }

    // Bu metot, Animator bu durumdan ÇIKTIĞINDA bir kere çalışır.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Animator'ün bağlı olduğu nesneden PlayerCharacter script'ini bul ve hareket kilidini aç.
        PlayerCharacter player = animator.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.UnlockMovement();
        }
    }
}