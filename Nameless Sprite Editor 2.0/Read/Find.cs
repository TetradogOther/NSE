using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NSE2
{
    public class Find
    {
        public Find(String FilePath)
        {
            this.FilePath = FilePath;
            write = new Write(FilePath);
        }

        public string FilePath;
        FileStream Stream;
        Write write;

        public int FindFreeSpace(int StartOffset, int Size)
        {
            int _return = -1;
            int FindPos = StartOffset;
            int check = -1;
            int Window = 0xFFFFFF;
            //Creating and filling search buffer
            byte[] SearchBytes = new byte[Size - 1];
            for (int i = 0; i < Size - 1; i++)
            {
                SearchBytes[i] = 0xFF;
            }

            //Creating ReadBuffer
            byte[] ReadBytes;

            Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(this.Stream);
            br.BaseStream.Seek(FindPos, SeekOrigin.Begin);
            while (check == -1 && FindPos < Stream.Length)
            {
                ReadBytes = br.ReadBytes(Window);
                check = FindBytes(ReadBytes, SearchBytes);
                if (check != -1)
                {
                    _return = FindPos + check;
                }
                else
                {
                    FindPos += Window - SearchBytes.Length;
                }
            }
            br.Close();
            return _return;

        }

        public int FindBytes(byte[] Bytes, byte[] SearchBytes, int Offset = 0)
        {
            int _return = -1;
            int fpos2 = 0;
            bool compatible = false;
            while (!(Offset == Bytes.Length - SearchBytes.Length | _return != -1 | Offset == Bytes.Length))
            {
                if (Bytes[Offset] == SearchBytes[0] & Bytes[Offset + 1] == SearchBytes[1])
                {
                    compatible = true;
                    fpos2 = 0;
                    while (!(fpos2 == SearchBytes.Length - 1 || compatible == false))
                    {
                        if (Bytes[Offset + fpos2] != SearchBytes[fpos2])
                        {
                            compatible = false;
                        }
                        fpos2 = fpos2 + 1;
                    }
                    if (compatible == true)
                    {
                        _return = Offset;
                    }
                    else
                    {
                        _return = -1;
                    }

                }
                Offset = Offset + 1;
            }
            return _return;
        }

        public void SearchAndReplace(byte[] Search, byte[] Replace, List<int> Pointers = null)
        {
            if (Search.Length == Replace.Length)
            {
                int check = -1;
                int window = 0xFFFFF;
                int SearchPos = 0;
                int Offset = 0;
                byte[] ReadBytes;

                Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(this.Stream);
                
                while (!(SearchPos >= Stream.Length - Search.Length - 1))
                {
                    br.BaseStream.Seek(SearchPos, SeekOrigin.Begin);
                    ReadBytes = br.ReadBytes(window);
                    check = FindBytes(ReadBytes, Replace);
                        
                    while (check != -1)
                    {    
                        Offset = SearchPos + check;
                        write.WriteBytes(Replace, Offset);
                        if (Pointers != null)
                        {
                            Pointers.Add(Offset);
                        }
                        check = FindBytes(ReadBytes, Replace, check + Search.Length);

                    }
                    SearchPos += window - Search.Length;                 
                }

            }
            else
            {
                throw new Exception("Search and Replace must be the same length");
            }
        }
        
    }
}
