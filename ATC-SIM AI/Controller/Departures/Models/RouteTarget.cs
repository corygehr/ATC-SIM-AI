using AtcSimController.SiteReflection.Models;

namespace AtcSimController.Controller.Departures.Models
{
    /// <summary>
    /// Routing target information
    /// </summary>
    public class RouteTarget
    {
        private Location _dest;
        private int _heading;
        private int _altitude;
        private bool _expedite;
    }
}
