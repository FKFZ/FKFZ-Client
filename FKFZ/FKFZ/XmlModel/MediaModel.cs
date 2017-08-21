using System;
using System.Collections.Generic;

namespace FKFZ.XmlModel
{
    public class MediaModel
    {
        public MediaModel() { }

        public int Id { get; set; }
        public String Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public String TextPath { get; set; }
        public List<PicModel> PicturePath { get; set; }
        public String AbsPath { get; set; }
    }

    public class PicModel
    {
        public PicModel() { }
        public int Id { get; set; }
        public String Path { get; set; }
    }
}
