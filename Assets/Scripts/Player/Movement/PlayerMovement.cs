using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Player.Movement
{
    public abstract class PlayerMovement : MonoBehaviour
    {


        public abstract void ApplySway(float sway);
        public abstract void SetBoost(bool isBoosted);
    }
}