using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Environment.Level
{
    public interface ILevelRestartCommand
    {
        public event Action OnRestartCommand;
    }
}