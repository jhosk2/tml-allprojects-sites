using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace NetEngine
{
	class Message
	{

		void GetMsgID(out UInt32 MsgID)
		{
			MsgID = this.MsgID;
		}

		void GetTargetID(out UInt32 TargetID)
		{
			TargetID = this.TargetID;
		}

		void Read(out Object Output)
		{
			switch (Type.GetTypeCode(Output.GetType()))
			{
				default:
					/* 아래 것들은 지원 안함.
					case TypeCode.Empty:
						break;
					case TypeCode.DBNull:
						break;
					case TypeCode.Decimal:
						break;
					 */
					SerializedContent = null;
					break;
				case TypeCode.Boolean:
					ContentStream.Write(
					Output = BitConverter.ToBoolean(SerializedContent, 0);
					break;
				case TypeCode.Char:
					Output = BitConverter.ToChar(SerializedContent, 0);
					break;
				//case TypeCode.SByte:
					//Output = BitConverter.ToSByte(SerializedContent, 0);
					//break;
				//case TypeCode.Byte:
					//Output = BitConverter.ToByte(SerializedContent, 0);
					//break;
				case TypeCode.Int16:
					Output = BitConverter.ToInt16(SerializedContent, 0);
					break;
				case TypeCode.UInt16:
					Output = BitConverter.ToUInt16(SerializedContent, 0);
					break;
				case TypeCode.Int32:
					Output = BitConverter.ToInt32(SerializedContent, 0);
					break;
				case TypeCode.UInt32:
					Output = BitConverter.ToUInt32(SerializedContent, 0);
					break;
				case TypeCode.Int64:
					Output = BitConverter.ToInt64(SerializedContent, 0);
					break;
				case TypeCode.UInt64:
					Output = BitConverter.ToUInt64(SerializedContent, 0);
					break;
				case TypeCode.Single:
					Output = BitConverter.ToSingle(SerializedContent, 0);
					break;
				case TypeCode.Double:
					Output = BitConverter.ToDouble(SerializedContent, 0);
					break;
				case TypeCode.DateTime:
					Output = DateTime.FromBinary(BitConverter.ToInt64(SerializedContent, 0));
					break;
				case TypeCode.String:
					Int32 StringLength;
					StringLength = BitConverter.ToInt32(SerializedContent, 0);
					Output = Encoding.UTF8.GetString(SerializedContent, 0, StringLength);
					break;
			}

		}

		void Write(Object Content)
		{
			Byte[] SerializedContent;

			switch(Type.GetTypeCode(Content.GetType()))
			{
				default:
					/* 아래 것들은 지원 안함.
					case TypeCode.Empty:
						break;
					case TypeCode.DBNull:
						break;
					case TypeCode.Decimal:
						break;
					 */
					SerializedContent = null;
					break;
				case TypeCode.Boolean:
					SerializedContent = BitConverter.GetBytes((Boolean)Content);
					break;
				case TypeCode.Char:
					SerializedContent = BitConverter.GetBytes((Char)Content);
					break;
				case TypeCode.SByte:
					SerializedContent = BitConverter.GetBytes((SByte)Content);
					break;
				case TypeCode.Byte:
					SerializedContent = BitConverter.GetBytes((Byte)Content);
					break;
				case TypeCode.Int16:
					SerializedContent = BitConverter.GetBytes((Int16)Content);
					break;
				case TypeCode.UInt16:
					SerializedContent = BitConverter.GetBytes((UInt16)Content);
					break;
				case TypeCode.Int32:
					SerializedContent = BitConverter.GetBytes((Int32)Content);
					break;
				case TypeCode.UInt32:
					SerializedContent = BitConverter.GetBytes((UInt32)Content);
					break;
				case TypeCode.Int64:
					SerializedContent = BitConverter.GetBytes((Int64)Content);
					break;
				case TypeCode.UInt64:
					SerializedContent = BitConverter.GetBytes((UInt64)Content);
					break;
				case TypeCode.Single:
					SerializedContent = BitConverter.GetBytes((Single)Content);
					break;
				case TypeCode.Double:
					SerializedContent = BitConverter.GetBytes((Double)Content);
					break;
				case TypeCode.DateTime:
					SerializedContent = BitConverter.GetBytes(((DateTime)Content).ToBinary());
					break;
				case TypeCode.String:
					Byte[] StringLength = BitConverter.GetBytes(((String)Content).Length);
					ContentStream.Write(StringLength, (int)ContentStream.Length, StringLength.Length);
					SerializedContent = Encoding.UTF8.GetBytes((String)Content);
					break;
			}

			if (SerializedContent != null)
			{
				ContentStream.Read(SerializedContent, (int)ContentStream.Length, SerializedContent.Length);
			}
		}
		
		private Stream ContentStream = new MemoryStream();
		private UInt32 MsgID = 0;
		private UInt32 TargetID = 0;

	}
}
