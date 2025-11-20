using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
//using HslCommunication.Core;
using Newtonsoft.Json.Linq;

namespace EZSocketNc.Commons
{
	/// <summary>
	/// 一个软件基础类，提供常用的一些静态方法，比如字符串转换，字节转换的方法<br />
	/// A software-based class that provides some common static methods，Such as string conversion, byte conversion method
	/// </summary>
	public class SoftBasic
	{
		/// <summary>
		/// 获取文件的md5码<br />
		/// Get the MD5 code of the file
		/// </summary>
		/// <param name="filePath">文件的路径，既可以是完整的路径，也可以是相对的路径 -&gt; The path to the file</param>
		/// <returns>Md5字符串</returns>
		/// <example>
		/// 下面举例实现获取一个文件的md5码
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="CalculateFileMD5Example" title="CalculateFileMD5示例" />
		/// </example>
		public static string CalculateFileMD5(string filePath)
		{
			string text = string.Empty;
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				text = SoftBasic.CalculateStreamMD5(fileStream);
			}
			return text;
		}

		/// <summary>
		/// 获取数据流的md5码<br />
		/// Get the MD5 code for the data stream
		/// </summary>
		/// <param name="stream">数据流，可以是内存流，也可以是文件流</param>
		/// <returns>Md5字符串</returns>
		/// <example>
		/// 下面举例实现获取一个流的md5码
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="CalculateStreamMD5Example1" title="CalculateStreamMD5示例" />
		/// </example>
		// Token: 0x0600310B RID: 12555 RVA: 0x0011EA00 File Offset: 0x0011CC00
		public static string CalculateStreamMD5(Stream stream)
		{
			byte[] array = null;
			using (MD5 md = new MD5CryptoServiceProvider())
			{
				array = md.ComputeHash(stream);
			}
			return BitConverter.ToString(array).Replace("-", "");
		}

		/// <summary>
		/// 获取文本字符串信息的Md5码，编码为UTF8<br />
		/// Get the Md5 code of the text string information, using the utf-8 encoding
		/// </summary>
		/// <param name="data">文本数据信息</param>
		/// <returns>Md5字符串</returns>
		// Token: 0x0600310C RID: 12556 RVA: 0x0011EA58 File Offset: 0x0011CC58
		public static string CalculateStreamMD5(string data)
		{
			return SoftBasic.CalculateStreamMD5(data, Encoding.UTF8);
		}

		/// <summary>
		/// 获取文本字符串信息的Md5码，使用指定的编码<br />
		/// Get the Md5 code of the text string information, using the specified encoding
		/// </summary>
		/// <param name="data">文本数据信息</param>
		/// <param name="encode">编码信息</param>
		/// <returns>Md5字符串</returns>
		// Token: 0x0600310D RID: 12557 RVA: 0x0011EA68 File Offset: 0x0011CC68
		public static string CalculateStreamMD5(string data, Encoding encode)
		{
			string text = string.Empty;
			using (MD5 md = new MD5CryptoServiceProvider())
			{
				byte[] array = md.ComputeHash(encode.GetBytes(data));
				text = BitConverter.ToString(array).Replace("-", "");
			}
			return text;
		}

		///// <summary>
		///// 获取内存图片的md5码<br />
		///// Get the MD5 code of the memory picture
		///// </summary>
		///// <param name="bitmap">内存图片</param>
		///// <returns>Md5字符串</returns>
		///// <example>
		///// 下面举例实现获取一个图像的md5码
		///// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="CalculateStreamMD5Example2" title="CalculateStreamMD5示例" />
		///// </example>
		//// Token: 0x0600310E RID: 12558 RVA: 0x0011EACC File Offset: 0x0011CCCC
		//public static string CalculateStreamMD5(Bitmap bitmap)
		//{
		//	MemoryStream memoryStream = new MemoryStream();
		//	bitmap.Save(memoryStream, bitmap.RawFormat);
		//	byte[] array = null;
		//	using (MD5 md = new MD5CryptoServiceProvider())
		//	{
		//		array = md.ComputeHash(memoryStream);
		//	}
		//	memoryStream.Dispose();
		//	return SoftBasic.ByteToHexString(array);
		//}

		/// <summary>
		/// 从一个字节大小返回带单位的描述，主要是用于显示操作<br />
		/// Returns a description with units from a byte size, mainly for display operations
		/// </summary>
		/// <param name="size">实际的大小值</param>
		/// <returns>最终的字符串值</returns>
		/// <example>
		/// 比如说我们获取了文件的长度，这个长度可以来自于本地，也可以来自于数据库查询
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetSizeDescriptionExample" title="GetSizeDescription示例" />
		/// </example>
		public static string GetSizeDescription(long size)
		{
			bool flag = size < 1000L;
			string text;
			if (flag)
			{
				text = size.ToString() + " B";
			}
			else
			{
				bool flag2 = size < 1000000L;
				if (flag2)
				{
					text = ((float)size / 1024f).ToString("F2") + " Kb";
				}
				else
				{
					bool flag3 = size < 1000000000L;
					if (flag3)
					{
						text = ((float)size / 1024f / 1024f).ToString("F2") + " Mb";
					}
					else
					{
						text = ((float)size / 1024f / 1024f / 1024f).ToString("F2") + " Gb";
					}
				}
			}
			return text;
		}

		///// <summary>
		///// 从一个时间差返回带单位的描述，主要是用于显示操作。<br />
		///// Returns a description with units from a time difference, mainly for display operations.
		///// </summary>
		///// <param name="ts">实际的时间差</param>
		///// <returns>最终的字符串值</returns>
		///// <example>
		///// 比如说我们获取了一个时间差信息
		///// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetTimeSpanDescriptionExample" title="GetTimeSpanDescription示例" />
		///// </example>
		//// Token: 0x06003110 RID: 12560 RVA: 0x0011EBFC File Offset: 0x0011CDFC
		//public static string GetTimeSpanDescription(TimeSpan ts)
		//{
		//	bool flag = ts.TotalSeconds <= 60.0;
		//	string text;
		//	if (flag)
		//	{
		//		text = ((int)ts.TotalSeconds).ToString() + StringResources.Language.TimeDescriptionSecond;
		//	}
		//	else
		//	{
		//		bool flag2 = ts.TotalMinutes <= 60.0;
		//		if (flag2)
		//		{
		//			text = ts.TotalMinutes.ToString("F1") + StringResources.Language.TimeDescriptionMinute;
		//		}
		//		else
		//		{
		//			bool flag3 = ts.TotalHours <= 24.0;
		//			if (flag3)
		//			{
		//				text = ts.TotalHours.ToString("F2") + StringResources.Language.TimeDescriptionHour;
		//			}
		//			else
		//			{
		//				text = ts.TotalDays.ToString("F2") + StringResources.Language.TimeDescriptionDay;
		//			}
		//		}
		//	}
		//	return text;
		//}

		/// <summary>
		/// 将数组格式化为显示的字符串的信息，支持所有的类型对象<br />
		/// Formats the array into the displayed string information, supporting all types of objects
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="array">数组信息</param>
		/// <returns>最终显示的信息</returns>
		public static string ArrayFormat<T>(T[] array)
		{
			return SoftBasic.ArrayFormat<T>(array, string.Empty);
		}

