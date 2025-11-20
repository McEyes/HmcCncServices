using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc
{
    
    /// <summary>
    /// 
    /// </summary>
    public enum NetDataType
    {
        /// <summary>
        /// 
        /// </summary>
        Bit = 0,
        /// <summary>
        /// 
        /// </summary>
        Byte = 1,
        /// <summary>
        /// 
        /// </summary>
        Short = 2,
        /// <summary>
        /// 
        /// </summary>
        Ushort = 3,
        /// <summary>
        /// 
        /// </summary>
        Int = 4,
        /// <summary>
        /// 
        /// </summary>
        Uint = 5,
        /// <summary>
        /// 
        /// </summary>
        Long = 6,
        /// <summary>
        /// 
        /// </summary>
        Ulong = 7,
        /// <summary>
        /// 
        /// </summary>
        Float = 8,
        /// <summary>
        /// 
        /// </summary>
        Double = 9,
        /// <summary>
        /// 
        /// </summary>
        String = 10,
        /// <summary>
        /// 
        /// </summary>
        StringAscii = 11,
        /// <summary>
        /// 
        /// </summary>
        StringUtf8 = 12,
        /// <summary>
        /// 
        /// </summary>
        StringUnicode = 13,
        /// <summary>
        /// 
        /// </summary>
        StringUnicodeBig = 14,
        /// <summary>
        /// 
        /// </summary>
        StringGB2312 = 15,
        /// <summary>
        /// 
        /// </summary>
        StringANSI = 16,
        /// <summary>
        /// 
        /// </summary>
        StringUTF32 = 17,
       
    }

    
    
    /// <summary>
    /// 
    /// </summary>
    public enum PlcWindows
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("R424")]
        Win1 = 424,
        
        /// <summary>
        /// 
        /// </summary>
        [Description("R428")]
 Win2 = 428,
        /// <summary>
        /// 
        /// </summary>
        [Description("R432")]
      Win3 = 432


       
    }
    /// <summary>
    /// 
    /// </summary>
    public sealed class PlcWindowsCmd
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("R424")]
       public const int Win1 = 424;
       public const int  Win1ReadStartAddress = 9300;
    //    public string Win1ReadRandomCmd = "2:1:R424 9300,R425 6,R9300 1,R9301 63,R9302 1,R9303 4,R9304 1,R9305 0,R9306 1";
        /// <summary>
        /// 
        /// </summary>
        [Description("R428")]
       public const int  Win2 = 428;
       public const int  Win2ReadStartAddress = 19300;
       public const string Win2ReadRandomCmd = "2:1:R428 19300,R429 4,R19301 40,R19302 0,R19303 2,R19304 0,R19305 1,R19306 4,R19300 1";
        /// <summary>
        /// 
        /// </summary>
        [Description("R432")]
       public const int  Win3 = 432;
       public const int  Win3ReadStartAddress = 29000;
       public const string Win3ReadRandomCmd = "2:1:R432 29000,R433 4,R29001 46,R29002 1,R29003 101,R29004 0,R29005 1,R29006 4,R29000 1";


       
    }
}