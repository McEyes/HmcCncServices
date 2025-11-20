using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EZSocketNc.Commons
{
	/// <summary>
	/// 扩展的辅助类方法
	/// </summary>
	// Token: 0x02000019 RID: 25
	public static class HslExtension
	{
		
		public static string ToAsciiString(this byte[] InBytes)
		{
			return Encoding.ASCII.GetString(InBytes);
			// return SoftBasic.GetAsciiStringRender(InBytes);
		}
		// public static string ToAsciiString(this byte[] InBytes)
		// {
		// 	return Encoding.ASCII.GetString(InBytes);
		// }

		public static string ToUtf8String(this byte[] InBytes)
		{
			return Encoding.UTF8.GetString(InBytes);
		}
		public static string ToUnicodeString(this byte[] InBytes)
		{
			return Encoding.Unicode.GetString(InBytes);
		}
		public static string ToHexString(this byte[] InBytes)
		{
			return SoftBasic.ByteToHexString(InBytes);
		}
		public static string HexToBIN(this byte[] InBytes)
		{
			StringBuilder strResult = new StringBuilder();
			for (int i = 0; i < InBytes.Length; i++)
			{
				string strTemp = System.Convert.ToString(InBytes[i], 2);
				strTemp = strTemp.Insert(0, new string('0', 8 - strTemp.Length));
				strResult.Append(strTemp);
			}
			return strResult.ToString();
		}
		
		public static string HexToBIN(this byte InBytes)
		{
				string strTemp = System.Convert.ToString(InBytes, 2);
				strTemp = strTemp.Insert(0, new string('0', 8 - strTemp.Length));
				return strTemp;
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ByteToHexString(System.Byte[],System.Char)" />
		public static string ToHexString(this byte[] InBytes, char segment)
		{
			return SoftBasic.ByteToHexString(InBytes, segment);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ByteToHexString(System.Byte[],System.Char,System.Int32,System.String)" />
		public static string ToHexString(this byte[] InBytes, char segment, int newLineCount, string format = "{0:X2}")
		{
			return SoftBasic.ByteToHexString(InBytes, segment, newLineCount, format);
		}

		/// <summary>
		/// 字符串转换成字节,以ASCII编码的单字节
		/// </summary>
		/// <param name="hex">任意字符串</param>
		/// <returns>转换后的字节数组</returns>
		/// <remarks>参数举例：123sfdg</remarks>
		public static byte[] ToHexBytes(this string inString) => Encoding.ASCII.GetBytes(inString);

		/// <summary>
		/// 字符串转换成字节,以Unicode编码的双字节
		/// </summary>
		/// <param name="hex">任意字符串</param>
		/// <returns>转换后的字节数组</returns>
		/// <remarks>参数举例：123sfdg</remarks>
		public static byte[] ToDoubleHexBytes(this string inString) => Encoding.Unicode.GetBytes(inString);

		/// <summary>
		/// 将16进制的字符串转化成Byte数据，将检测每2个字符转化，也就是说，中间可以是任意字符<br />
		/// Converts a 16-character string into byte data, which will detect every 2 characters converted, that is, the middle can be any character
		/// </summary>
		/// <param name="hex">十六进制的字符串，中间可以是任意的分隔符</param>
		/// <returns>转换后的字节数组</returns>
		/// <remarks>参数举例：AA 01 34 A8</remarks>
		public static byte[] HexStringToBytes(this string value)
		{
			return SoftBasic.HexStringToBytes(value);
		}


		public static byte[] ToHexBytes(this Int16 value) => System.BitConverter.GetBytes(value);
		public static byte[] ToHexBytes(this Int16[] values)
		{
			var data = new List<byte>();
			foreach (var value in values)
				data.AddRange(System.BitConverter.GetBytes(value));
			return data.ToArray();
		}
		public static byte[] ToHexBytes(this Int32 value) => System.BitConverter.GetBytes(value);
		public static byte[] ToHexBytes(this Int32[] values)
		{
			var data = new List<byte>();
			foreach (var value in values)
				data.AddRange(System.BitConverter.GetBytes(value));
			return data.ToArray();
		}
		public static byte[] ToHexBytes(this Int64 value) => System.BitConverter.GetBytes(value);
		public static byte[] ToHexBytes(this double value) => System.BitConverter.GetBytes(value);
		public static byte[] ToHexBytes(this float value) => System.BitConverter.GetBytes(value);
		public static byte[] ToHexBytes(this decimal value) => System.BitConverter.GetBytes((double)value);



		public static int ToInt(this string value)
		{
			var ivalue = int.MinValue;
			if (int.TryParse(value, out ivalue))
				return ivalue;
			return int.MinValue;
		}
		public static short ToShort(this string value)
		{
			var ivalue = short.MinValue;
			if (short.TryParse(value, out ivalue))
				return ivalue;
			return short.MinValue;
		}
		public static float ToFloat(this string value)
		{
			var ivalue = float.MinValue;
			if (float.TryParse(value, out ivalue))
				return ivalue;
			return float.MinValue;
		}
		public static decimal ToDecimal(this string value)
		{
			var ivalue = decimal.MinValue;
			if (decimal.TryParse(value, out ivalue))
				return ivalue;
			return decimal.MinValue;
		}
		public static double ToDouble(this string value)
		{
			var ivalue = double.MinValue;
			if (double.TryParse(value, out ivalue))
				return ivalue;
			return double.MinValue;
		}
		public static long ToLong(this string value)
		{
			var ivalue = long.MinValue;
			if (long.TryParse(value, out ivalue))
				return ivalue;
			return long.MinValue;
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.BoolArrayToByte(System.Boolean[])" />
		public static byte[] ToByteArray(this bool[] array)
		{
			return SoftBasic.BoolArrayToByte(array);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ByteToBoolArray(System.Byte[],System.Int32)" />
		public static bool[] ToBoolArray(this byte[] InBytes, int length)
		{
			return SoftBasic.ByteToBoolArray(InBytes, length);
		}

		/// <summary>
		/// 获取当前数组的倒序数组，这是一个新的实例，不改变原来的数组值<br />
		/// Get the reversed array of the current byte array, this is a new instance, does not change the original array value
		/// </summary>
		/// <param name="value">输入的原始数组</param>
		/// <returns>反转之后的数组信息</returns>
		public static T[] ReverseNew<T>(this T[] value)
		{
			T[] array = value.CopyArray<T>();
			Array.Reverse(array);
			return array;
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ByteToBoolArray(System.Byte[])" />
		public static bool[] ToBoolArray(this byte[] InBytes)
		{
			return SoftBasic.ByteToBoolArray(InBytes);
		}

		/// <summary>
		/// 获取Byte数组的第 bytIndex 个位置的，boolIndex偏移的bool值<br />
		/// Get the bool value of the bytIndex position of the Byte array and the boolIndex offset
		/// </summary>
		/// <param name="bytes">字节数组信息</param>
		/// <param name="bytIndex">字节的偏移位置</param>
		/// <param name="boolIndex">指定字节的位偏移</param>
		/// <returns>bool值</returns>
		public static bool GetBoolValue(this byte[] bytes, int bytIndex, int boolIndex)
		{
			return SoftBasic.BoolOnByteIndex(bytes[bytIndex], boolIndex);
		}

		/// <summary>
		/// 获取Byte数组的第 boolIndex 偏移的bool值，这个偏移值可以为 10，就是第 1 个字节的 第3位 <br />
		/// Get the bool value of the boolIndex offset of the Byte array. The offset value can be 10, which is the third bit of the first byte
		/// </summary>
		/// <param name="bytes">字节数组信息</param>
		/// <param name="boolIndex">指定字节的位偏移</param>
		/// <returns>bool值</returns>
		public static bool GetBoolByIndex(this byte[] bytes, int boolIndex)
		{
			return SoftBasic.BoolOnByteIndex(bytes[boolIndex / 8], boolIndex % 8);
		}

		/// <summary>
		/// 获取Byte的第 boolIndex 偏移的bool值，比如3，就是第4位 <br />
		/// Get the bool value of Byte's boolIndex offset, such as 3, which is the 4th bit
		/// </summary>
		/// <param name="byt">字节信息</param>
		/// <param name="boolIndex">指定字节的位偏移</param>
		/// <returns>bool值</returns>
		public static bool GetBoolByIndex(this byte byt, int boolIndex)
		{
			return SoftBasic.BoolOnByteIndex(byt, boolIndex % 8);
		}

		/// <summary>
		/// 获取short类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
		/// Get the bool value of the boolIndex (starting from 0) offset of the short type data, such as 3, which is the 4th bit
		/// </summary>
		/// <param name="value">short数据值</param>
		/// <param name="boolIndex">位偏移索引，从0开始</param>
		/// <returns>bool值</returns>
		public static bool GetBoolByIndex(this short value, int boolIndex)
		{
			return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
		}

		/// <summary>
		/// 获取ushort类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
		/// Get the bool value of the boolIndex (starting from 0) offset of the ushort type data, such as 3, which is the 4th bit
		/// </summary>
		/// <param name="value">ushort数据值</param>
		/// <param name="boolIndex">位偏移索引，从0开始</param>
		/// <returns>bool值</returns>
		public static bool GetBoolByIndex(this ushort value, int boolIndex)
		{
			return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
		}

		/// <summary>
		/// 获取int类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
		/// Get the bool value of the boolIndex (starting from 0) offset of the int type data, such as 3, which is the 4th bit
		/// </summary>
		/// <param name="value">int数据值</param>
		/// <param name="boolIndex">位偏移索引，从0开始</param>
		/// <returns>bool值</returns>
		public static bool GetBoolByIndex(this int value, int boolIndex)
		{
			return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
		}

		/// <summary>
		/// 获取uint类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
		/// Get the bool value of the boolIndex (starting from 0) offset of the uint type data, such as 3, which is the 4th bit
		/// </summary>
		/// <param name="value">uint数据值</param>
		/// <param name="boolIndex">位偏移索引，从0开始</param>
		/// <returns>bool值</returns>
		public static bool GetBoolByIndex(this uint value, int boolIndex)
		{
			return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
		}

		/// <summary>
		/// 获取long类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
		/// Get the bool value of the boolIndex (starting from 0) offset of the long type data, such as 3, which is the 4th bit
		/// </summary>
		/// <param name="value">long数据值</param>
		/// <param name="boolIndex">位偏移索引，从0开始</param>
		/// <returns>bool值</returns>
		public static bool GetBoolByIndex(this long value, int boolIndex)
		{
			return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
		}

		/// <summary>
		/// 获取ulong类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
		/// Get the bool value of the boolIndex (starting from 0) offset of the ulong type data, such as 3, which is the 4th bit
		/// </summary>
		/// <param name="value">ulong数据值</param>
		/// <param name="boolIndex">位偏移索引，从0开始</param>
		/// <returns>bool值</returns>
		public static bool GetBoolByIndex(this ulong value, int boolIndex)
		{
			return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
		}

		/// <summary>
		/// 从字节数组里提取字符串数据，如果碰到0x00字节，就直接结束
		/// </summary>
		/// <param name="buffer">原始字节信息</param>
		/// <param name="index">起始的偏移地址</param>
		/// <param name="length">字节长度信息</param>
		/// <param name="encoding">编码</param>
		/// <returns>字符串信息</returns>
		public static string GetStringOrEndChar(this byte[] buffer, int index, int length, Encoding encoding)
		{
			for (int i = index; i < index + length; i++)
			{
				bool flag = buffer[i] == 0;
				if (flag)
				{
					length = i - index;
					break;
				}
			}
			return Encoding.UTF8.GetString(buffer, index, length);
		}

		/// <summary>
		/// 设置Byte的第 boolIndex 位的bool值，可以强制为 true 或是 false, 不影响其他的位<br />
		/// Set the bool value of the boolIndex bit of Byte, which can be forced to true or false, without affecting other bits
		/// </summary>
		/// <param name="byt">字节信息</param>
		/// <param name="boolIndex">指定字节的位偏移</param>
		/// <param name="value">bool的值</param>
		/// <returns>修改之后的byte值</returns>
		public static byte SetBoolByIndex(this byte byt, int boolIndex, bool value)
		{
			return SoftBasic.SetBoolOnByteIndex(byt, boolIndex, value);
		}

		/// <summary>
		/// 设置Byte[]的第 boolIndex 位的bool值，可以强制为 true 或是 false, 不影响其他的位，如果是第 10 位，则表示第 1 个字节的第 2 位（都是从 0 地址开始算的）<br />
		/// Set the bool value of the boolIndex bit of Byte[], which can be forced to true or false, without affecting other bits. 
		/// If it is the 10th bit, it means the second bit of the first byte (both starting from the 0 address Calculated)
		/// </summary>
		/// <param name="buffer">字节数组信息</param>
		/// <param name="boolIndex">位偏移的索引</param>
		/// <param name="value">bool的值</param>
		public static void SetBoolByIndex(this byte[] buffer, int boolIndex, bool value)
		{
			buffer[boolIndex / 8] = buffer[boolIndex / 8].SetBoolByIndex(boolIndex % 8, value);
		}

		/// <summary>
		/// 修改short数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~15，之外的值会引发异常<br />
		/// Modify a bit of short data and return the modified value without affecting the original value. Bit index is 0~15, values outside will raise an exception
		/// </summary>
		/// <param name="shortValue">等待修改的short值</param>
		/// <param name="boolIndex">位索引，位索引为 0~15，之外的值会引发异常</param>
		/// <param name="value">bool值</param>
		/// <returns>修改之后的short值</returns>
		public static short SetBoolByIndex(this short shortValue, int boolIndex, bool value)
		{
			byte[] bytes = BitConverter.GetBytes(shortValue);
			bytes.SetBoolByIndex(boolIndex, value);
			return BitConverter.ToInt16(bytes, 0);
		}

		/// <summary>
		/// 修改ushort数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~15，之外的值会引发异常<br />
		/// Modify a bit of ushort data and return the modified value without affecting the original value. Bit index is 0~15, values outside will raise an exception
		/// </summary>
		/// <param name="ushortValue">等待修改的ushort值</param>
		/// <param name="boolIndex">位索引，位索引为 0~15，之外的值会引发异常</param>
		/// <param name="value">bool值</param>
		/// <returns>修改之后的ushort值</returns>
		public static ushort SetBoolByIndex(this ushort ushortValue, int boolIndex, bool value)
		{
			byte[] bytes = BitConverter.GetBytes(ushortValue);
			bytes.SetBoolByIndex(boolIndex, value);
			return BitConverter.ToUInt16(bytes, 0);
		}

		/// <summary>
		/// 修改int数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~31，之外的值会引发异常<br />
		/// Modify a bit of int data and return the modified value without affecting the original value. Bit index is 0~31, values outside will raise an exception
		/// </summary>
		/// <param name="intValue">等待修改的int值</param>
		/// <param name="boolIndex">位索引，位索引为 0~31，之外的值会引发异常</param>
		/// <param name="value">bool值</param>
		/// <returns>修改之后的int值</returns>
		public static int SetBoolByIndex(this int intValue, int boolIndex, bool value)
		{
			byte[] bytes = BitConverter.GetBytes(intValue);
			bytes.SetBoolByIndex(boolIndex, value);
			return BitConverter.ToInt32(bytes, 0);
		}

		/// <summary>
		/// 修改uint数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~31，之外的值会引发异常<br />
		/// Modify a bit of uint data and return the modified value without affecting the original value. Bit index is 0~31, values outside will raise an exception
		/// </summary>
		/// <param name="uintValue">等待修改的uint值</param>
		/// <param name="boolIndex">位索引，位索引为 0~31，之外的值会引发异常</param>
		/// <param name="value">bool值</param>
		/// <returns>修改之后的uint值</returns>
		public static uint SetBoolByIndex(this uint uintValue, int boolIndex, bool value)
		{
			byte[] bytes = BitConverter.GetBytes(uintValue);
			bytes.SetBoolByIndex(boolIndex, value);
			return BitConverter.ToUInt32(bytes, 0);
		}

		/// <summary>
		/// 修改long数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~63，之外的值会引发异常<br />
		/// Modify a bit of long data and return the modified value without affecting the original value. Bit index is 0~63, values outside will raise an exception
		/// </summary>
		/// <param name="longValue">等待修改的long值</param>
		/// <param name="boolIndex">位索引，位索引为 0~63，之外的值会引发异常</param>
		/// <param name="value">bool值</param>
		/// <returns>修改之后的long值</returns>
		public static long SetBoolByIndex(this long longValue, int boolIndex, bool value)
		{
			byte[] bytes = BitConverter.GetBytes(longValue);
			bytes.SetBoolByIndex(boolIndex, value);
			return BitConverter.ToInt64(bytes, 0);
		}

		/// <summary>
		/// 修改ulong数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~63，之外的值会引发异常<br />
		/// Modify a bit of ulong data and return the modified value without affecting the original value. Bit index is 0~63, values outside will raise an exception
		/// </summary>
		/// <param name="ulongValue">等待修改的ulong值</param>
		/// <param name="boolIndex">位索引，位索引为 0~63，之外的值会引发异常</param>
		/// <param name="value">bool值</param>
		/// <returns>修改之后的ulong值</returns>
		public static ulong SetBoolByIndex(this ulong ulongValue, int boolIndex, bool value)
		{
			byte[] bytes = BitConverter.GetBytes(ulongValue);
			bytes.SetBoolByIndex(boolIndex, value);
			return BitConverter.ToUInt64(bytes, 0);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ArrayRemoveDouble``1(``0[],System.Int32,System.Int32)" />
		public static T[] RemoveDouble<T>(this T[] value, int leftLength, int rightLength)
		{
			return SoftBasic.ArrayRemoveDouble<T>(value, leftLength, rightLength);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ArrayRemoveBegin``1(``0[],System.Int32)" />
		public static T[] RemoveBegin<T>(this T[] value, int length)
		{
			return SoftBasic.ArrayRemoveBegin<T>(value, length);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ArrayRemoveLast``1(``0[],System.Int32)" />
		public static T[] RemoveLast<T>(this T[] value, int length)
		{
			return SoftBasic.ArrayRemoveLast<T>(value, length);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ArraySelectMiddle``1(``0[],System.Int32,System.Int32)" />
		public static T[] SelectMiddle<T>(this T[] value, int index, int length)
		{
			return SoftBasic.ArraySelectMiddle<T>(value, index, length);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ArraySelectBegin``1(``0[],System.Int32)" />
		public static T[] SelectBegin<T>(this T[] value, int length)
		{
			return SoftBasic.ArraySelectBegin<T>(value, length);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ArraySelectLast``1(``0[],System.Int32)" />
		public static T[] SelectLast<T>(this T[] value, int length)
		{
			return SoftBasic.ArraySelectLast<T>(value, length);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.GetValueFromJsonObject``1(Newtonsoft.Json.Linq.JObject,System.String,``0)" />
		public static T GetValueOrDefault<T>(JObject jObject, string name, T defaultValue)
		{
			return SoftBasic.GetValueFromJsonObject<T>(jObject, name, defaultValue);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.SpliceArray``1(``0[][])" />
		public static T[] SpliceArray<T>(this T[] value, params T[][] arrays)
		{
			List<T[]> list = new List<T[]>(arrays.Length + 1);
			list.Add(value);
			list.AddRange(arrays);
			return SoftBasic.SpliceArray<T>(list.ToArray());
		}

		/// <summary>
		/// 移除指定字符串数据的最后 length 个字符。如果字符串本身的长度不足 length，则返回为空字符串。<br />
		/// Remove the last "length" characters of the specified string data. If the length of the string itself is less than length, 
		/// an empty string is returned.
		/// </summary>
		/// <param name="value">等待操作的字符串数据</param>
		/// <param name="length">准备移除的长度信息</param>
		/// <returns>移除之后的数据信息</returns>
		public static string RemoveLast(this string value, int length)
		{
			bool flag = value == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				bool flag2 = value.Length < length;
				if (flag2)
				{
					text = string.Empty;
				}
				else
				{
					text = value.Remove(value.Length - length);
				}
			}
			return text;
		}

		/// <summary>
		/// 将指定的数据添加到数组的每个元素上去，会改变每个元素的值
		/// </summary>
		/// <param name="array">原始数组</param>
		/// <param name="value">值</param>
		/// <returns>修改后的数组信息</returns>
		public static byte[] EveryByteAdd(this byte[] array, int value)
		{
			bool flag = array == null;
			byte[] array2;
			if (flag)
			{
				array2 = null;
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (byte)((int)array[i] + value);
				}
				array2 = array;
			}
			return array2;
		}

		/// <summary>
		/// 将指定的数据添加到数组的每个元素上去，使用表达式树的形式实现，将会修改原数组。不适用byte类型
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="array">原始数据</param>
		/// <param name="value">数据值</param>
		/// <returns>返回的结果信息</returns>
		public static T[] IncreaseBy<T>(this T[] array, T value)
		{
			bool flag = typeof(T) == typeof(byte);
			if (flag)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(int), "first");
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(int), "second");
				Expression expression = Expression.Add(parameterExpression, parameterExpression2);
				Expression<Func<int, int, int>> expression2 = Expression.Lambda<Func<int, int, int>>(expression, new ParameterExpression[] { parameterExpression, parameterExpression2 });
				Func<int, int, int> func = expression2.Compile();
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (T)((object)((byte)func(Convert.ToInt32(array[i]), Convert.ToInt32(value))));
				}
			}
			else
			{
				ParameterExpression parameterExpression3 = Expression.Parameter(typeof(T), "first");
				ParameterExpression parameterExpression4 = Expression.Parameter(typeof(T), "second");
				Expression expression3 = Expression.Add(parameterExpression3, parameterExpression4);
				Expression<Func<T, T, T>> expression4 = Expression.Lambda<Func<T, T, T>>(expression3, new ParameterExpression[] { parameterExpression3, parameterExpression4 });
				Func<T, T, T> func2 = expression4.Compile();
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = func2(array[j], value);
				}
			}
			return array;
		}

		/// <summary>
		/// 拷贝当前的实例数组，是基于引用层的浅拷贝，如果类型为值类型，那就是深度拷贝，如果类型为引用类型，就是浅拷贝
		/// </summary>
		/// <typeparam name="T">类型对象</typeparam>
		/// <param name="value">数组对象</param>
		/// <returns>拷贝的结果内容</returns>
		public static T[] CopyArray<T>(this T[] value)
		{
			bool flag = value == null;
			T[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				T[] array2 = new T[value.Length];
				Array.Copy(value, array2, value.Length);
				array = array2;
			}
			return array;
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ArrayFormat``1(``0[])" />
		public static string ToArrayString<T>(this T[] value)
		{
			return SoftBasic.ArrayFormat<T>(value);
		}

		/// <inheritdoc cref="M:HslCommunication.BasicFramework.SoftBasic.ArrayFormat``1(``0,System.String)" />
		public static string ToArrayString<T>(this T[] value, string format)
		{
			return SoftBasic.ArrayFormat<T>(value, format);
		}

		/// <summary>
		/// 将字符串数组转换为实际的数据数组。例如字符串格式[1,2,3,4,5]，可以转成实际的数组对象<br />
		/// Converts a string array into an actual data array. For example, the string format [1,2,3,4,5] can be converted into an actual array object
		/// </summary>
		/// <typeparam name="T">类型对象</typeparam>
		/// <param name="value">字符串数据</param>
		/// <param name="selector">转换方法</param>
		/// <returns>实际的数组</returns>
		public static T[] ToStringArray<T>(this string value, Func<string, T> selector)
		{
			bool flag = value.IndexOf('[') >= 0;
			if (flag)
			{
				value = value.Replace("[", "");
			}
			bool flag2 = value.IndexOf(']') >= 0;
			if (flag2)
			{
				value = value.Replace("]", "");
			}
			string[] array = value.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
			return array.Select(selector).ToArray<T>();
		}

		/// <summary>
		/// 将字符串数组转换为实际的数据数组。支持byte,sbyte,bool,short,ushort,int,uint,long,ulong,float,double，使用默认的十进制，例如字符串格式[1,2,3,4,5]，可以转成实际的数组对象<br />
		/// Converts a string array into an actual data array. Support byte, sbyte, bool, short, ushort, int, uint, long, ulong, float, double, use the default decimal, 
		/// such as the string format [1,2,3,4,5], which can be converted into an actual array Object
		/// </summary>
		/// <typeparam name="T">类型对象</typeparam>
		/// <param name="value">字符串数据</param>
		/// <returns>实际的数组</returns>
		public static T[] ToStringArray<T>(this string value)
		{
			Type typeFromHandle = typeof(T);
			T[] array;
			if (typeFromHandle == typeof(byte))
			{
				array = value.ToStringArray(new Func<string, byte>(byte.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(sbyte))
			{
				array = value.ToStringArray(new Func<string, sbyte>(sbyte.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(bool))
			{
				array = value.ToStringArray(new Func<string, bool>(bool.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(short))
			{
				array = value.ToStringArray(new Func<string, short>(short.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(ushort))
			{
				array = value.ToStringArray(new Func<string, ushort>(ushort.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(int))
			{
				array = value.ToStringArray(new Func<string, int>(int.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(uint))
			{
				array = value.ToStringArray(new Func<string, uint>(uint.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(long))
			{
				array = value.ToStringArray(new Func<string, long>(long.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(ulong))
			{
				array = value.ToStringArray(new Func<string, ulong>(ulong.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(float))
			{
				array = value.ToStringArray(new Func<string, float>(float.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(double))
			{
				array = value.ToStringArray(new Func<string, double>(double.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(DateTime))
			{
				array = value.ToStringArray(new Func<string, DateTime>(DateTime.Parse)) as T[];
			}
			else if (typeFromHandle == typeof(Guid))
			{
				array = value.ToStringArray(new Func<string, Guid>(Guid.Parse)) as T[];
			}
			else if (!(typeFromHandle == typeof(string)))
			{
				throw new Exception("use ToArray<T>(Func<string,T>) method instead");
			}
			return value.ToStringArray((string m) => m) as T[];
		}


		/// <summary>
		/// 根据英文小数点进行切割字符串，去除空白的字符<br />
		/// Cut the string according to the English decimal point and remove the blank characters
		/// </summary>
		/// <param name="str">字符串本身</param>
		/// <returns>切割好的字符串数组，例如输入 "100.5"，返回 "100", "5"</returns>
		public static string[] SplitDot(this string str)
		{
			return str.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// 获取当前对象的JSON格式表示的字符串。<br />
		/// Gets the string represented by the JSON format of the current object.
		/// </summary>
		/// <returns>字符串对象</returns>
		public static string ToJsonString(this object obj, Formatting formatting = Formatting.Indented)
		{
			return JsonConvert.SerializeObject(obj, formatting);
		}


		/// <inheritdoc cref="M:System.IO.MemoryStream.Write(System.Byte[],System.Int32,System.Int32)" />
		public static void Write(this MemoryStream ms, byte[] buffer)
		{
			bool flag = buffer != null;
			if (flag)
			{
				ms.Write(buffer, 0, buffer.Length);
			}
		}

		/// <summary>
		/// 将<see cref="T:System.UInt16" />数据写入到字节流，字节顺序为相反<br />
		/// Write <see cref="T:System.UInt16" /> data to the byte stream, the byte order is reversed
		/// </summary>
		/// <param name="ms">字节流</param>
		/// <param name="value">等待写入的值</param>
		public static void WriteReverse(this MemoryStream ms, ushort value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			byte b = bytes[0];
			bytes[0] = bytes[1];
			bytes[1] = b;
			ms.Write(bytes);
		}


		/// <summary>
		/// 根据指定的字节长度信息，获取到随机的字节信息<br />
		/// Obtain random byte information according to the specified byte length information
		/// </summary>
		/// <param name="random">随机数对象</param>
		/// <param name="length">字节的长度信息</param>
		/// <returns>原始字节数组</returns>
		public static byte[] GetBytes(this Random random, int length)
		{
			byte[] array = new byte[length];
			random.NextBytes(array);
			return array;
		}


		/// <summary>
		/// 将byte数组按照双字节进行反转，如果为单数的情况，则自动补齐<br />
		/// Reverses the byte array by double byte, or if the singular is the case, automatically
		/// </summary>
		/// <remarks>
		/// 例如传入的字节数据是 01 02 03 04, 那么反转后就是 02 01 04 03
		/// </remarks>
		/// <param name="inBytes">输入的字节信息</param>
		/// <returns>反转后的数据</returns>
		public static byte[] ReverseByWord(this byte[] inBytes)
		{
			return SoftBasic.BytesReverseByWord(inBytes);
		}
	}
}