		/// <summary>
		/// 将数组格式化为显示的字符串的信息，支持所有的类型对象<br />
		/// Formats the array into the displayed string information, supporting all types of objects
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="array">数组信息</param>
		/// <param name="format">格式化的信息</param>
		/// <returns>最终显示的信息</returns>
		public static string ArrayFormat<T>(T[] array, string format)
		{
			bool flag = array == null;
			string text;
			if (flag)
			{
				text = "NULL";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder("[");
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(string.IsNullOrEmpty(format) ? array[i].ToString() : string.Format(format, array[i]));
					bool flag2 = i != array.Length - 1;
					if (flag2)
					{
						stringBuilder.Append(",");
					}
				}
				stringBuilder.Append("]");
				text = stringBuilder.ToString();
			}
			return text;
		}

		/// <summary>
		/// 将数组格式化为显示的字符串的信息，支持所有的类型对象<br />
		/// Formats the array into the displayed string information, supporting all types of objects
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="array">数组信息</param>
		/// <returns>最终显示的信息</returns>
		public static string ArrayFormat<T>(T array)
		{
			return SoftBasic.ArrayFormat<T>(array, string.Empty);
		}

		/// <summary>
		/// 将数组格式化为显示的字符串的信息，支持所有的类型对象<br />
		/// Formats the array into the displayed string information, supporting all types of objects
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="array">数组信息</param>
		/// <param name="format">格式化的信息</param>
		/// <returns>最终显示的信息</returns>
		public static string ArrayFormat<T>(T array, string format)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			Array array2 = array as Array;
			bool flag = array2 != null;
			if (flag)
			{
				foreach (object obj in array2)
				{
					stringBuilder.Append(string.IsNullOrEmpty(format) ? obj.ToString() : string.Format(format, obj));
					stringBuilder.Append(",");
				}
				bool flag2 = array2.Length > 0 && stringBuilder[stringBuilder.Length - 1] == ',';
				if (flag2)
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
			}
			else
			{
				stringBuilder.Append(string.IsNullOrEmpty(format) ? array.ToString() : string.Format(format, array));
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// 一个通用的数组新增个数方法，会自动判断越界情况，越界的情况下，会自动的截断或是填充<br />
		/// A common array of new methods, will automatically determine the cross-border situation, in the case of cross-border, will be automatically truncated or filled
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="array">原数据</param>
		/// <param name="data">等待新增的数据</param>
		/// <param name="max">原数据的最大值</param>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="AddArrayDataExample" title="AddArrayData示例" />
		/// </example>
		public static void AddArrayData<T>(ref T[] array, T[] data, int max)
		{
			bool flag = data == null;
			if (!flag)
			{
				bool flag2 = data.Length == 0;
				if (!flag2)
				{
					bool flag3 = array.Length == max;
					if (flag3)
					{
						Array.Copy(array, data.Length, array, 0, array.Length - data.Length);
						Array.Copy(data, 0, array, array.Length - data.Length, data.Length);
					}
					else
					{
						bool flag4 = array.Length + data.Length > max;
						if (flag4)
						{
							T[] array2 = new T[max];
							for (int i = 0; i < max - data.Length; i++)
							{
								array2[i] = array[i + (array.Length - max + data.Length)];
							}
							for (int j = 0; j < data.Length; j++)
							{
								array2[array2.Length - data.Length + j] = data[j];
							}
							array = array2;
						}
						else
						{
							T[] array3 = new T[array.Length + data.Length];
							for (int k = 0; k < array.Length; k++)
							{
								array3[k] = array[k];
							}
							for (int l = 0; l < data.Length; l++)
							{
								array3[array3.Length - data.Length + l] = data[l];
							}
							array = array3;
						}
					}
				}
			}
		}

		/// <summary>
		/// 将一个数组进行扩充到指定长度，或是缩短到指定长度<br />
		/// Extend an array to a specified length, or shorten to a specified length or fill
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="data">原先数据的数据</param>
		/// <param name="length">新数组的长度</param>
		/// <returns>新数组长度信息</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArrayExpandToLengthExample" title="ArrayExpandToLength示例" />
		/// </example>
		public static T[] ArrayExpandToLength<T>(T[] data, int length)
		{
			bool flag = data == null;
			T[] array;
			if (flag)
			{
				array = new T[length];
			}
			else
			{
				bool flag2 = data.Length == length;
				if (flag2)
				{
					array = data;
				}
				else
				{
					T[] array2 = new T[length];
					Array.Copy(data, array2, Math.Min(data.Length, array2.Length));
					array = array2;
				}
			}
			return array;
		}

		/// <summary>
		/// 将一个数组进行扩充到偶数长度<br />
		/// Extend an array to even lengths
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="data">原先数据的数据</param>
		/// <returns>新数组长度信息</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArrayExpandToLengthEvenExample" title="ArrayExpandToLengthEven示例" />
		/// </example>
		public static T[] ArrayExpandToLengthEven<T>(T[] data)
		{
			bool flag = data == null;
			T[] array;
			if (flag)
			{
				array = new T[0];
			}
			else
			{
				bool flag2 = data.Length % 2 == 1;
				if (flag2)
				{
					array = SoftBasic.ArrayExpandToLength<T>(data, data.Length + 1);
				}
				else
				{
					array = data;
				}
			}
			return array;
		}

		/// <summary>
		/// 将指定的数据按照指定长度进行分割，例如int[10]，指定长度4，就分割成int[4],int[4],int[2]，然后拼接list<br />
		/// Divide the specified data according to the specified length, such as int [10], and specify the length of 4 to divide into int [4], int [4], int [2], and then concatenate the list
		/// </summary>
		/// <typeparam name="T">数组的类型</typeparam>
		/// <param name="array">等待分割的数组</param>
		/// <param name="length">指定的长度信息</param>
		/// <returns>分割后结果内容</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArraySplitByLengthExample" title="ArraySplitByLength示例" />
		/// </example>
		public static List<T[]> ArraySplitByLength<T>(T[] array, int length)
		{
			bool flag = array == null;
			List<T[]> list;
			if (flag)
			{
				list = new List<T[]>();
			}
			else
			{
				List<T[]> list2 = new List<T[]>();
				int i = 0;
				while (i < array.Length)
				{
					bool flag2 = i + length < array.Length;
					if (flag2)
					{
						T[] array2 = new T[length];
						Array.Copy(array, i, array2, 0, length);
						i += length;
						list2.Add(array2);
					}
					else
					{
						T[] array3 = new T[array.Length - i];
						Array.Copy(array, i, array3, 0, array3.Length);
						i += length;
						list2.Add(array3);
					}
				}
				list = list2;
			}
			return list;
		}

		/// <summary>
		/// 将整数进行有效的拆分成数组，指定每个元素的最大值<br />
		/// Effectively split integers into arrays, specifying the maximum value for each element
		/// </summary>
		/// <param name="integer">整数信息</param>
		/// <param name="everyLength">单个的数组长度</param>
		/// <returns>拆分后的数组长度</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="SplitIntegerToArrayExample" title="SplitIntegerToArray示例" />
		/// </example>
		public static int[] SplitIntegerToArray(int integer, int everyLength)
		{
			int[] array = new int[integer / everyLength + ((integer % everyLength == 0) ? 0 : 1)];
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = i == array.Length - 1;
				if (flag)
				{
					array[i] = ((integer % everyLength == 0) ? everyLength : (integer % everyLength));
				}
				else
				{
					array[i] = everyLength;
				}
			}
			return array;
		}

		/// <summary>
		/// 判断两个字节的指定部分是否相同<br />
		/// Determines whether the specified portion of a two-byte is the same
		/// </summary>
		/// <param name="b1">第一个字节</param>
		/// <param name="start1">第一个字节的起始位置</param>
		/// <param name="b2">第二个字节</param>
		/// <param name="start2">第二个字节的起始位置</param>
		/// <param name="length">校验的长度</param>
		/// <returns>返回是否相等</returns>
		/// <exception cref="T:System.IndexOutOfRangeException"></exception>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="IsTwoBytesEquelExample1" title="IsTwoBytesEquel示例" />
		/// </example>
		public static bool IsTwoBytesEquel(byte[] b1, int start1, byte[] b2, int start2, int length)
		{
			bool flag = b1 == null || b2 == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < length; i++)
				{
					bool flag3 = b1[i + start1] != b2[i + start2];
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		/// <summary>
		/// 判断两个字节的指定部分是否相同<br />
		/// Determines whether the specified portion of a two-byte is the same
		/// </summary>
		/// <param name="b1">第一个字节</param>
		/// <param name="b2">第二个字节</param>
		/// <returns>返回是否相等</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="IsTwoBytesEquelExample2" title="IsTwoBytesEquel示例" />
		/// </example>
		// Token: 0x0600311B RID: 12571 RVA: 0x0011F21C File Offset: 0x0011D41C
		public static bool IsTwoBytesEquel(byte[] b1, byte[] b2)
		{
			bool flag = b1 == null || b2 == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = b1.Length != b2.Length;
				flag2 = !flag3 && SoftBasic.IsTwoBytesEquel(b1, 0, b2, 0, b1.Length);
			}
			return flag2;
		}

		/// <summary>
		/// 判断两个数据的令牌是否相等<br />
		/// Determines whether the tokens of two data are equal
		/// </summary>
		/// <param name="head">字节数据</param>
		/// <param name="token">GUID数据</param>
		/// <returns>返回是否相等</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="IsTwoTokenEquelExample" title="IsByteTokenEquel示例" />
		/// </example>
		// Token: 0x0600311C RID: 12572 RVA: 0x0011F25F File Offset: 0x0011D45F
		public static bool IsByteTokenEquel(byte[] head, Guid token)
		{
			return SoftBasic.IsTwoBytesEquel(head, 12, token.ToByteArray(), 0, 16);
		}

		/// <summary>
		/// 判断两个数据的令牌是否相等<br />
		/// Determines whether the tokens of two data are equal
		/// </summary>
		/// <param name="token1">第一个令牌</param>
		/// <param name="token2">第二个令牌</param>
		/// <returns>返回是否相等</returns>
		// Token: 0x0600311D RID: 12573 RVA: 0x0011F273 File Offset: 0x0011D473
		public static bool IsTwoTokenEquel(Guid token1, Guid token2)
		{
			return SoftBasic.IsTwoBytesEquel(token1.ToByteArray(), 0, token2.ToByteArray(), 0, 16);
		}

		/// <summary>
		/// 获取一个枚举类型的所有枚举值，可直接应用于组合框数据<br />
		/// Gets all the enumeration values of an enumeration type that can be applied directly to the combo box data
		/// </summary>
		/// <typeparam name="TEnum">枚举的类型值</typeparam>
		/// <returns>枚举值数组</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetEnumValuesExample" title="GetEnumValues示例" />
		/// </example>
		// Token: 0x0600311E RID: 12574 RVA: 0x0011F28C File Offset: 0x0011D48C
		public static TEnum[] GetEnumValues<TEnum>() where TEnum : struct
		{
			return (TEnum[])Enum.GetValues(typeof(TEnum));
		}

		/// <summary>
		/// 从字符串的枚举值数据转换成真实的枚举值数据<br />
		/// Convert enumeration value data from strings to real enumeration value data
		/// </summary>
		/// <typeparam name="TEnum">枚举的类型值</typeparam>
		/// <param name="value">枚举的字符串的数据值</param>
		/// <returns>真实的枚举值</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetEnumFromStringExample" title="GetEnumFromString示例" />
		/// </example>
		public static TEnum GetEnumFromString<TEnum>(string value) where TEnum : struct
		{
			return (TEnum)((object)Enum.Parse(typeof(TEnum), value));
		}

		/// <summary>
		/// 一个泛型方法，提供json对象的数据读取<br />
		/// A generic method that provides data read for a JSON object
		/// </summary>
		/// <typeparam name="T">读取的泛型</typeparam>
		/// <param name="json">json对象</param>
		/// <param name="name">值名称</param>
		/// <param name="defaultValue">默认值</param>
		/// <returns>值对象</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetValueFromJsonObjectExample" title="GetValueFromJsonObject示例" />
		/// </example>
		public static T GetValueFromJsonObject<T>(JObject json, string name, T defaultValue)
		{
			bool flag = json.Property(name) != null;
			T t;
			if (flag)
			{
				t = json.Property(name).Value.Value<T>();
			}
			else
			{
				t = defaultValue;
			}
			return t;
		}

		/// <summary>
		/// 一个泛型方法，提供json对象的数据写入<br />
		/// A generic method that provides data writing to a JSON object
		/// </summary>
		/// <typeparam name="T">写入的泛型</typeparam>
		/// <param name="json">json对象</param>
		/// <param name="property">值名称</param>
		/// <param name="value">值数据</param>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="JsonSetValueExample" title="JsonSetValue示例" />
		/// </example>
		public static void JsonSetValue<T>(JObject json, string property, T value)
		{
			bool flag = json.Property(property) != null;
			if (flag)
			{
				json.Property(property).Value = new JValue(value);
			}
			else
			{
				json.Add(property, new JValue(value));
			}
		}

		/// <summary>
		/// 一个泛型的方法，提供XML对象的数据读取为实际的数据，支持BCL的基础类型。<br />
		/// A generic method that provides data for XML objects to be read as actual data, supporting the underlying types of BCL.
		/// </summary>
		/// <typeparam name="T">类型对象</typeparam>
		/// <param name="element">元素信息</param>
		/// <param name="name">属性的名称</param>
		/// <param name="defaultValue">默认值信息</param>
		/// <returns>解析后的值</returns>
		/// <exception cref="T:System.Exception">解析失败的异常</exception>
		// Token: 0x06003122 RID: 12578 RVA: 0x0011F344 File Offset: 0x0011D544
		public static T GetXmlValue<T>(XElement element, string name, T defaultValue)
		{
			bool flag = element.Attribute(name) == null;
			T t;
			if (flag)
			{
				t = defaultValue;
			}
			else
			{
				Type typeFromHandle = typeof(T);
				bool flag2 = typeFromHandle == typeof(bool);
				if (flag2)
				{
					t = (T)((object)bool.Parse(element.Attribute(name).Value));
				}
				else
				{
					bool flag3 = typeFromHandle == typeof(bool[]);
					if (flag3)
					{
						t = (T)((object)element.Attribute(name).Value.ToStringArray<bool>());
					}
					else
					{
						bool flag4 = typeFromHandle == typeof(byte);
						if (flag4)
						{
							t = (T)((object)byte.Parse(element.Attribute(name).Value));
						}
						else
						{
							bool flag5 = typeFromHandle == typeof(byte[]);
							if (flag5)
							{
								t = (T)((object)element.Attribute(name).Value.ToHexBytes());
							}
							else
							{
								bool flag6 = typeFromHandle == typeof(sbyte);
								if (flag6)
								{
									t = (T)((object)sbyte.Parse(element.Attribute(name).Value));
								}
								else
								{
									bool flag7 = typeFromHandle == typeof(sbyte[]);
									if (flag7)
									{
										t = (T)((object)element.Attribute(name).Value.ToStringArray<sbyte>());
									}
									else
									{
										bool flag8 = typeFromHandle == typeof(short);
										if (flag8)
										{
											t = (T)((object)short.Parse(element.Attribute(name).Value));
										}
										else
										{
											bool flag9 = typeFromHandle == typeof(short[]);
											if (flag9)
											{
												t = (T)((object)element.Attribute(name).Value.ToStringArray<short>());
											}
											else
											{
												bool flag10 = typeFromHandle == typeof(ushort);
												if (flag10)
												{
													t = (T)((object)ushort.Parse(element.Attribute(name).Value));
												}
												else
												{
													bool flag11 = typeFromHandle == typeof(ushort[]);
													if (flag11)
													{
														t = (T)((object)element.Attribute(name).Value.ToStringArray<ushort>());
													}
													else
													{
														bool flag12 = typeFromHandle == typeof(int);
														if (flag12)
														{
															t = (T)((object)int.Parse(element.Attribute(name).Value));
														}
														else
														{
															bool flag13 = typeFromHandle == typeof(int[]);
															if (flag13)
															{
																t = (T)((object)element.Attribute(name).Value.ToStringArray<int>());
															}
															else
															{
																bool flag14 = typeFromHandle == typeof(uint);
																if (flag14)
																{
																	t = (T)((object)uint.Parse(element.Attribute(name).Value));
																}
																else
																{
																	bool flag15 = typeFromHandle == typeof(uint[]);
																	if (flag15)
																	{
																		t = (T)((object)element.Attribute(name).Value.ToStringArray<uint>());
																	}
																	else
																	{
																		bool flag16 = typeFromHandle == typeof(long);
																		if (flag16)
																		{
																			t = (T)((object)long.Parse(element.Attribute(name).Value));
																		}
																		else
																		{
																			bool flag17 = typeFromHandle == typeof(long[]);
																			if (flag17)
																			{
																				t = (T)((object)element.Attribute(name).Value.ToStringArray<long>());
																			}
																			else
																			{
																				bool flag18 = typeFromHandle == typeof(ulong);
																				if (flag18)
																				{
																					t = (T)((object)ulong.Parse(element.Attribute(name).Value));
																				}
																				else
																				{
																					bool flag19 = typeFromHandle == typeof(ulong[]);
																					if (flag19)
																					{
																						t = (T)((object)element.Attribute(name).Value.ToStringArray<ulong>());
																					}
																					else
																					{
																						bool flag20 = typeFromHandle == typeof(float);
																						if (flag20)
																						{
																							t = (T)((object)float.Parse(element.Attribute(name).Value));
																						}
																						else
																						{
																							bool flag21 = typeFromHandle == typeof(float[]);
																							if (flag21)
																							{
																								t = (T)((object)element.Attribute(name).Value.ToStringArray<float>());
																							}
																							else
																							{
																								bool flag22 = typeFromHandle == typeof(double);
																								if (flag22)
																								{
																									t = (T)((object)double.Parse(element.Attribute(name).Value));
																								}
																								else
																								{
																									bool flag23 = typeFromHandle == typeof(double[]);
																									if (flag23)
																									{
																										t = (T)((object)element.Attribute(name).Value.ToStringArray<double>());
																									}
																									else
																									{
																										bool flag24 = typeFromHandle == typeof(DateTime);
																										if (flag24)
																										{
																											t = (T)((object)DateTime.Parse(element.Attribute(name).Value));
																										}
																										else
																										{
																											bool flag25 = typeFromHandle == typeof(DateTime[]);
																											if (flag25)
																											{
																												t = (T)((object)element.Attribute(name).Value.ToStringArray<DateTime>());
																											}
																											else
																											{
																												bool flag26 = typeFromHandle == typeof(string);
																												if (flag26)
																												{
																													t = (T)((object)element.Attribute(name).Value);
																												}
																												else
																												{
																													bool flag27 = typeFromHandle == typeof(string[]);
																													if (!flag27)
																													{
																														throw new Exception("not supported type:" + typeFromHandle.Name);
																													}
																													t = (T)((object)element.Attribute(name).Value.ToStringArray<string>());
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return t;
		}

		///// <summary>
		///// 显示一个完整的错误信息<br />
		///// Displays a complete error message
		///// </summary>
		///// <param name="ex">异常对象</param>
		///// <remarks>调用本方法可以显示一个异常的详细信息</remarks>
		///// <exception cref="T:System.NullReferenceException"></exception>
		//// Token: 0x06003123 RID: 12579 RVA: 0x0011F950 File Offset: 0x0011DB50
		//public static void ShowExceptionMessage(Exception ex)
		//{
		//	MessageBox.Show(SoftBasic.GetExceptionMessage(ex));
		//}

		///// <summary>
		///// 显示一个完整的错误信息，和额外的字符串描述信息<br />
		///// Displays a complete error message, and additional string description information
		///// </summary>
		///// <param name="extraMsg">额外的描述信息</param>
		///// <remarks>调用本方法可以显示一个异常的详细信息</remarks>
		///// <param name="ex">异常对象</param>
		///// <exception cref="T:System.NullReferenceException"></exception>
		//// Token: 0x06003124 RID: 12580 RVA: 0x0011F95F File Offset: 0x0011DB5F
		//public static void ShowExceptionMessage(string extraMsg, Exception ex)
		//{
		//	MessageBox.Show(SoftBasic.GetExceptionMessage(extraMsg, ex));
		//}

		///// <summary>
		///// 获取一个异常的完整错误信息<br />
		///// Gets the complete error message for an exception
		///// </summary>
		///// <param name="ex">异常对象</param>
		///// <returns>完整的字符串数据</returns>
		///// <remarks>获取异常的完整信息</remarks>
		///// <exception cref="T:System.NullReferenceException">ex不能为空</exception>
		///// <example>
		///// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetExceptionMessageExample1" title="GetExceptionMessage示例" />
		///// </example>
		//// Token: 0x06003125 RID: 12581 RVA: 0x0011F970 File Offset: 0x0011DB70
		//public static string GetExceptionMessage(Exception ex)
		//{
		//	string[] array = new string[8];
		//	array[0] = StringResources.Language.ExceptionMessage;
		//	array[1] = ex.Message;
		//	array[2] = Environment.NewLine;
		//	array[3] = StringResources.Language.ExceptionStackTrace;
		//	array[4] = ex.StackTrace;
		//	array[5] = Environment.NewLine;
		//	array[6] = StringResources.Language.ExceptionTargetSite;
		//	int num = 7;
		//	MethodBase targetSite = ex.TargetSite;
		//	array[num] = ((targetSite != null) ? targetSite.ToString() : null);
		//	return string.Concat(array);
		//}

		///// <summary>
		///// 获取一个异常的完整错误信息，和额外的字符串描述信息<br />
		///// Gets the complete error message for an exception, and additional string description information
		///// </summary>
		///// <param name="extraMsg">额外的信息</param>
		///// <param name="ex">异常对象</param>
		///// <returns>完整的字符串数据</returns>
		///// <exception cref="T:System.NullReferenceException"></exception>
		///// <example>
		///// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetExceptionMessageExample2" title="GetExceptionMessage示例" />
		///// </example>
		//// Token: 0x06003126 RID: 12582 RVA: 0x0011F9EC File Offset: 0x0011DBEC
		//public static string GetExceptionMessage(string extraMsg, Exception ex)
		//{
		//	bool flag = string.IsNullOrEmpty(extraMsg);
		//	string text;
		//	if (flag)
		//	{
		//		text = SoftBasic.GetExceptionMessage(ex);
		//	}
		//	else
		//	{
		//		text = extraMsg + Environment.NewLine + SoftBasic.GetExceptionMessage(ex);
		//	}
		//	return text;
		//}

		/// <summary>
		/// 字节数据转化成16进制表示的字符串<br />
		/// Byte data into a string of 16 binary representations
		/// </summary>
		/// <param name="InBytes">字节数组</param>
		/// <returns>返回的字符串</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample1" title="ByteToHexString示例" />
		/// </example>
		public static string ByteToHexString(byte[] InBytes)
		{
			return SoftBasic.ByteToHexString(InBytes, '\0');
		}

		/// <summary>
		/// 字节数据转化成16进制表示的字符串<br />
		/// Byte data into a string of 16 binary representations
		/// </summary>
		/// <param name="InBytes">字节数组</param>
		/// <param name="segment">分割符</param>
		/// <returns>返回的字符串</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample2" title="ByteToHexString示例" />
		/// </example>
		public static string ByteToHexString(byte[] InBytes, char segment)
		{
			return SoftBasic.ByteToHexString(InBytes, segment, 0, "{0:X2}");
		}

		/// <summary>
		/// 字节数据转化成16进制表示的字符串<br />
		/// Byte data into a string of 16 binary representations
		/// </summary>
		/// <param name="InBytes">字节数组</param>
		/// <param name="segment">分割符，如果设置为0，则没有分隔符信息</param>
		/// <param name="newLineCount">每隔指定数量的时候进行换行，如果小于等于0，则不进行分行显示</param>
		/// <param name="format">格式信息，默认为{0:X2}</param>
		/// <returns>返回的字符串</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample2" title="ByteToHexString示例" />
		/// </example>
		public static string ByteToHexString(byte[] InBytes, char segment, int newLineCount, string format = "{0:X2}")
		{
			bool flag = InBytes == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				long num = 0L;
				foreach (byte b in InBytes)
				{
					bool flag2 = segment == '\0';
					if (flag2)
					{
						stringBuilder.Append(string.Format(format, b));
					}
					else
					{
						stringBuilder.Append(string.Format(format + "{1}", b, segment));
					}
					num += 1L;
					bool flag3 = newLineCount > 0 && num >= (long)newLineCount;
					if (flag3)
					{
						stringBuilder.Append(Environment.NewLine);
						num = 0L;
					}
				}
				bool flag4 = segment != '\0' && stringBuilder.Length > 1 && stringBuilder[stringBuilder.Length - 1] == segment;
				if (flag4)
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		/// <summary>
		/// 字符串数据转化成16进制表示的字符串<br />
		/// String data into a string of 16 binary representations
		/// </summary>
		/// <param name="InString">输入的字符串数据</param>
		/// <returns>返回的字符串</returns>
		/// <exception cref="T:System.NullReferenceException"></exception>
		public static string ByteToHexString(string InString)
		{
			return SoftBasic.ByteToHexString(Encoding.Unicode.GetBytes(InString));
		}

		private static int GetHexCharIndex(char ch)
		{
			switch (ch)
			{
				case '0':
					return 0;
				case '1':
					return 1;
				case '2':
					return 2;
				case '3':
					return 3;
				case '4':
					return 4;
				case '5':
					return 5;
				case '6':
					return 6;
				case '7':
					return 7;
				case '8':
					return 8;
				case '9':
					return 9;
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
					goto IL_D6;
				case 'A':
					break;
				case 'B':
					goto IL_BD;
				case 'C':
					goto IL_C2;
				case 'D':
					goto IL_C7;
				case 'E':
					goto IL_CC;
				case 'F':
					goto IL_D1;
				default:
					switch (ch)
					{
						case 'a':
							break;
						case 'b':
							goto IL_BD;
						case 'c':
							goto IL_C2;
						case 'd':
							goto IL_C7;
						case 'e':
							goto IL_CC;
						case 'f':
							goto IL_D1;
						default:
							goto IL_D6;
					}
					break;
			}
			return 10;
		IL_BD:
			return 11;
		IL_C2:
			return 12;
		IL_C7:
			return 13;
		IL_CC:
			return 14;
		IL_D1:
			return 15;
		IL_D6:
			return -1;
		}

		/// <summary>
		/// 将16进制的字符串转化成Byte数据，将检测每2个字符转化，也就是说，中间可以是任意字符<br />
		/// Converts a 16-character string into byte data, which will detect every 2 characters converted, that is, the middle can be any character
		/// </summary>
		/// <param name="hex">十六进制的字符串，中间可以是任意的分隔符</param>
		/// <returns>转换后的字节数组</returns>
		/// <remarks>参数举例：AA 01 34 A8</remarks>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="HexStringToBytesExample" title="HexStringToBytes示例" />
		/// </example>
		public static byte[] HexStringToBytes(string hex)
		{
			MemoryStream memoryStream = new MemoryStream();
			for (int i = 0; i < hex.Length; i++)
			{
				if (i + 1 < hex.Length)
				{
					if (SoftBasic.GetHexCharIndex(hex[i]) >= 0 && SoftBasic.GetHexCharIndex(hex[i + 1]) >= 0)
					{
						memoryStream.WriteByte((byte)(SoftBasic.GetHexCharIndex(hex[i]) * 16 + SoftBasic.GetHexCharIndex(hex[i + 1])));
						i++;
					}
				}
			}
			byte[] array = memoryStream.ToArray();
			memoryStream.Dispose();
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
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="BytesReverseByWord" title="BytesReverseByWord示例" />
		/// </example>
		public static byte[] BytesReverseByWord(byte[] inBytes)
		{
			byte[] array;
			if (inBytes == null)
			{
				array = null;
			}
			else if (inBytes.Length == 0)
			{
				array = new byte[0];
			}
			else
			{
				byte[] array2 = SoftBasic.ArrayExpandToLengthEven<byte>(inBytes.CopyArray<byte>());
				for (int i = 0; i < array2.Length / 2; i++)
				{
					byte b = array2[i * 2];
					array2[i * 2] = array2[i * 2 + 1];
					array2[i * 2 + 1] = b;
				}
				array = array2;
			}
			return array;
		}

		/// <summary>
		/// 将字节数组显示为ASCII格式的字符串，当遇到0x20以下及0x7E以上的不可见字符时，使用十六进制的数据显示<br />
		/// Display the byte array as a string in ASCII format, when encountering invisible characters below 0x20 and above 0x7E, use hexadecimal data to display<br />
		/// </summary>
		/// <param name="content">字节数组信息</param>
		/// <returns>ASCII格式的字符串信息</returns>
		public static string GetAsciiStringRender(byte[] content)
		{
			string text;
			if (content == null)
			{
				text = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < content.Length; i++)
				{
					if(content[i] == 0 )continue;
					if (content[i] < 32 || content[i] > 126)
					{
						stringBuilder.Append(string.Format("\\{0:X2}", content[i]));
					}
					else
					{
						stringBuilder.Append((char)content[i]);
					}
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		/// <summary>
		/// 从显示的ASCII格式的字符串，转为原始字节数组，如果遇到 \00 这种表示原始字节的内容，则直接进行转换操作，遇到 \r 直接转换 0x0D, \n 直接转换 0x0A<br />
		/// Convert from the displayed string in ASCII format to the original byte array. If you encounter \00, which represents the original byte content,
		/// the conversion operation is performed directly. When encountering \r, it is directly converted to 0x0D, and \n is directly converted to 0x0A.
		/// </summary>
		/// <param name="render">等待转换的字符串</param>
		/// <returns>原始字节数组</returns>
		public static byte[] GetFromAsciiStringRender(string render)
		{
			byte[] array;
			if (string.IsNullOrEmpty(render))
			{
				array = new byte[0];
			}
			else
			{
				MatchEvaluator matchEvaluator = (Match m) => string.Format("{0}", (char)Convert.ToByte(m.Value.Substring(1), 16));
				array = Encoding.ASCII.GetBytes(Regex.Replace(render.Replace("\\r", "\r").Replace("\\n", "\n"), "\\\\[0-9A-Fa-f]{2}", matchEvaluator));
			}
			return array;
		}

		/// <summary>
		/// 将原始的byte数组转换成ascii格式的byte数组<br />
		/// Converts the original byte array to an ASCII-formatted byte array
		/// </summary>
		/// <param name="inBytes">等待转换的byte数组</param>
		/// <returns>转换后的数组</returns>
		public static byte[] BytesToAsciiBytes(byte[] inBytes)
		{
			return Encoding.ASCII.GetBytes(SoftBasic.ByteToHexString(inBytes));
		}

		/// <summary>
		/// 将ascii格式的byte数组转换成原始的byte数组<br />
		/// Converts an ASCII-formatted byte array to the original byte array
		/// </summary>
		/// <param name="inBytes">等待转换的byte数组</param>
		/// <returns>转换后的数组</returns>
		public static byte[] AsciiBytesToBytes(byte[] inBytes)
		{
			return SoftBasic.HexStringToBytes(Encoding.ASCII.GetString(inBytes));
		}

		/// <summary>
		/// 从字节构建一个ASCII格式的数据内容<br />
		/// Build an ASCII-formatted data content from bytes
		/// </summary>
		/// <param name="value">数据</param>
		/// <returns>ASCII格式的字节数组</returns>
		public static byte[] BuildAsciiBytesFrom(byte value)
		{
			return Encoding.ASCII.GetBytes(value.ToString("X2"));
		}

		/// <summary>
		/// 从short构建一个ASCII格式的数据内容<br />
		/// Constructing an ASCII-formatted data content from a short
		/// </summary>
		/// <param name="value">数据</param>
		/// <returns>ASCII格式的字节数组</returns>
		public static byte[] BuildAsciiBytesFrom(short value)
		{
			return Encoding.ASCII.GetBytes(value.ToString("X4"));
		}

		/// <summary>
		/// 从ushort构建一个ASCII格式的数据内容<br />
		/// Constructing an ASCII-formatted data content from ushort
		/// </summary>
		/// <param name="value">数据</param>
		/// <returns>ASCII格式的字节数组</returns>
		public static byte[] BuildAsciiBytesFrom(ushort value)
		{
			return Encoding.ASCII.GetBytes(value.ToString("X4"));
		}

		/// <summary>
		/// 从uint构建一个ASCII格式的数据内容<br />
		/// Constructing an ASCII-formatted data content from uint
		/// </summary>
		/// <param name="value">数据</param>
		/// <returns>ASCII格式的字节数组</returns>
		public static byte[] BuildAsciiBytesFrom(uint value)
		{
			return Encoding.ASCII.GetBytes(value.ToString("X8"));
		}

		/// <summary>
		/// 从字节数组构建一个ASCII格式的数据内容<br />
		/// Byte array to construct an ASCII format data content
		/// </summary>
		/// <param name="value">字节信息</param>
		/// <returns>ASCII格式的地址</returns>
		public static byte[] BuildAsciiBytesFrom(byte[] value)
		{
			byte[] array = new byte[value.Length * 2];
			for (int i = 0; i < value.Length; i++)
			{
				SoftBasic.BuildAsciiBytesFrom(value[i]).CopyTo(array, 2 * i);
			}
			return array;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		private static byte GetDataByBitIndex(int offset)
		{
			byte b;
			switch (offset)
			{
				case 0:
					b = 1;
					break;
				case 1:
					b = 2;
					break;
				case 2:
					b = 4;
					break;
				case 3:
					b = 8;
					break;
				case 4:
					b = 16;
					break;
				case 5:
					b = 32;
					break;
				case 6:
					b = 64;
					break;
				case 7:
					b = 128;
					break;
				default:
					b = 0;
					break;
			}
			return b;
		}

		/// <summary>
		/// 获取byte数据类型的第offset位，是否为True<br />
		/// Gets the index bit of the byte data type, whether it is True
		/// </summary>
		/// <param name="value">byte数值</param>
		/// <param name="offset">索引位置</param>
		/// <returns>结果</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="BoolOnByteIndex" title="BoolOnByteIndex示例" />
		/// </example>
		public static bool BoolOnByteIndex(byte value, int offset)
		{
			byte dataByBitIndex = SoftBasic.GetDataByBitIndex(offset);
			return (value & dataByBitIndex) == dataByBitIndex;
		}

		/// <summary>
		/// 设置取byte数据类型的第offset位，是否为True<br />
		/// Set the offset bit of the byte data type, whether it is True
		/// </summary>
		/// <param name="byt">byte数值</param>
		/// <param name="offset">索引位置</param>
		/// <param name="value">写入的结果值</param>
		/// <returns>结果</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="SetBoolOnByteIndex" title="SetBoolOnByteIndex示例" />
		/// </example>
		public static byte SetBoolOnByteIndex(byte byt, int offset, bool value)
		{
			byte dataByBitIndex = SoftBasic.GetDataByBitIndex(offset);
			byte b;
			if (value)
			{
				b = (byte)(byt | dataByBitIndex);
			}
			else
			{
				b = (byte)(byt & ~dataByBitIndex);
			}
			return b;
		}

		/// <summary>
		/// 将bool数组转换到byte数组<br />
		/// Converting a bool array to a byte array
		/// </summary>
		/// <param name="array">bool数组</param>
		/// <returns>转换后的字节数组</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="BoolArrayToByte" title="BoolArrayToByte示例" />
		/// </example>
		public static byte[] BoolArrayToByte(bool[] array)
		{
			byte[] array2;
			if (array == null)
			{
				array2 = null;
			}
			else
			{
				int num = ((array.Length % 8 == 0) ? (array.Length / 8) : (array.Length / 8 + 1));
				byte[] array3 = new byte[num];
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i])
					{
						byte[] array4 = array3;
						int num2 = i / 8;
						array4[num2] += SoftBasic.GetDataByBitIndex(i % 8);
					}
				}
				array2 = array3;
			}
			return array2;
		}

		/// <summary>
		/// 将bool数组转换为字符串进行显示，true被转为1，false转换为0<br />
		/// Convert the bool array to a string for display, true is converted to 1, false is converted to 0
		/// </summary>
		/// <param name="array">bool数组</param>
		/// <returns>转换后的字符串</returns>
		public static string BoolArrayToString(bool[] array)
		{
			string text;
			if (array == null)
			{
				text = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i] ? "1" : "0");
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		/// <summary>
		/// 从Byte数组中提取位数组，length代表位数<br />
		/// Extracts a bit array from a byte array, length represents the number of digits
		/// </summary>
		/// <param name="inBytes">原先的字节数组</param>
		/// <param name="length">想要转换的长度，如果超出自动会缩小到数组最大长度</param>
		/// <returns>转换后的bool数组</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToBoolArray" title="ByteToBoolArray示例" />
		/// </example> 
		public static bool[] ByteToBoolArray(byte[] inBytes, int length)
		{
			bool[] array;
			if (inBytes == null)
			{
				array = null;
			}
			else
			{
				if (length > inBytes.Length * 8)
				{
					length = inBytes.Length * 8;
				}
				bool[] array2 = new bool[length];
				for (int i = 0; i < length; i++)
				{
					array2[i] = SoftBasic.BoolOnByteIndex(inBytes[i / 8], i % 8);
				}
				array = array2;
			}
			return array;
		}

		/// <summary>
		/// 从Byte数组中提取所有的位数组<br />
		/// Extracts a bit array from a byte array, length represents the number of digits
		/// </summary>
		/// <param name="InBytes">原先的字节数组</param>
		/// <returns>转换后的bool数组</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToBoolArray" title="ByteToBoolArray示例" />
		/// </example> 
		public static bool[] ByteToBoolArray(byte[] InBytes)
		{
			return (InBytes == null) ? null : SoftBasic.ByteToBoolArray(InBytes, InBytes.Length * 8);
		}

		/// <summary>
		/// 将一个数组的前后移除指定位数，返回新的一个数组<br />
		/// Removes a array before and after the specified number of bits, returning a new array
		/// </summary>
		/// <param name="value">数组</param>
		/// <param name="leftLength">前面的位数</param>
		/// <param name="rightLength">后面的位数</param>
		/// <returns>新的数组</returns>
		/// <exception cref="T:System.RankException"></exception>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArrayRemoveDouble" title="ArrayRemoveDouble示例" />
		/// </example> 
		public static T[] ArrayRemoveDouble<T>(T[] value, int leftLength, int rightLength)
		{
			T[] array;
			if (value == null)
			{
				array = null;
			}
			else
			if (value.Length <= leftLength + rightLength)
			{
				array = new T[0];
			}
			else
			{
				T[] array2 = new T[value.Length - leftLength - rightLength];
				Array.Copy(value, leftLength, array2, 0, array2.Length);
				array = array2;
			}
			return array;
		}

		/// <summary>
		/// 将一个数组的前面指定位数移除，返回新的一个数组<br />
		/// Removes the preceding specified number of bits in a array, returning a new array
		/// </summary>
		/// <param name="value">数组</param>
		/// <param name="length">等待移除的长度</param>
		/// <returns>新的数组</returns>
		/// <exception cref="T:System.RankException"></exception>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArrayRemoveBegin" title="ArrayRemoveBegin示例" />
		/// </example> 
		public static T[] ArrayRemoveBegin<T>(T[] value, int length)
		{
			return SoftBasic.ArrayRemoveDouble<T>(value, length, 0);
		}

		/// <summary>
		/// 将一个数组的后面指定位数移除，返回新的一个数组<br />
		/// Removes the specified number of digits after a array, returning a new array
		/// </summary>
		/// <param name="value">数组</param>
		/// <param name="length">等待移除的长度</param>
		/// <returns>新的数组</returns>
		/// <exception cref="T:System.RankException"></exception>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArrayRemoveLast" title="ArrayRemoveLast示例" />
		/// </example> 
		public static T[] ArrayRemoveLast<T>(T[] value, int length)
		{
			return SoftBasic.ArrayRemoveDouble<T>(value, 0, length);
		}

		/// <summary>
		/// 获取到数组里面的中间指定长度的数组<br />
		/// Get an array of the specified length in the array
		/// </summary>
		/// <param name="value">数组</param>
		/// <param name="index">起始索引</param>
		/// <param name="length">数据的长度</param>
		/// <returns>新的数组值</returns>
		/// <exception cref="T:System.IndexOutOfRangeException"></exception>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArraySelectMiddle" title="ArraySelectMiddle示例" />
		/// </example> 
		public static T[] ArraySelectMiddle<T>(T[] value, int index, int length)
		{
			T[] array;
			if (value == null)
			{
				array = null;
			}
			else if (length == 0)
			{
				array = new T[0];
			}
			else
			{
				T[] array2 = new T[Math.Min(value.Length, length)];
				Array.Copy(value, index, array2, 0, array2.Length);
				array = array2;
			}
			return array;
		}

		/// <summary>
		/// 选择一个数组的前面的几个数据信息<br />
		/// Select the begin few items of data information of a array
		/// </summary>
		/// <param name="value">数组</param>
		/// <param name="length">数据的长度</param>
		/// <returns>新的数组</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArraySelectBegin" title="ArraySelectBegin示例" />
		/// </example> 
		public static T[] ArraySelectBegin<T>(T[] value, int length)
		{
			T[] array;
			if (length == 0)
			{
				array = new T[0];
			}
			else
			{
				T[] array2 = new T[Math.Min(value.Length, length)];
				if (array2.Length != 0)
				{
					Array.Copy(value, 0, array2, 0, array2.Length);
				}
				array = array2;
			}
			return array;
		}

		/// <summary>
		/// 选择一个数组的后面的几个数据信息<br />
		/// Select the last few items of data information of a array
		/// </summary>
		/// <param name="value">数组</param>
		/// <param name="length">数据的长度</param>
		/// <returns>新的数组信息</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ArraySelectLast" title="ArraySelectLast示例" />
		/// </example> 
		public static T[] ArraySelectLast<T>(T[] value, int length)
		{
			T[] array;
			if (length == 0)
			{
				array = new T[0];
			}
			else
			{
				T[] array2 = new T[Math.Min(value.Length, length)];
				Array.Copy(value, value.Length - length, array2, 0, array2.Length);
				array = array2;
			}
			return array;
		}

		/// <summary>
		/// 拼接任意个泛型数组为一个总的泛型数组对象，采用深度拷贝实现。<br />
		/// Splicing any number of generic arrays into a total generic array object is implemented using deep copy.
		/// </summary>
		/// <typeparam name="T">数组的类型信息</typeparam>
		/// <param name="arrays">任意个长度的数组</param>
		/// <returns>拼接之后的最终的结果对象</returns>
		/// <example>
		/// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="SpliceByteArray" title="SpliceByteArray示例" />
		/// </example> 
		public static T[] SpliceArray<T>(params T[][] arrays)
		{
			int num = 0;
			for (int i = 0; i < arrays.Length; i++)
			{
				T[] array = arrays[i];
				if (array != null && array.Length != 0)
				{
					num += arrays[i].Length;
				}
			}
			int num2 = 0;
			T[] array2 = new T[num];
			for (int j = 0; j < arrays.Length; j++)
			{
				T[] array3 = arrays[j];
				if (array3 != null && array3.Length != 0)
				{
					arrays[j].CopyTo(array2, num2);
					num2 += arrays[j].Length;
				}
			}
			return array2;
		}

		/// <summary>
		/// 将一个<see cref="T:System.String" />的数组和多个<see cref="T:System.String" /> 类型的对象整合成一个数组<br />
		/// Combine an array of <see cref="T:System.String" /> and multiple objects of type <see cref="T:System.String" /> into an array
		/// </summary>
		/// <param name="first">第一个数组对象</param>
		/// <param name="array">字符串数组信息</param>
		/// <returns>总的数组对象</returns>
		public static string[] SpliceStringArray(string first, string[] array)
		{
			List<string> list = new List<string>();
			list.Add(first);
			list.AddRange(array);
			return list.ToArray();
		}

		/// <summary>
		/// 将两个<see cref="T:System.String" />的数组和多个<see cref="T:System.String" /> 类型的对象整合成一个数组<br />
		/// Combine two arrays of <see cref="T:System.String" /> and multiple objects of type <see cref="T:System.String" /> into one array
		/// </summary>
		/// <param name="first">第一个数据对象</param>
		/// <param name="second">第二个数据对象</param>
		/// <param name="array">字符串数组信息</param>
		/// <returns>总的数组对象</returns>
		public static string[] SpliceStringArray(string first, string second, string[] array)
		{
			List<string> list = new List<string>();
			list.Add(first);
			list.Add(second);
			list.AddRange(array);
			return list.ToArray();
		}

		/// <summary>
		/// 将两个<see cref="T:System.String" />的数组和多个<see cref="T:System.String" /> 类型的对象整合成一个数组<br />
		/// Combine two arrays of <see cref="T:System.String" /> and multiple objects of type <see cref="T:System.String" /> into one array
		/// </summary>
		/// <param name="first">第一个数据对象</param>
		/// <param name="second">第二个数据对象</param>
		/// <param name="third">第三个数据对象</param>
		/// <param name="array">字符串数组信息</param>
		/// <returns>总的数组对象</returns>
		public static string[] SpliceStringArray(string first, string second, string third, string[] array)
		{
			List<string> list = new List<string>();
			list.Add(first);
			list.Add(second);
			list.Add(third);
			list.AddRange(array);
			return list.ToArray();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="h"></param>
		/// <returns></returns>
		public static int HexToInt(char h)
		{
			return (h >= '0' && h <= '9') ? ((int)(h - '0')) : ((h >= 'a' && h <= 'f') ? ((int)(h - 'a' + '\n')) : ((h >= 'A' && h <= 'F') ? ((int)(h - 'A' + '\n')) : (-1)));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="skipUtf16Validation"></param>
		/// <returns></returns>
		private static string ValidateString(string input, bool skipUtf16Validation)
		{
			string text;
			if (skipUtf16Validation || string.IsNullOrEmpty(input))
			{
				text = input;
			}
			else
			{
				int num = -1;
				for (int i = 0; i < input.Length; i++)
				{
					if (char.IsSurrogate(input[i]))
					{
						num = i;
						break;
					}
				}
				if (num < 0)
				{
					text = input;
				}
				else
				{
					char[] array = input.ToCharArray();
					for (int j = num; j < array.Length; j++)
					{
						char c = array[j];
						if (char.IsLowSurrogate(c))
						{
							array[j] = '\ufffd';
						}
						else if (char.IsHighSurrogate(c))
						{
							if (j + 1 < array.Length && char.IsLowSurrogate(array[j + 1]))
							{
								j++;
							}
							else
							{
								array[j] = '\ufffd';
							}
						}
					}
					text = new string(array);
				}
			}
			return text;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
		{
			bool flag2 = true;
			if (bytes == null && count == 0)
			{
				flag2 = false;
			}
			else if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (offset < 0 || offset > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return flag2;
		}


		private static bool IsUrlSafeChar(char ch)
		{
			bool flag2;
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				flag2 = true;
			}
			else
			{
				if (ch != '!')
				{
					switch (ch)
					{
						case '(':
						case ')':
						case '*':
						case '-':
						case '.':
							goto IL_69;
						case '+':
						case ',':
							break;
						default:
							if (ch == '_')
							{
								goto IL_69;
							}
							break;
					}
					return false;
				}
			IL_69:
				flag2 = true;
			}
			return flag2;
		}

		private static string UrlEncodeSpaces(string str)
		{
			if (str != null && str.IndexOf(' ') >= 0)
			{
				str = str.Replace(" ", "%20");
			}
			return str;
		}


		private static char IntToHex(int n)
		{
			Debug.Assert(n < 16);
			char c;
			if (n <= 9)
			{
				c = (char)(n + 48);
			}
			else
			{
				c = (char)(n - 10 + 65);
			}
			return c;
		}

		private static byte[] UrlEncodeToBytes(byte[] bytes)
		{
			int num = 0;
			int num2 = bytes.Length;
			byte[] array;
			if (!SoftBasic.ValidateUrlEncodingParameters(bytes, num, num2))
			{
				array = null;
			}
			else
			{
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < num2; i++)
				{
					char c = (char)bytes[num + i];
					if (c == ' ')
					{
						num3++;
					}
					else if (!SoftBasic.IsUrlSafeChar(c))
					{
						num4++;
					}
				}
				if (num3 == 0 && num4 == 0)
				{
					if (num == 0 && bytes.Length == num2)
					{
						array = bytes;
					}
					else
					{
						byte[] array2 = new byte[num2];
						Buffer.BlockCopy(bytes, num, array2, 0, num2);
						array = array2;
					}
				}
				else
				{
					byte[] array3 = new byte[num2 + num4 * 2];
					int num5 = 0;
					for (int j = 0; j < num2; j++)
					{
						byte b = bytes[num + j];
						char c2 = (char)b;
						if (SoftBasic.IsUrlSafeChar(c2))
						{
							array3[num5++] = b;
						}
						else if (c2 == ' ')
						{
							array3[num5++] = 43;
						}
						else
						{
							array3[num5++] = 37;
							array3[num5++] = (byte)SoftBasic.IntToHex((b >> 4) & 15);
							array3[num5++] = (byte)SoftBasic.IntToHex((int)(b & 15));
						}
					}
					array = array3;
				}
			}
			return array;
		}

		private static byte[] UrlEncode(byte[] bytes, bool alwaysCreateNewReturnValue)
		{
			byte[] array = SoftBasic.UrlEncodeToBytes(bytes);
			return (alwaysCreateNewReturnValue && array != null && array == bytes) ? ((byte[])array.Clone()) : array;
		}

		/// <summary>
		/// 将字符串编码为URL可以识别的字符串，中文会被编码为%开头的数据，例如 中文 转义为 %2F%E4%B8%AD%E6%96%87 <br />
		/// Encoding a string as a URL-recognizable string Chinese encoded as data that begins with %, such as 中文 escaped as %2F%E4%B8%AD%E6%96%87
		/// </summary>
		/// <param name="str">等待转换的字符串数据</param>
		/// <param name="e">编码信息，一般为 UTF8 </param>
		/// <returns>编码之后的结果</returns>
		public static string UrlEncode(string str, Encoding e)
		{
			string text;
			if (str == null)
			{
				text = null;
			}
			else
			{
				byte[] bytes = e.GetBytes(str);
				text = Encoding.ASCII.GetString(SoftBasic.UrlEncode(bytes, true));
			}
			return text;
		}

		///// <summary>
		///// 将url的编码解码为真实的字符串，例如 %2F%E4%B8%AD%E6%96%87 解码为 中文<br />
		///// Decode the encoding of url as a real string, for example %2F%E4%B8%AD%E6%96%87 to 中文
		///// </summary>
		///// <param name="value">等待转换的值</param>
		///// <param name="encoding">编码信息，一般为 UTF8</param>
		///// <returns>解码之后的结果</returns>
		//// Token: 0x06003151 RID: 12625 RVA: 0x0012084C File Offset: 0x0011EA4C
		//public static string UrlDecode(string value, Encoding encoding)
		//{
		//	int length = value.Length;
		//	UrlDecoder urlDecoder = new UrlDecoder(length, encoding);
		//	int i = 0;
		//	while (i < length)
		//	{
		//		char c = value[i];
		//		bool flag = c == '+';
		//		if (flag)
		//		{
		//			c = ' ';
		//			goto IL_146;
		//		}
		//		bool flag2 = c == '%' && i < length - 2;
		//		if (flag2)
		//		{
		//			bool flag3 = value[i + 1] == 'u' && i < length - 5;
		//			if (flag3)
		//			{
		//				int num = SoftBasic.HexToInt(value[i + 2]);
		//				int num2 = SoftBasic.HexToInt(value[i + 3]);
		//				int num3 = SoftBasic.HexToInt(value[i + 4]);
		//				int num4 = SoftBasic.HexToInt(value[i + 5]);
		//				bool flag4 = num >= 0 && num2 >= 0 && num3 >= 0 && num4 >= 0;
		//				if (flag4)
		//				{
		//					c = (char)((num << 12) | (num2 << 8) | (num3 << 4) | num4);
		//					i += 5;
		//					urlDecoder.AddChar(c);
		//					goto IL_16A;
		//				}
		//			}
		//			else
		//			{
		//				int num5 = SoftBasic.HexToInt(value[i + 1]);
		//				int num6 = SoftBasic.HexToInt(value[i + 2]);
		//				bool flag5 = num5 >= 0 && num6 >= 0;
		//				if (flag5)
		//				{
		//					byte b = (byte)((num5 << 4) | num6);
		//					i += 2;
		//					urlDecoder.AddByte(b);
		//					goto IL_16A;
		//				}
		//			}
		//			goto IL_146;
		//		}
		//		goto IL_146;
		//		IL_16A:
		//		i++;
		//		continue;
		//		IL_146:
		//		bool flag6 = (c & 'ﾀ') == '\0';
		//		if (flag6)
		//		{
		//			urlDecoder.AddByte((byte)c);
		//		}
		//		else
		//		{
		//			urlDecoder.AddChar(c);
		//		}
		//		goto IL_16A;
		//	}
		//	return SoftBasic.ValidateString(urlDecoder.GetString(), true);
		//}

		///// <summary>
		///// 设置或获取系统框架的版本号<br />
		///// Set or get the version number of the system framework
		///// </summary>
		///// <remarks>
		///// 当你要显示本组件框架的版本号的时候，就可以用这个属性来显示
		///// </remarks>
		//// Token: 0x170009FD RID: 2557
		//// (get) Token: 0x06003152 RID: 12626 RVA: 0x001209E6 File Offset: 0x0011EBE6
		//public static SystemVersion FrameworkVersion
		//{
		//	get
		//	{
		//		return new SystemVersion("11.6.3");
		//	}
		//}

		///// <summary>
		///// 使用序列化反序列化深度克隆一个对象，该对象需要支持序列化特性<br />
		///// Cloning an object with serialization deserialization depth that requires support for serialization attributes
		///// </summary>
		///// <param name="oringinal">源对象，支持序列化</param>
		///// <returns>新的一个实例化的对象</returns>
		///// <exception cref="T:System.NullReferenceException"></exception>
		///// <exception cref="T:System.NonSerializedAttribute"></exception>
		///// <remarks>
		///// <note type="warning">
		///// <paramref name="oringinal" /> 参数必须实现序列化的特性
		///// </note>
		///// </remarks>
		///// <example>
		///// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="DeepClone" title="DeepClone示例" />
		///// </example>
		//public static object DeepClone(object oringinal)
		//{
		//	object obj;
		//	using (MemoryStream memoryStream = new MemoryStream())
		//	{
		//		BinaryFormatter binaryFormatter = new BinaryFormatter
		//		{
		//			Context = new StreamingContext(StreamingContextStates.Clone)
		//		};
		//		binaryFormatter.Serialize(memoryStream, oringinal);
		//		memoryStream.Position = 0L;
		//		obj = binaryFormatter.Deserialize(memoryStream);
		//	}
		//	return obj;
		//}

		///// <summary>
		///// 获取一串唯一的随机字符串，长度为20，由Guid码和4位数的随机数组成，保证字符串的唯一性<br />
		///// Gets a string of unique random strings with a length of 20, consisting of a GUID code and a 4-digit random number to guarantee the uniqueness of the string
		///// </summary>
		///// <returns>随机字符串数据</returns>
		///// <example>
		///// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetUniqueStringByGuidAndRandom" title="GetUniqueStringByGuidAndRandom示例" />
		///// </example>
		//// Token: 0x06003154 RID: 12628 RVA: 0x00120A58 File Offset: 0x0011EC58
		//public static string GetUniqueStringByGuidAndRandom()
		//{
		//	return Guid.NewGuid().ToString("N") + HslHelper.HslRandom.Next(1000, 10000).ToString();
		//}
	}
}
