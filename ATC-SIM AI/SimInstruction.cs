using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_SIM_AI
{
    /// <summary>
    /// Contains all possible sim instructions
    /// </summary>
    enum SimInstruction
    {
        ALTITUDE = 'c',
        DESTINATION = 'c',
        EXPEDITE = 'x',
        LAND = 'l',
        TAKEOFF = 't',
        TURN = 'c',
        SPEED = 's'
    }
}
