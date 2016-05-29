using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE_Framework.Data
{
    public enum CheckLz77Type
    {
        Sprite,
        Palette
    }

    public static class Lz77
    {
        
        public static byte[] DecompressBytes(byte[] Data)
        {
            byte[] data;
            if (Data[0] == 0x10)
            {
                int DataLength = BitConverter.ToInt32(new Byte[] { Data[1], Data[2], Data[3], 0x0 }, 0);
                data = new Byte[DataLength];

                int Offset = 4;

                string watch = "";
                int i = 0;
                byte pos = 8;

                while (i < DataLength)
                {
                    //br.BaseStream.Seek(Offset, SeekOrigin.Begin);
                    if (pos != 8)
                    {
                        if (watch[pos] == "0"[0])
                        {
                            //data[i] = br.ReadByte();
                            data[i] = Data[Offset];
                        }
                        else
                        {
                            //byte[] r = br.ReadBytes(2);
                            byte[] r = new byte[]{Data[Offset] , Data[Offset + 1]};
                            int length = r[0] >> 4;
                            int start = ((r[0] - ((r[0] >> 4) << 4)) << 8) + r[1];
                            AmmendArray(ref data, ref i, i - start - 1, length + 3);
                            Offset++;
                        }
                        Offset++;
                        i++;
                        pos++;

                    }
                    else
                    {
                        //watch = Convert.ToString(br.ReadByte(), 2);
                        watch = Convert.ToString(Data[Offset], 2);
                        while (watch.Length != 8)
                        {
                            watch = "0" + watch;
                        }
                        Offset++;
                        pos = 0;
                    }
                }
                //br.Close();


                return data;
            }
            else
            {
                throw new Exception("This data is not Lz77 compressed!");
            }
        }

        static void AmmendArray(ref byte[] Bytes, ref int Index, int Start, int Length)
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

            Index += Length - 1;
        }


        // For picking what type of Compression Look-up we want
        public enum CompressionMode
        {
            Old, // Good
            New  // Perfect!
        }

        public static byte[] CompressBytes(byte[] Data, CompressionMode Mode = CompressionMode.New)
        {
            byte[] header = BitConverter.GetBytes(Data.Length);
            List<byte> Bytes = new List<byte>();
            List<byte> PreBytes = new List<byte>();
            byte Watch = 0;
            byte ShortPosition = 2;
            int ActualPosition = 2;
            int match = -1;

            int BestLength = 0;

            // Adds the Lz77 header to the bytes 0x10 3 bytes size reversed
            Bytes.Add(0x10);
            Bytes.Add(header[0]);
            Bytes.Add(header[1]);
            Bytes.Add(header[2]);

            // Lz77 Compression requires SOME starting data, so we provide the first 2 bytes
            PreBytes.Add(Data[0]);
            PreBytes.Add(Data[1]);

            // Compress everything
            while (ActualPosition < Data.Length)
            {
                //If we've compressed 8 of 8 bytes
                if (ShortPosition == 8)
                {
                    // Add the Watch Mask
                    // Add the 8 steps in PreBytes
                    Bytes.Add(Watch);
                    Bytes.AddRange(PreBytes);

                    Watch = 0;
                    PreBytes.Clear();

                    // Back to 0 of 8 compressed bytes
                    ShortPosition = 0;
                }
                else
                {
                    // If we are approaching the end
                    if (ActualPosition + 1 < Data.Length)
                    {
                        // Old NSE 1.x compression lookup
                        if (Mode == CompressionMode.Old)
                        {
                            match = SearchBytesOld(
                                Data,
                                ActualPosition,
                                Math.Min(4096, ActualPosition));
                        }
                        else if (Mode == CompressionMode.New)
                        {
                            // New NSE 2.x compression lookup
                            match = SearchBytes(
                                        Data,
                                        ActualPosition,
                                        Math.Min(4096, ActualPosition), out BestLength);
                        }
                    }
                    else
                    {
                        match = -1;
                    }

                    // If we have NOT found a match in the compression lookup
                    if (match == -1)
                    {
                        // Add the byte
                        PreBytes.Add(Data[ActualPosition]);
                        // Add a 0 to the mask
                        Watch = BitConverter.GetBytes((int)Watch << 1)[0];

                        ActualPosition++;
                    }
                    else
                    {
                        // How many bytes match
                        int length = -1;

                        int start = match;
                        if (Mode == CompressionMode.Old || BestLength == -1)
                        {
                            // Old look-up technique
                            # region GetLength_Old
                            start = match;

                            bool Compatible = true;

                            while (Compatible  && length < 18 && length + ActualPosition < Data.Length - 1)
                            {
                                length++;
                                if (Data[ActualPosition + length] != Data[ActualPosition - start + length])
                                {
                                    Compatible = false;
                                }
                            }
                            #endregion
                        }
                        else if (Mode == CompressionMode.New)
                        {
                            // New lookup (Perfect Compression!)
                            length = BestLength;
                        }

                        // Add the rel-compression pointer (P) and length of bytes to copy (L)
                        // Format: L P P P
                        byte[] b = BitConverter.GetBytes(((length - 3) << 12) + (start - 1));

                        b = new byte[] { b[1], b[0] };
                        PreBytes.AddRange(b);

                        // Move to the next position
                        ActualPosition += length;

                        // Add a 1 to the bit Mask
                        Watch = BitConverter.GetBytes(((int)Watch << 1) + 1)[0];
                    }

                    // We've just compressed 1 more 8
                    ShortPosition++;
                }


            }

            // Finnish off the compression
            if (ShortPosition != 0)
            {
                //Tyeing up any left-over data compression
                Watch = BitConverter.GetBytes((int)Watch << (8 - ShortPosition))[0];

                Bytes.Add(Watch);
                Bytes.AddRange(PreBytes);
            }

            // Return the Compressed bytes as an array!
            return Bytes.ToArray();
        }

        static int SearchBytesOld(byte[] Data, int Index, int Length)
        {
            int found = -1;
            int pos = 2;

            if (Index + 2 < Data.Length)
            {
                while (pos < Length + 1 && found == -1)
                {
                    if (Data[Index - pos] == Data[Index] && Data[Index - pos + 1] == Data[Index + 1])
                    {

                        if (Index > 2)
                        {
                            if (Data[Index - pos + 2] == Data[Index + 2])
                            {
                                found = pos;
                            }
                            else
                            {
                                pos++;
                            }
                        }
                        else
                        {
                            found = pos;
                        }


                    }
                    else
                    {
                        pos++;
                    }
                }

                return found;
            }
            else
            {
                return -1;
            }

        }

        static int SearchBytes(byte[] Data, int Index, int Length, out int match)
        {

            int pos = 2;
            match = 0;
            int found = -1;

            if (Index + 2 < Data.Length)
            {
                while (pos < Length + 1 && match != 18)
                {
                    if (Data[Index - pos] == Data[Index] && Data[Index - pos + 1] == Data[Index + 1])
                    {

                        if (Index > 2)
                        {
                            if (Data[Index - pos + 2] == Data[Index + 2])
                            {
                                int _match = 2;
                                bool Compatible = true;
                                while (Compatible  && _match < 18 && _match + Index < Data.Length - 1)
                                {
                                    _match++;
                                    if (Data[Index + _match] != Data[Index - pos + _match])
                                    {
                                        Compatible = false;
                                    }
                                }
                                if (_match > match)
                                {
                                    match = _match;
                                    found = pos;
                                }

                            }
                            pos++;
                        }
                        else
                        {
                            found = pos;
                            match = -1;
                            pos++;
                        }


                    }
                    else
                    {
                        pos++;
                    }
                }

                return found;
            }
            else
            {
                return -1;
            }

        }

        

        public static int CheckLz77(Read read, int Offset, CheckLz77Type Type)
        {
            return CheckLz77(read.ReadBytes(Offset, 5), Type);
        }

        public static int CheckLz77(byte[] Header5Bytes, CheckLz77Type Type)
        {
            if (Header5Bytes[0] == 0x10)
            {
                int length = BitConverter.ToInt32(new Byte[] { Header5Bytes[1], Header5Bytes[2], Header5Bytes[3], 0x0 }, 0);

                if (Type == CheckLz77Type.Sprite && Header5Bytes[4] <= 63 && length >= 64 && length % 8 == 0)
                {
                    return length;
                }
                else if (Type == CheckLz77Type.Palette && length == 0x20 && Header5Bytes[4] <= 63)
                {
                    return length;
                }
                else
                {
                    return -1;
                }


            }
            else
            {
                return -1;
            }
        }
    }
}
