using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace IronRubyViewEngine
{
	public class RubyTemplate
	{
		string template;
		List<string> requires = new List<string>();
		
		public RubyTemplate(string templateContents)
		{
			template = templateContents;
		}

		public void AddRequire(string require)
		{
			requires.Add(require);
		}

		/// <summary>
		/// Parses the template and returns 
		/// </summary>
		/// <param name="templateContents"></param>
		/// <returns></returns>
		public string ToScript()
		{
            if (this.template == null) {
                throw new ArgumentNullException("templateContents", "Cannot pass null templateContents to the constructor");
            }
			string contents = this.template;

			StringBuilder builder = new StringBuilder();
			requires.ForEach(delegate(string require) {
				builder.AppendLine(string.Format("require '{0}'", require));
			});

			Regex scriptBlocks = new Regex("<%.*?%>", RegexOptions.Compiled | RegexOptions.Singleline);
			MatchCollection matches = scriptBlocks.Matches(contents);

			int currentIndex = 0;
			int blockBeginIndex = 0;
			foreach (Match match in matches) {
				blockBeginIndex = match.Index;
				RubyScriptBlock block = RubyScriptBlock.Parse(contents.Substring(currentIndex, blockBeginIndex - currentIndex));
                if (!String.IsNullOrEmpty(block.Contents)) {
                    builder.AppendLine(block.Contents);
                }
				
				block = RubyScriptBlock.Parse(match.Value);
				builder.AppendLine(block.Contents);
				currentIndex = match.Index + match.Length;
			}

			if (currentIndex < contents.Length - 1) {
				RubyScriptBlock endBlock = RubyScriptBlock.Parse(contents.Substring(currentIndex));
				builder.Append(endBlock.Contents);
			}

			return builder.ToString().Trim();
		}
	}
}
