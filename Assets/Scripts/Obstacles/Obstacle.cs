using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Collision;
using Skyroad.Player;
using Skyroad.Pooling;

namespace Skyroad.Obstacles
{
    //Please note that obstacles aren't considered Environment, because they're a critical gameplay piece that (in many forms it can potentially take) is intended as an enemy\antagonist
    //If we really wanted reusability\speed of modification, we could make a special pickup system that game designers could cook up any kind of road object with; this would be overengeneering for this protytype, however.
    public sealed class Obstacle : PoolableBehaviour
    {
        [SerializeField] private PlayerCollisionZone collision;

        private void Awake()
        {
            //We're expecting collision to be our component, thus we do not unsubscribe from this event (as collision should be destroyed with this script simultaneously)
            collision.OnEnter += OnPlayerEntry;
        }

        private void OnPlayerEntry(IPlayer player)
        {
            player.Kill();
        }
    }
}