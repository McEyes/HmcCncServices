using System;
using System.Linq;
using System.Text;

namespace EZSocketNc.Extensions
{
    public static class ByteExtension
    {
        public static float[] HexByteToFloats(this byte[] hexBytes, bool reverse = false)
        {
            if (hexBytes == null) return new float[1] { 0 };
            if (reverse) hexBytes = hexBytes.Reverse().ToArray();
            int len = hexBytes.Length / 4;
            float[] intValues = new float[len];
            for (int i = 0; i < len; i++)
            {
                intValues[i] = BitConverter.ToSingle(hexBytes, i * 4);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
            }
            return intValues;
        }
        public static float HexByteToFloat(this byte[] hexBytes, bool reverse = false)
        {
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(hexBytes);
            return BitConverter.ToSingle(hexBytes, 0);
        }

        public static float HexByteToFloat(this byte hexBytes)
        {
            return BitConverter.ToSingle(new byte[] { hexBytes, 0x00 }, 0);
        }

        public static ushort[] HexByteToUInt16s(this byte[] hexBytes, bool reverse = false)
        {
            if (hexBytes == null) return new ushort[1] { 0 };
            if (reverse) hexBytes = hexBytes.Reverse().ToArray();
            int len = hexBytes.Length / 2;
            ushort[] intValues = new ushort[len];
            for (int i = 0; i < len; i++)
            {
                intValues[i] = BitConverter.ToUInt16(hexBytes, i * 2);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
            }
            return intValues;
        }

        public static ushort HexByteToUInt16(this byte[] hexBytes, bool reverse = false)
        {
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(hexBytes);
            return BitConverter.ToUInt16(hexBytes, 0);
        }
        public static ushort HexByteToUInt16(this byte hexBytes)
        {
            return BitConverter.ToUInt16(new byte[] { hexBytes, 0x00 }, 0);
        }

        public static short[] HexByteToInt16s(this byte[] hexBytes, bool reverse = false)
        {
            if (hexBytes == null) return new short[1] { 0 };
            if (reverse) hexBytes = hexBytes.Reverse().ToArray();
            int len = hexBytes.Length / 2;
            short[] intValues = new short[len];
            for (int i = 0; i < len; i++)
            {
                intValues[i] = BitConverter.ToInt16(hexBytes, i * 2);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
            }
            return intValues;
        }

        public static short HexByteToInt16(this byte[] hexBytes, bool reverse = false)
        {
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(hexBytes);
            if (hexBytes.Length == 1) return hexBytes[0].HexByteToInt16();
            return BitConverter.ToInt16(hexBytes, 0);
        }

        public static short HexByteToInt16(this byte hexBytes)
        {
            return BitConverter.ToInt16(new byte[] { hexBytes, 0x00 }, 0);
        }


        public static int HexByteToInt32(this byte[] hexBytes, bool reverse = false)
        {
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(hexBytes);
            return BitConverter.ToInt32(hexBytes, 0);
        }
        public static int HexByteToInt32(this byte hexBytes)
        {
            return BitConverter.ToInt32(new byte[] { hexBytes, 0x00, 0x00, 0x00 }, 0);
        }
        public static int[] HexByteToInt32s(this byte[] hexBytes, bool reverse = false)
        {
            if (hexBytes == null) return new int[1] { 0 };
            if (reverse) hexBytes = hexBytes.Reverse().ToArray();
            int len = hexBytes.Length / 4;
            int[] intValues = new int[len];
            for (int i = 0; i < len; i++)
            {
                intValues[i] = BitConverter.ToInt32(hexBytes, i * 4);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
            }
            return intValues;
        }

        public static uint HexByteToUInt32(this byte[] hexBytes, bool reverse = false)
        {
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(hexBytes);
            if (hexBytes.Length > 3) return BitConverter.ToUInt32(hexBytes, 0);
            else if (hexBytes.Length > 0)
            {
                var len = 4 - hexBytes.Length;
                var list = hexBytes.ToList();
                while (len > 0)
                {
                    list.Add(0x00);
                    len--;
                }
                return BitConverter.ToUInt32(list.ToArray(), 0);
            }
            else return 0;
        }
        public static UInt32 HexByteToUInt32(this byte hexBytes)
        {
            return BitConverter.ToUInt32(new byte[] { hexBytes, 0x00, 0x00, 0x00 }, 0);
        }
        public static UInt32[] HexByteToUInt32s(this byte[] hexBytes, bool reverse = false)
        {
            if (hexBytes == null) return new UInt32[1] { 0 };
            if (reverse) hexBytes = hexBytes.Reverse().ToArray();
            int len = hexBytes.Length / 4;
            UInt32[] intValues = new UInt32[len];
            for (int i = 0; i < len; i++)
            {
                intValues[i] = BitConverter.ToUInt32(hexBytes, i * 4);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
            }
            return intValues;
        }



        public static long HexByteToInt64(this byte[] hexBytes, bool reverse = false)
        {
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(hexBytes);
            return BitConverter.ToInt64(hexBytes, 0);
        }
        public static Int64 HexByteToInt64(this byte hexBytes)
        {
            return BitConverter.ToInt64(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, hexBytes }, 0);
        }

