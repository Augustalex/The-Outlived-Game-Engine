﻿using System.Collections.Generic;
using ZEngine.Components;

namespace Spelkonstruktionsprojekt.ZEngine.Components
{
    public class TagComponent : IComponent
    {
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }

    public enum Tag
    {
        Delete = 0
    }
}