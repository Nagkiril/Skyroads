using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skyroad.Collision;
using Skyroad.Player;

namespace Skyroad.Collision
{
    //Collision classes will tend to be empty bc CollisionZone covers their code; however, they will provide us with an easy way to setup specific types with next to 0 boilerplate code
    //We can also either track an abstract class or a series of subclasses using multiple CollisionZone scripts attached, as per our choice, allowing us to retain flexibility no matter how our prototype shall expand
    public class PlayerCollisionZone : CollisionZone<IPlayer>
    {
        //Still, I can't deny that having empty classes feels at least a little bit weird.
    }
}