        public static Int64[] HexByteToInt64s(this byte[] hexBytes, bool reverse = false)
        {
            if (hexBytes == null) return new Int64[1] { 0 };
            if (reverse) hexBytes = hexBytes.Reverse().ToArray();
            int len = hexBytes.Length / 8;
            Int64[] intValues = new Int64[len];
            for (int i = 0; i < len; i++)
            {
                intValues[i] = BitConverter.ToInt64(hexBytes, i * 8);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
            }
            return intValues;
        }

        public static ulong HexByteToUInt64(this byte[] hexBytes, bool reverse = false)
        {
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(hexBytes);
            return BitConverter.ToUInt64(hexBytes, 0);
        }

        public static UInt64 HexByteToUInt64(this byte hexBytes)
        {
            return BitConverter.ToUInt64(new byte[] { 0x00, 0x00, 0x00, hexBytes }, 0);
        }

        public static UInt64[] HexByteToUInt64s(this byte[] hexBytes, bool reverse = false)
        {
            if (hexBytes == null) return new UInt64[1] { 0 };
            if (reverse) hexBytes = hexBytes.Reverse().ToArray();
            int len = hexBytes.Length / 4;
            UInt64[] intValues = new UInt64[len];
            for (int i = 0; i < len; i++)
            {
                intValues[i] = BitConverter.ToUInt64(hexBytes, i * 4);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
            }
            return intValues;
        }


        //public static Boolean[] HexByteToBooleans(this byte[] hexBytes, bool reverse = false)
        //{
        //    if (hexBytes == null) return new Boolean[1] { false };
        //    if (reverse) hexBytes = hexBytes.Reverse().ToArray();
        //    int len = hexBytes.Length * 2;
        //    Boolean[] intValues = new Boolean[len];

        //    var data = hexBytes.ToHexString().ToCharArray();
        //    for (int i = 0; i < len; i++)
        //    {
        //        intValues[i] = data[i] == '1';
        //        // intValues[i] = BitConverter.ToBoolean(hexBytes, i);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
        //    }
        //    return intValues;
        //}
        public static bool HexByteToBoolean(this byte hexBytes)
        {
            return BitConverter.ToBoolean(new byte[] { hexBytes }, 0);
        }


        public static Double[] HexByteToDoubles(this byte[] hexBytes, bool reverse = false)
        {
            if (reverse) hexBytes = hexBytes.Reverse().ToArray();
            int len = hexBytes.Length / 8;
            Double[] intValues = new Double[len];
            for (int i = 0; i < len; i++)
            {
                intValues[i] = BitConverter.ToDouble(hexBytes, i * 8);// Convert.ToInt32(hexBytes[i].ToString("X2"), 32);
            }
            return intValues;
        }
        public static Double HexByteToDouble(this byte hexBytes)
        {
            return BitConverter.ToDouble(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, hexBytes }, 0);
        }

        /// <summary>
        /// 转化为二进制
        /// </summary>
        /// <param name="hexBytes"></param>
        /// <returns></returns>
        public static string HexByteToBit(this byte[] byteArray, int bit = 8)
        {
            return string.Join(" ", byteArray.Select(b => Convert.ToString(b, 2).PadLeft(bit, '0')));
        }

        /// <summary>
        /// 转化为二进制
        /// </summary>
        /// <param name="hexBytes"></param>
        /// <returns></returns>
        public static string[] HexByteToBits(this byte[] byteArray, int bit = 8)
        {
            if (byteArray == null) return null;
            return byteArray.Select(b => Convert.ToString(b, 2).PadLeft(bit, '0')).ToArray();
        }

        /// <summary>
        /// byte数组转string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string byteArrayToString(byte[] data)
        {


            return Encoding.Default.GetString(data);
        }



        /// <summary>
        /// string转 byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] stringToByteArray(string data)
        {

            return Encoding.Default.GetBytes(data);
        }





        /// <summary>
        /// byte数组转16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string byteArrayToHexString(byte[] data)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(string.Format("{0:X2} ", data[i]));
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 16进制字符串转byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] hexStringToByteArray(string data)
        {
            string[] chars = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 16);
            }
            return returnBytes;
        }



        /// <summary>
        /// byte数组转10进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string byteArrayToDecString(byte[] data)
        {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i] + " ");
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 10进制字符串转byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] decStringToByteArray(string data)
        {

            string[] chars = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为10进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 10);
            }
            return returnBytes;
        }




        /// <summary>
        /// byte数组转八进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string byteArrayToOtcString(byte[] data)
        {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(Convert.ToString(data[i], 8) + " ");
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 八进制字符串转byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] otcStringToByteArray(string data)
        {

            string[] chars = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为8进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 8);
            }
            return returnBytes;
        }





        /// <summary>
        /// 二进制字符串转byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] binStringToByteArray(string data)
        {

            string[] chars = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为2进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 2);
            }
            return returnBytes;
        }



        /// <summary>
        /// byte数组转二进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string byteArrayToBinString(byte[] data)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(Convert.ToString(data[i], 2) + " ");
            }
            return builder.ToString().Trim();
        }


    }
}
