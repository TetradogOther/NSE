using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NSE_Framework
{
    public class Read
    {
        long filelength = 0;
        public long FileLength
        {
            get { return filelength; }
        }

        public Read(String FilePath)
        {
            this.FilePath = FilePath;

            Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            filelength = Stream.Length;
            Stream.Close();
        }

        public string FilePath;
        FileStream Stream;

        public byte ReadByte(int Offset)
        {
            Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(this.Stream);
            br.BaseStream.Seek(Offset, SeekOrigin.Begin);
            byte rb = br.ReadByte();
            br.Close();
            Stream.Close();
            return rb;
        }

        public byte[] ReadBytes(int Offset, int Length)
        {
            Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(this.Stream);
            br.BaseStream.Seek(Offset, SeekOrigin.Begin);
            Byte[] rb = br.ReadBytes(Length);
            br.Close();
            Stream.Close();
            return rb;
        }

        public string ReadString(int Offset, int Length)
        {
            string rs = "";
            Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(this.Stream);
            br.BaseStream.Seek(Offset, SeekOrigin.Begin);
            Byte[] rb = br.ReadBytes(Length);
            Stream.Close();
            br.Close();

            foreach (byte b in rb)
            {
                rs += (char)b;
            }

            return rs;
        }

        public byte[] ReadLz77Bytes(int Offset, out int DataLength)
        {
            int StartOffset = Offset;
            Stream = System.IO.File.Open(FilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(this.Stream);
            br.BaseStream.Seek(Offset, SeekOrigin.Begin);
            Byte[] data = br.ReadBytes(4);

            if (data[0] == 0x10)
            {
                DataLength = BitConverter.ToInt32(new Byte[] { data[1], data[2], data[3], 0x0 }, 0); 
                data = new Byte[DataLength];

                Offset += 4;

                string watch = "";
                int i = 0;
                byte pos = 8;

                while (i < DataLength)
                {
                    br.BaseStream.Seek(Offset, SeekOrigin.Begin);
                    if (pos != 8)
                    {
                        if (watch[pos] == "0"[0])
                        {
                            data[i] = br.ReadByte();
                        }
                        else
                        {
                            byte[] r = br.ReadBytes(2);
                            int length = r[0] >> 4;
                            int start = ((r[0] - ((r[0] >> 4) << 4)) << 8) + r[1];
                            AmmendArray(ref data, ref i, i - start - 1, length + 3);
                            Offset++;
                        }
                        Offset ++;
                        i++;
                        pos++;

                    }
                    else
                    {
                        watch = Convert.ToString(br.ReadByte(), 2);
                        while (watch.Length != 8)
                        {
                            watch = "0" + watch;
                        }
                        Offset++;
                        pos = 0;
                    }
                }
                DataLength = Offset - StartOffset;
                br.Close();
                Stream.Close();

                return data;
            }
            else
            {
                throw new Exception("This data is not Lz77 compressed!");
            }
        }

        public int ReadPointer(int Offset)
        {
            return BitConverter.ToInt32(ReadBytes(Offset, 4), 0) - 0x8000000;
        }

        void AmmendArray(ref byte[] Bytes, ref int Index, int Start, int Length)
        {
            int a = 0; // Act
            int r = 0; // Rel

            byte Backup = 0;

            if (Index > 0)
            {
                Backup = Bytes[Index - 1];
            }

            while (a != Length)
            {
                if (Index + r >= 0 && Start + r >= 0 && Index + a < Bytes.Length)
                {
                    if (Start + r >= Index)
                    {
                        r = 0;
                        Bytes[Index + a] = Bytes[Start + r];
                    }
                    else
                    {
                        Bytes[Index + a] = Bytes[Start + r];
                        Backup = Bytes[Index + r];
                    }
                }
                a++;
                r++;
            }

            Index += Length -1 ;
        }

    }
}

