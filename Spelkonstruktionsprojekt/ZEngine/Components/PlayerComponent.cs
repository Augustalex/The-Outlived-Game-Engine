﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEngine.Components;

namespace Spelkonstruktionsprojekt.ZEngine.Components
{
    public class PlayerComponent : IComponent
    {
        public string Name { get; set; }
        public bool IsHuman { get; set; }

        public IComponent Reset()
        {
            Name = string.Empty;
            IsHuman = false;
            return this;
        }
    }
}
