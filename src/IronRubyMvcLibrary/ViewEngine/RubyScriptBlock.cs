using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronRubyViewEngine
{
	internal class RubyScriptBlock
	{
		public static RubyScriptBlock Parse(string block) {
			return new RubyScriptBlock(block);
		}

		private RubyScriptBlock(string block)
		{
			bool ignoreNewLine = ignoreNextNewLine;

			if (String.IsNullOrEmpty(block))
			{
				this.contents = string.Empty;
				return;
			}

			int endOffset = 4;
			if (block.EndsWith("-%>")) {
				endOffset = 5;
				ignoreNextNewLine = true;
			}
			else {
				ignoreNextNewLine = false;
			}

			if (block.StartsWith("<%=")) {
                int outputLength = block.Length - endOffset - 1;
                if(outputLength < 1)
                    throw new InvalidOperationException("Started a '<%=' block without ending it.");

                string output = block.Substring(3, outputLength).Trim();
                this.contents = String.Format("$response.Write({0})", output).Trim();
				return;
			}

			if (block.StartsWith("<%"))
			{
				this.contents = block.Substring(2, block.Length - endOffset).Trim();
				return;
			}

			if (ignoreNewLine)
				block = block.Trim();

            block = block.Replace(@"\", @"\\");
			block = block.Replace(Environment.NewLine, "\\r\\n");
            block = block.Replace(@"""", @"\""");
			if(block.Length > 0)
                this.contents = string.Format("$response.Write(\"{0}\")", block);
		}

		string contents;
		public string Contents
		{
			get
			{
				return this.contents;
			}
		}

		[ThreadStatic]
		static bool ignoreNextNewLine = false;

	}
}
