using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NSE_Framework
{
    public class Write
    {
        public Write(string FilePath)
        {
            // Set this Write's Filepath
            this.FilePath = FilePath;
        }

        public string FilePath;
        FileStream Stream;

        public void WriteBytes(byte[] WriteBytes, int Offset)
        {
            // Creating the stream and Binary writer
            Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            BinaryWriter bw = new BinaryWriter(this.Stream);
            // Setting the binary-writer position
            bw.Seek(Offset, SeekOrigin.Begin);

            // Write our array
            bw.Write(WriteBytes);
            bw.Close();
        }

        public void WriteByte(byte WriteByte, int Offset)
        {
            // Creating the stream and Binary writer
            Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            BinaryWriter bw = new BinaryWriter(this.Stream);
            // Setting the binary-writer position
            bw.Seek(Offset, SeekOrigin.Begin);

            // Write our byte
            bw.Write(WriteByte);
            bw.Close();
        }
    }
}
