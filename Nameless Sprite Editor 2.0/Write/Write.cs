using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NSE2
{
    class Write
    {
        public Write(string FilePath)
        {
            // Set this Write's Filepath
            this.FilePath = FilePath;
        }

        public string FilePath;
        FileStream Stream;

        public void WriteBytes(Byte[] WriteBytes, int Offset)
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

        // For picking what type of Compression Look-up we want
        public enum CompressionMode
        {
            Old, // Good
            New  // Perfect!
        }

        public byte[] CompressLzBytes(byte[] Data, CompressionMode Mode = CompressionMode.New)
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
            Bytes.Add(header[2]);
            Bytes.Add(header[1]);
            Bytes.Add(header[0]);

            // Lz77 Compression requires SOME starting data, so we provide the first 2 bytes
            PreBytes.Add(Data[0]);
            PreBytes.Add(Data[1]);

            // Compress everything
            while (ActualPosition  < Data.Length)
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
                        else if(Mode == CompressionMode.New)
                        {
                            // New NSE 2.x compression lookup
                            match = SearchBytes(
                                        Data,
                                        ActualPosition,
                                        Math.Min(4096, ActualPosition),out BestLength);
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

                            while (Compatible == true && length < 18 && length + ActualPosition < Data.Length - 1)
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
                        byte[] b = BitConverter.GetBytes(((length - 3) << 12) + (start - 1) );

                        b = new byte[] { b[1], b[0] };
                        PreBytes.AddRange(b);

                        // Move to the next position
                        ActualPosition += length;
                        
                        // Add a 1 to the bit Mask
                        Watch = BitConverter.GetBytes( ((int)Watch << 1) + 1)[0];
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

        int SearchBytesOld(byte[] Data, int Index, int Length)
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

        int SearchBytes(byte[] Data, int Index, int Length, out int match)
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
                                while (Compatible == true && _match < 18 && _match + Index < Data.Length - 1)
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
    }
}
