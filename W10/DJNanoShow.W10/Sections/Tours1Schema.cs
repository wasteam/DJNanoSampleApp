using System;
using AppStudio.DataProviders;

namespace DJNanoShow.Sections
{
    /// <summary>
    /// Implementation of the Tours1Schema class.
    /// </summary>
    public class Tours1Schema : SchemaBase
    {

        public string Where { get; set; }

        public string When { get; set; }

        public string WhereFrom { get; set; }

        public DateTime? WhenDate { get; set; }

        public string Month { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
