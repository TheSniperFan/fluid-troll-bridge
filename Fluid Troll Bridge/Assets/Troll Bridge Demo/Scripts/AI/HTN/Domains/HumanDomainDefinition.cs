﻿
using FluidHTN;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanDomain", menuName = "AI/Domains/Human")]
public class HumanDomainDefinition : AIDomainDefinition
{
    public override Domain<AIContext> Create()
    {
        return new AIDomainBuilder("Human")
            .Select("Received damage")
                .ReceivedDamage()
            .End()
            .Select("Be tired")
                .BeTired(2f)
            .End()
            .Sequence("Enemy engagement")
                .HasState(AIWorldState.HasEnemyInSight)
                .FindEnemy()
                .Select("Melee or pursue?")
                    .AttackEnemy()
                    .Sequence("Pursue enemy")
                        .MoveToEnemy()
                        .AttackEnemy()
                    .End()
                .End()
            .End()
            .Sequence("Bridge patrol")
                .HasState(AIWorldState.HasBridgesInSight)
                .FindBridge()
                .MoveToBridge()
                .Wait(2f)
            .End()
            .Build();
    }
}