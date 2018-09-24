using System;
using System.IO;
using System.Threading.Tasks;

namespace EDictionary.Core.Utilities
{
	class LogWriter
	{
		#region Singleton

		private static readonly Lazy<LogWriter> instance = new Lazy<LogWriter>(() => new LogWriter());
		public static LogWriter Instance => instance.Value;

		#endregion


		StreamWriter logWriter;

		int fileCount = 0;
		static string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
		string logFilename = string.Empty;
		long LogFileSize = 2097152; // 2Mb

		private string GetLogPath()
		{
			return Path.Combine(logPath, DateTime.Now.ToString("yyyy_MM_dd") + ".Log." + ++fileCount + ".txt");
		}

		public async void WriteLine(string logMessage)
		{
			await Task.Run(() =>
					{
						try
						{
							if (string.IsNullOrEmpty(logFilename))
								logFilename = GetLogPath();

							if (!(Directory.Exists(logPath)))
								Directory.CreateDirectory(logPath);

							while (true)
							{
								if (!File.Exists(logFilename))
								{
									logWriter = new StreamWriter(logFilename);
									break;
								}
								else
								{
									if (LogFileSize == 0)
										LogFileSize = 2097152;

									FileInfo fi = new FileInfo(logFilename);

									if (fi.Length > LogFileSize)
									{
										fileCount++;
										logFilename = GetLogPath();
										continue;
									}
									else
									{
										logWriter = File.AppendText(logFilename);
										break;
									}
								}
							}

							logWriter.WriteLine(DateTime.Now.ToString("g") + ": " + logMessage);
							logWriter.Flush();
							logWriter.Close();
						}
						catch (Exception)
						{

						}
					});
		}

		#region IDisposable Member

		public void Dispose()
		{
			logWriter.Close();
			GC.Collect();
		}

		#endregion
	}
}
