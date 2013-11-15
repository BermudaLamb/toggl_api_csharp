using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    interface IToggl
    {
        int? Id { get; set; }
        string Name { get; set; }
    }
}
