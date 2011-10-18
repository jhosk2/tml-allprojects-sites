using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

		Boolean Read(out Object output)
		{
			output = null;
			return false;
		}

		Boolean Write(Object Content)
		{
			try
			{
				StreamWriter sw = new StreamWriter(ContentStream);
				sw.Write(Content);
				sw.Close();
			}
			catch (Exception ex)
			{

			}

			return false;
		}


		private Stream ContentStream = new MemoryStream();
		private UInt32 MsgID = 0;
		private UInt32 TargetID = 0;

	}
}
