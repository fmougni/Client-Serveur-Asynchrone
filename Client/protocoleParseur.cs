using System;
using System.Collections.Generic;


    public class protocoleParseur
    {
        public int version { get; set; }
        public List<String> encryption { get; set; }
        public List<String> compression { get; set; }
        public protocoleParseur(int version,List<String> encryption , List<String> compression)
        {
            this.version = version;
            this.encryption = encryption;
            this.compression = compression;
        }
    public protocoleParseur()
    {
    }
}
