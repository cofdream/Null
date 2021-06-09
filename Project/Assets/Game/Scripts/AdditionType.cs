using System;
using UnityEngine;

namespace Game
{
    [Flags]
    public enum AdditionType : int
    {
        None           = 1 << 0,
        Health         = 1 << 1,
        MaxHealth      = 1 << 2,
        AttackPower    = 1 << 3,
        MagicPower     = 1 << 4,
        PhysicsDefense = 1 << 5,
        MagicDefense   = 1 << 6,
        Speed          = 1 << 7,
    }
}