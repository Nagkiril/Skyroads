using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Player.Visuals
{
    public abstract class PlayerVisuals : MonoBehaviour
    {
        //We could also expand it with reaction to boosts, death, et cetera
        public abstract void ApplySway(float sway);
    }
}