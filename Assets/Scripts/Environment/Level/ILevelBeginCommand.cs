using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Environment.Level
{
    public interface ILevelBeginCommand
    {
        public event Action OnBeginCommand;
    }
}