using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace NetEngine
{
	public class Message
	{

		public UInt32 GetMsgID()
		{
			return MsgID;
		}

		public UInt32 GetTargetID()
		{
			return TargetID;
		}

		public void SetMsgID(UInt32 MsgID)
		{
			this.MsgID = MsgID;
		}

		public void SetTargetID(UInt32 TargetID)
		{
			this.TargetID = TargetID;
		}

		public void Write(Object value)
		{
			if (ContentStream.Position != ContentStream.Length)
			{
				throw new InvalidOperationException("한번 읽기 시작한 스트림은 다시 쓸 수 없다!");
			}

			Type type = value.GetType();

			BinaryWriter bw = new BinaryWriter(ContentStream);

			MethodInfo method = typeof(BinaryWriter).GetMethod("Write", new Type[] { type });

			if (method == null) throw new NotSupportedException();

			method.Invoke(bw, new object[] { value });

			bw.Flush();
		}


		public void Read(out String Output)
		{
			BinaryReader br = new BinaryReader(ContentStream);
			Output = br.ReadString();
		}
		
		public void Read<T>(out T Output)
		{
			T temp = default(T);
			
			Object Param;

			BinaryReader br = new BinaryReader(ContentStream);

			if (ContentStream.Position == ContentStream.Length)
			{
				ContentStream.Position = 0;
			}

			switch( Type.GetTypeCode(temp.GetType()))
			{
				default:
					Param = null;
					break;
				case TypeCode.Boolean:
					Param = br.ReadBoolean();
					break;
				case TypeCode.Byte:
					Param = br.ReadByte();
					break;
				case TypeCode.Char:
					Param = br.ReadChar();
					break;
				case TypeCode.Decimal:
					Param = br.ReadDecimal();
					break;
				case TypeCode.Double:
					Param = br.ReadDouble();
					break;
				case TypeCode.Int16:
					Param = br.ReadInt16();
					break;
				case TypeCode.Int32:
					Param = br.ReadInt32();
					break;
				case TypeCode.Int64:
					Param = br.ReadInt64();
					break;
				case TypeCode.UInt16:
					Param = br.ReadUInt16();
					break;
				case TypeCode.UInt32:
					Param = br.ReadUInt32();
					break;
				case TypeCode.UInt64:
					Param = br.ReadUInt64();
					break;
				case TypeCode.SByte:
					Param = br.ReadSByte();
					break;
				case TypeCode.Single:
					Param = br.ReadSingle();
					break;
			}			
			Output = (T)Param;
		}

		private MemoryStream ContentStream = new MemoryStream();
		private UInt32 MsgID = 0;
		private UInt32 TargetID = 0;

	}
}
