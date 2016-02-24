using System;
using AppStudio.DataProviders;

namespace DJNanoShow.Sections
{
    /// <summary>
    /// Implementation of the Discography1Schema class.
    /// </summary>
    public class Discography1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string LabelName { get; set; }

        public string Genre { get; set; }

        public string Link { get; set; }
    }
}
