using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [field: SerializeField]
    public NavMeshAgent Agent { get; set; }
    [field: SerializeField]
    private AttackBehaviour AttackBehaviour { get; set; }
    [field: SerializeField]
    private Animator AnimationController { get; set; }

    private GameObject Target { get; set; }


    void Start()
    {
        GameStateManager.Instance.OnGameLost += Cheer;
        Target = GameObject.FindGameObjectWithTag("TowerWall");
        Agent.SetDestination(Target.transform.position);
        this.transform.LookAt(Target.transform.position);

        AttackBehaviour.SetTarget(Target.GetComponent<IDestroyableBehaviour>());
    }

    public float GetEnemyHeight()
    {
        var renderer = gameObject.GetComponentInChildren<Renderer>();
        return renderer.bounds.size.y;
    }

    public void StopWalking()
    {
        Agent.isStopped = true;
        AnimationController.SetBool("IsWalking", false);
    }

    public void StartWalking()
    {
        Agent.isStopped = false;
        AnimationController.SetBool("IsWalking", true);
    }

    public void Cheer()
    {
        Agent.isStopped = true;
        AnimationController.SetBool("IsWalking", false);
        AnimationController.SetBool("IsAttacking", false);
        AnimationController.SetBool("IsCheering", true);
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameLost -= Cheer;
    }
}
