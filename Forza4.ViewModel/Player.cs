using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forza4.Engine;

namespace Forza4.ViewModel
{
    public class PlayerVM
    {
        public string Name { get; set; }

        public PlayerType Type { get; set; }

        public Color Color { get; set; }

        public bool CanChange { get; set; }
    }
}
