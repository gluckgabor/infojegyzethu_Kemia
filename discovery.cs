using System;
using System.Collections.Generic;
using System.Text;

namespace infojegyzethu_kemiaiElemekFelfedezese
{
    class discovery
    {
        public string ev { get; set; }
        public string elem { get; set; }
        public string vegyjel { get; set; }
        public int rendszam { get; set; }
        public string felfedezo { get; set; }

        public discovery(string line)
        {
            string[] lineSplitted = line.Split(";");
            ev = lineSplitted[0];
            elem = lineSplitted[1];
            vegyjel = lineSplitted[2];
            rendszam = Convert.ToInt32(lineSplitted[3]);
            felfedezo = lineSplitted[4];
        }
    }
}
