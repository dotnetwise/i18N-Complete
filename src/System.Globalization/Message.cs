using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization
{
	/// <summary>A piece of text that may have been translated into another language.</summary>
	/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:58 GMT"/>
	public class Message
	{

		#region Fields


		#region Const

		/// <summary>An empty message to be used fast</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:56:28 GMT"/>
        public static readonly Message Empty = new Message() { ExtractedComments = new List<string>(), TranslatorComments = new List<string>() };

		#endregion Const


		#region Static

		private static int _nextAutoID;

		#endregion Static


		#endregion Fields


		#region Constructors

		/// <summary>Create a new, empty Message</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:58 GMT"/>
		public Message()
		{
			AutoID = _nextAutoID++;
			MsgID = "";
			MsgStr = "";
			Contexts = new List<string>();
            TranslatorComments = new List<string>();
            ExtractedComments = new List<string>();
		}

		/// <summary>Create a new Message, setting the msgID to the supplied value</summary>
		/// <param name="msgID"></param>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:58 GMT"/>
		public Message(string msgID)
			: this()
		{
			MsgID = msgID;
		}

		/// <summary>Create a new Message, setting the supplied values</summary>
		/// <param name="msgID"></param>
		/// <param name="msgStr"></param>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:58 GMT"/>
		public Message(string msgID, string msgStr)
			: this()
		{
			MsgID = msgID;
			MsgStr = msgStr;
		}

		/// <summary>Create a new Message, setting the supplied values</summary>
		/// <param name="msgID"></param>
		/// <param name="msgStr"></param>
		/// <param name="firstContext"></param>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:58 GMT"/>
		public Message(string msgID, string msgStr, string firstContext)
			: this()
		{
			MsgID = msgID;
			MsgStr = msgStr;
			Contexts.Add(firstContext);
		}


		#endregion Constructors


		#region Properties

		/// <summary>The original text of the message, as scraped from source code.</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:58 GMT"/>
		public string MsgID { get; set; }

		/// <summary>The original plural text of the message, as scraped from source code.</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:59 GMT"/>
		public string MsgID_Plural { get; set; }

		/// <summary>Returns whether this message has a plural form</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:49:00 GMT"/>
		public bool HasPlural { get { return !string.IsNullOrEmpty(MsgID_Plural); } }

		/// <summary>The translated text, or an empty string if the message has not yet been translated.</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:58 GMT"/>
		public string MsgStr { get; set; }

		/// <summary>Gets or sets the MsgStr_Plural</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:50:16 GMT"/>
		public string MsgStr_Plural { get; set; }

		/// <summary>
		/// <para>A list of translator's comments found with this message in its .po file.</para>
		/// <para>Will only be populated if the loadComments flag is set while loading the Localization.</para>
		/// </summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:59 GMT"/>
		public List<string> TranslatorComments { get; set; }
        /// <summary>
        /// <para>A list of comments found with this message in its .po file (i.e. location where the message was found)</para>
        /// <para>Will only be populated if the loadComments flag is set while loading the Localization.</para>
        /// </summary>
        public List<string> ExtractedComments { get; set; }


		/// <summary>
		/// <para>A list of filename:linenumber pairs representing where this message
		/// was found in the source code.</para>
		/// <para>Will only be populated if the loadComments flag is set while loading the Localization.</para>
		/// </summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:59 GMT"/>
		public List<string> Contexts { get; set; }

		/// <summary>
		/// <para>A temporary short ID that can be used for lookups.</para>
		/// <para>(but don't store it since it will change depending on the order in which messages are loaded)</para>
		/// </summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:59 GMT"/>
		public int AutoID { get; set; }

		/// <summary>Gets or sets the Translated</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:59 GMT"/>
		public bool Translated { get { return !string.IsNullOrEmpty(MsgStr); } }


		#endregion Properties


		#region Override Methods

		/// <summary>
		/// Return the translated version of this message. 
		/// If no translation is available, return the message itself
		/// </summary>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:59 GMT"/>
		public override string ToString()
		{
			return String.IsNullOrEmpty(MsgStr) ? MsgID : MsgStr;
		}


		#endregion Override Methods


		#region Business Methods
		/// <summary>Encodes a string line of text suitable for msgstr</summary>
		/// <param name="text"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 17:36:19 GMT"/>
		private string Encode(string text)
		{
			return (text ?? string.Empty).Replace("\"", "\\\\").Replace("\n", "\\n\"\n\"");
		}
		/// <summary>Return this message, formatted as it would appear in a .po file</summary>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 16:47:59 GMT"/>
		public string ToPOBlock()
		{
			StringBuilder sb = new StringBuilder();
			foreach (string context in Contexts)
				sb.Append("#: ")
					.AppendLine((context ?? string.Empty).Replace("\n", "\n#: "));
			foreach (string comment in TranslatorComments)
				sb.Append("# ")
					.AppendLine((comment ?? string.Empty).Replace("\n", "\n# "));

			if (sb.Length > 0)
				sb.AppendLine();
			sb.Append("msgid \"")
				.Append(Encode(MsgID))
				.AppendLine("\"")
				;
			if (HasPlural)
				sb.Append("msgid_plural \"")
					.Append(Encode(MsgID_Plural))
					.AppendLine("\"")
					;
			sb.Append("msgstr")
				.Append(HasPlural ? "[0]" : null)
				.Append(" \"")
				.Append(Encode(MsgID))
				.AppendLine("\"")
				;
			if (HasPlural)
				sb.Append("msgstr[1]")
					.Append(" \"")
					.Append(Encode(MsgID))
					.AppendLine("\"")
					;
			sb.AppendLine();
			return sb.ToString();
		}


		#endregion Business Methods


		/// <summary>
		/// Returns either the text or the msgid.
		/// You can specify whether to return the plural form.
		/// </summary>
		/// <param name="plural"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 17:36:19 GMT"/>
		public string GetText(bool plural = false)
		{
			return
				plural
				? (string.IsNullOrEmpty(MsgStr_Plural) ? (string.IsNullOrEmpty(MsgID_Plural) ? MsgID : MsgID_Plural) : MsgStr_Plural)
				: (string.IsNullOrEmpty(MsgStr) ? MsgID : MsgStr);
		}
	}
}
