using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NSE_Framework
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

        public int FindFreeSpace(int StartOffset, int Size, bool Safe = true)
        {
            int _return = -1;
            int FindPos = StartOffset;
            if (Safe )
            {
                FindPos = FindPos - FindPos % 4;
            }
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
            while (check == -1 && FindPos < Stream.Length - 513)
            {
                ReadBytes = br.ReadBytes(Window);
                if (Safe )
                {
                    check = FindSafeBytes(ReadBytes, SearchBytes);
                }
                else
                {
                    check = FindBytes(ReadBytes, SearchBytes);
                }
                if (check != -1)
                {
                    if (!Safe)
                    {
                        _return = FindPos + check;
                    }
                    else if (Safe  && (FindPos + check) % 4 == 0)
                    {
                        _return = FindPos + check;
                    }
                    else
                    {
                        check = -1;
                        FindPos += Window - SearchBytes.Length;
                    }
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
                    while (!(fpos2 == SearchBytes.Length || !compatible))
                    {
                        if (Bytes[Offset + fpos2] != SearchBytes[fpos2])
                        {
                            compatible = false;
                        }
                        fpos2 = fpos2 + 1;
                    }
                    if (compatible )
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

        public int FindSafeBytes(byte[] Bytes, byte[] SearchBytes, int Offset = 0)
        {
            Offset = Offset - (Offset % 4);

            int _return = -1;
            int fpos2 = 0;
            bool compatible = false;
            while (!(Offset == Bytes.Length - SearchBytes.Length | _return != -1 | Offset == Bytes.Length))
            {
                if (Bytes[Offset] == SearchBytes[0] & Bytes[Offset + 1] == SearchBytes[1])
                {
                    compatible = true;
                    fpos2 = 0;
                    while (!(fpos2 == SearchBytes.Length || !compatible))
                    {
                        if (Bytes[Offset + fpos2] != SearchBytes[fpos2])
                        {
                            compatible = false;
                        }
                        fpos2 = fpos2 + 1;
                    }
                    if (compatible )
                    {
                        _return = Offset;
                    }
                    else
                    {
                        _return = -1;
                    }

                }
                Offset = Offset + 4;
            }
            return _return;
        }

        public void SearchAndReplace(byte[] Search, byte[] Replace, out List<int> Pointers)
        {
            if (Search.Length == Replace.Length)
            {
                int check = -1;
                int window = 0xFFFFF;
                int SearchPos = 0;
                int Offset = 0;
                byte[] ReadBytes;

                Pointers = new List<int>();

                Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(this.Stream);

                while (!(SearchPos >= Stream.Length - Search.Length - 1))
                {
                    br.BaseStream.Seek(SearchPos, SeekOrigin.Begin);
                    ReadBytes = br.ReadBytes(window);
                    check = FindBytes(ReadBytes, Search);

                    while (check != -1)
                    {
                        Offset = SearchPos + check;
                        write.WriteBytes(Replace, Offset);

                        Pointers.Add(Offset);

                        check = FindBytes(ReadBytes, Search, check + Search.Length);

                    }
                    SearchPos += window - Search.Length;
                }

            }
            else
            {
                throw new Exception("Search and Replace must be the same length");
            }
        }

        public void SearchAndReplace(byte[] Search, byte[] Replace)
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

                        check = FindBytes(ReadBytes, Search, check + Search.Length);

                    }
                    SearchPos += window - Search.Length;
                }

            }
            else
            {
                throw new Exception("Search and Replace must be the same length");
            }
        }

        public List<int> Search(byte[] Search)
        {

            {
                int check = -1;
                int window = 0xFFFFF;
                int SearchPos = 0;
                int Offset = 0;
                byte[] ReadBytes;

                List<int> Pointers = new List<int>();

                Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(this.Stream);

                while (!(SearchPos >= Stream.Length - Search.Length - 1))
                {
                    br.BaseStream.Seek(SearchPos, SeekOrigin.Begin);
                    ReadBytes = br.ReadBytes(window);
                    check = FindBytes(ReadBytes, Search);

                    while (check != -1)
                    {
                        Offset = SearchPos + check;
                        Pointers.Add(Offset);

                        check = FindBytes(ReadBytes, Search, check + Search.Length);

                    }
                    SearchPos += window - Search.Length;
                }

                return Pointers;

            }
        }
    }
}
