using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE_Framework.IO
{
    public class NBM_Decoder
    {
        public NBM_Decoder(ref Read Read)
        {
            this.Read = Read;
        }

        public Read Read;

        private  BookMark bookmark;

        public List<byte[]> Memory { get; set; }
        public int index;
        public bool nxt;
        public int CommandIndex { get { return index; } set { index = value; nxt = false; } }
        public List<byte[]> Commands { get { return bookmark.Commands; } set { bookmark.Commands = value; } }

        //Conditional
        int depth;
        byte[] b;

        //Math
        int x;
        int y;


        //public  void Return(int value, BookMarkCommands.ReturnType type)
        //{
        //    if (bookmark != null)
        //    {
        //        if (type == BookMarkCommands.ReturnType.ImageOffset)
        //        {
        //            bookmark.ImageOffset = value;
        //        }
        //        else if (type == BookMarkCommands.ReturnType.PaletteOffset)
        //        {
        //            bookmark.PaletteOffset = value;
        //        }
        //        else if (type == BookMarkCommands.ReturnType.Width)
        //        {
        //            bookmark.Width = value;
        //        }
        //        else if (type == BookMarkCommands.ReturnType.Height)
        //        {
        //            bookmark.Height = value;
        //        }

        //        Memory.Clear();
        //    }
        //}

        public void Decode(ref BookMark bm)
        {
            if (bm.Commands != null)
            {
                if (bm.Commands.Count != 0)
                {
                    Memory = new List<byte[]>();
                    depth = 0;
                    bookmark = bm;
                    index = 0;
                    nxt = true;

                        while (index < bm.Commands.Count)
                        {

                            if (bm.Commands[index][0] == 0x01)
                            {
                                #region 0x01 Memory

                                if (bm.Commands[index][1] == 0x01)
                                {
                                    //Append
                                    //Format -,-,m1?,v->                                 

                                    if (bm.Commands[index][2] == 0x01)
                                    {
                                        y = BitConverter.ToInt32(bm.Commands[index], 3);
                                        Memory.Add(Memory[BitConverter.ToInt32(bm.Commands[index],3)]);
                                    }
                                    else
                                    {
                                        Memory.Add(bm.Commands[index].SubArray(3, bm.Commands[index].Length - 3));
                                    }

                                    
                                }
                                else if (bm.Commands[index][1] == 0x02)
                                {
                                    Memory[ BitConverter.ToInt32( bm.Commands[index], 2) ] = new byte[1];
                                }
                                else if (bm.Commands[index][1] == 0x04)
                                {
                                    //Pointer formatter
                                    //Format -,-,m1?,i,i,i,i    

                                    b = bm.Commands[index].SubArray(3,4);
                                    if (bm.Commands[index][2] == 0x01)
                                    {
                                        b = Memory[BitConverter.ToInt32(b,0)];
                                    }

                                    if (b[3] >= 8)
                                    {
                                        b[3] = (byte)(b[3] - 8);
                                    }
                                    


                                }

                                #endregion
                            }
                            else if (bm.Commands[index][0] == 0x02)
                            {
                                #region 0x02 End Conditional "}"
                                //Format -

                                depth -= 1;
                                if (depth < 0)
                                {
                                    depth = 0;
                                }

                                #endregion
                            }
                            else if (bm.Commands[index][0] == 0x03)
                            {
                                #region 0x03 if

                                //Format -,-,m1?,m2?,i,i,i,i,v->

                                x = BitConverter.ToInt32(bm.Commands[index], 4);
                                if (bm.Commands[index][2] == 0x01)
                                {
                                    x = BitConverter.ToInt32(Memory[x], 0);
                                }

                                b =  bm.Commands[index].SubArray(8,bm.Commands[index].Length - 8);
                                if (bm.Commands[index][3] == 0x01)
                                {
                                    y = BitConverter.ToInt32(b, 0);
                                    y = BitConverter.ToInt32(Memory[y], 0);
                                    b = BitConverter.GetBytes(y);
                                }

                                if (bm.Commands[index][1] == 0x01)
                                {
                                    // True
                                    #region true
                                    if (Memory[x].Length != b.Length)
                                    {
                                        int dpth = depth + 1;
                                        index++;
                                        while (dpth != depth && index < bm.Commands.Count)
                                        {
                                            if (bm.Commands[index][0] == 0x03)
                                            {
                                                dpth++;
                                            }
                                            else if (bm.Commands[index][0] == 0x02)
                                            {
                                                dpth--;
                                            }
                                            index++;
                                        }
                                        nxt = false;
                                    }
                                    else if (!Memory[x].SequenceEqual<byte>(b))
                                    {
                                        int dpth = depth + 1;
                                        index++;
                                        while (dpth != depth && index < bm.Commands.Count)
                                        {
                                            if (bm.Commands[index][0] == 0x03)
                                            {
                                                dpth++;
                                            }
                                            else if (bm.Commands[index][0] == 0x02)
                                            {
                                                dpth--;
                                            }
                                            index++;
                                        }
                                        nxt = false;
                                    }
                                    else
                                    {
                                        depth++;
                                    }
                                    #endregion
                                }
                                else if (bm.Commands[index][1] == 0x02)
                                {
                                    // False
                                    #region false
                                    if (Memory[x].SequenceEqual<byte>(b))
                                    {
                                        while ((bm.Commands[index][0] != 0x04 || bm.Commands[index][0] != 0x05 || bm.Commands[index][0] != 0x02) && index < bm.Commands.Count - 1)
                                        {
                                            index++;
                                        }
                                        nxt = false;
                                    }
                                    else
                                    {
                                        depth++;
                                    }
                                    #endregion
                                }
                                else if (bm.Commands[index][1] == 0x03)
                                {
                                    // LessThan
                                    #region lessThan
                                    if (BitConverter.ToInt32(Memory[x], 0) >= BitConverter.ToInt32(b, 0))
                                    {
                                        while ((bm.Commands[index][0] != 0x04 || bm.Commands[index][0] != 0x05 || bm.Commands[index][0] != 0x02) && index < bm.Commands.Count - 1)
                                        {
                                            index++;
                                        }
                                        nxt = false;
                                    }
                                    else
                                    {
                                        depth++;
                                    }
                                    #endregion
                                }
                                else if (bm.Commands[index][1] == 0x04)
                                {
                                    // GreaterThan
                                    #region greaterThan
                                    if (BitConverter.ToInt32(Memory[x], 0) <= BitConverter.ToInt32(b, 0))
                                    {
                                        while ((bm.Commands[index][0] != 0x04 || bm.Commands[index][0] != 0x05 || bm.Commands[index][0] != 0x02) && index < bm.Commands.Count - 1)
                                        {
                                            index++;
                                        }
                                        nxt = false;
                                    }
                                    else
                                    {
                                        depth++;
                                    }
                                    #endregion
                                }
                                else if (bm.Commands[index][1] == 0x03)
                                {
                                    // LessThanOrEqual
                                    #region lessThanOrEqual
                                    if (BitConverter.ToInt32(Memory[x], 0) > BitConverter.ToInt32(b, 0))
                                    {
                                        while ((bm.Commands[index][0] != 0x04 || bm.Commands[index][0] != 0x05 || bm.Commands[index][0] != 0x02) && index < bm.Commands.Count - 1)
                                        {
                                            index++;
                                        }
                                        nxt = false;
                                    }
                                    else
                                    {
                                        depth++;
                                    }
                                    #endregion
                                }
                                else if (bm.Commands[index][1] == 0x03)
                                {
                                    // GreaterThanOrEqual
                                    #region greaterThanOrEqual
                                    if (BitConverter.ToInt32(Memory[x], 0) < BitConverter.ToInt32(b, 0))
                                    {
                                        while ((bm.Commands[index][0] != 0x04 || bm.Commands[index][0] != 0x05 || bm.Commands[index][0] != 0x02) && index < bm.Commands.Count - 1)
                                        {
                                            index++;
                                        }
                                        nxt = false;
                                    }
                                    else
                                    {
                                        depth++;
                                    }
                                    #endregion
                                }



                                #endregion
                            }
                            else if (bm.Commands[index][0] == 0x04)
                            {

                            }
                            else if (bm.Commands[index][0] == 0x08)
                            {
                                #region 0x08 Math

                                //Format -,-,m1?,m2?,i,i,i,i,v,v,v,v

                                x = BitConverter.ToInt32(bm.Commands[index], 4);
                                if (bm.Commands[index][2] == 0x01)
                                {
                                    x = BitConverter.ToInt32(Memory[x], 0);
                                }

                                y = BitConverter.ToInt32(bm.Commands[index], 8);
                                if (bm.Commands[index][3] == 0x01)
                                {
                                    y = BitConverter.ToInt32(Memory[y], 0);
                                }

                                if (bm.Commands[index][1] == 0x01)
                                {
                                    //Add 0x01
                                    Memory[x] = BitConverter.GetBytes((BitConverter.ToInt32(Memory[x], 0) + y));
                                }
                                else if (bm.Commands[index][1] == 0x02)
                                {
                                    //Subtract 0x02
                                    Memory[x] = BitConverter.GetBytes((BitConverter.ToInt32(Memory[x], 0) - y));
                                }
                                else if (bm.Commands[index][1] == 0x03)
                                {
                                    //Multiply 0x03
                                    Memory[x] = BitConverter.GetBytes((BitConverter.ToInt32(Memory[x], 0) * y));
                                }
                                else if (bm.Commands[index][1] == 0x04)
                                {
                                    //Divide 0x03
                                    Memory[x] = BitConverter.GetBytes((BitConverter.ToInt32(Memory[x], 0) / y));
                                }
                                else
                                {
                                    throw new Exception("Error: 0x" + bm.Commands[index][1].ToString("X2") + " is not a valid math operation.");
                                }

                                #endregion
                            }
                            else if (bm.Commands[index][0] == 0x09)
                            {
                                #region 0x09 Read

                                //Format -,-,m1?,m2?,o,o,o,o,l,l,l,l
                                if (bm.Commands[index][1] == 0x01)
                                {
                                    x = BitConverter.ToInt32(bm.Commands[index], 4);
                                    if (bm.Commands[index][2] == 0x01)
                                    {
                                        x = BitConverter.ToInt32(Memory[x], 0);
                                    }

                                    y = BitConverter.ToInt32(bm.Commands[index], 8);
                                    if (bm.Commands[index][3] == 0x01)
                                    {
                                        y = BitConverter.ToInt32(Memory[y], 0);
                                    }

                                    Memory.Add(Read.ReadBytes(x, y));

                                }


                                #endregion

                            }
                            else if (bm.Commands[index][0] == 0x0A)
                            {
                                #region 0x0A Command

                                //Format -,-,m1?,v,v,v,v
                                x = BitConverter.ToInt32(bm.Commands[index], 3);
                                if (bm.Commands[index][2] == 0x01)
                                {
                                    x = BitConverter.ToInt32(Memory[x], 0);
                                }

                                if (bm.Commands[index][1] == 0x01)
                                {
                                    //Image Offset
                                    bm.ImageOffset = x;
                                }
                                else if (bm.Commands[index][1] == 0x02)
                                {
                                    //Palette Offset
                                    bm.PaletteOffset = x;
                                }
                                else if (bm.Commands[index][1] == 0x03)
                                {
                                    //Width
                                    bm.Width = x;
                                }
                                else if (bm.Commands[index][1] == 0x04)
                                {
                                    //Height
                                    bm.Height = x;
                                }
                                else if (bm.Commands[index][1] == 0x10)
                                {
                                    //Stop Execution
                                    index = bm.Commands.Count;
                                }

                                #endregion
                            }
                                                       
                            
                            if (nxt )
                            {
                                index++;
                            }
                            else
                            {
                                nxt = true;
                            }
                        }
                    
                }
            }
        }

    }
}
