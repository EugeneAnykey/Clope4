using System;
using System.IO;

namespace TestingHelper
{
	public class FileBroker : IFileBroker
	{
		public string DirectoryPath { get; }

		public FileBroker(string dirPath)
		{
			DirectoryPath = dirPath ?? throw new ArgumentNullException();
		}



		public string GetFilePath(DelimitedFile file)
		{
			if (file == null)
				throw new ArgumentNullException();

			return Path.Combine(DirectoryPath, file.Filename);
		}
	}
}
