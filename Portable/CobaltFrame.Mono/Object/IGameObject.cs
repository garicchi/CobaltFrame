using CobaltFrame.Core.Object;
using CobaltFrame.Mono.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Object
{
    public interface IGameObject
    {
        GameInputCollection Inputs { get; set; }
    }
}
