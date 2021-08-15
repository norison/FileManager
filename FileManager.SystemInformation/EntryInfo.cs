﻿using System.IO;

namespace FileManager.SystemInformation
{
    public class EntryInfo
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public FileAttributes Attributes { get; set; }
        public string Extenstion { get; set; }
    }
}