
using UnityEngine;

public class BossStateBehaviour : StateMachineBehaviour
{
    [Range(0, 2)]
    public int faixaVida;

    [Range(0, 2)]
    public int comportamento;

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        Enemy chefe = animator.GetComponent<Enemy>();

        if (chefe == null)
            return;

        int faixaAtual = animator.GetInteger("FaixaVida");

        if (faixaAtual != faixaVida)
            return;

        chefe.DefinirComportamento(faixaVida, comportamento);
    }
}
