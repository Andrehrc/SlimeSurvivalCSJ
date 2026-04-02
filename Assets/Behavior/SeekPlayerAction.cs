using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SeekPlayer", story: "Agent seeks Player [speed]", category: "Action", id: "64eedd69657525e8f5c24f2931745d88")]
public partial class SeekPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Speed;

    private Transform player;
    private Transform self;

    private EnemyHealth enemyHealth;

    protected override Status OnStart()
    {
        enemyHealth = GameObject.GetComponent<EnemyHealth>();
        player = PlayerController.Instance.transform;
        self = GameObject.transform;

        if (player == null)
        {
            return Status.Failure;
        }
        else
        {
            return Status.Running;
        }
    }

    protected override Status OnUpdate()
    {
        if (player == null)
            return Status.Failure;

        if (enemyHealth.isKnockedBack)
        {
            return Status.Running;
        }

        Vector3 direction = (player.position - self.position).normalized;
        self.position += direction * Speed * Time.deltaTime;

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

