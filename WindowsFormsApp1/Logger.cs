using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;         //	For Mutex.
using System.IO;

namespace WindowsFormsApp1
{
	public interface ILogger
	{
		void writeEntry( List<string> stringList );
		void writeEntry( string text );
	}

	public abstract class BaseLogger
	{
		protected static Mutex mutex = new Mutex();

		protected abstract void LogText( string text );
	}


	/// <summary>
	/// To have a console and windows at the same time:
	/// Then: Project Properties -> Application -> Output Type -> Console Application 
	/// Then can have Console and Forms running together.
	/// </summary>
	public class ConsoleLogger : BaseLogger, ILogger
	{
		public void writeEntry( List<string> stringList )
		{
			mutex.WaitOne();
			{
				foreach( string text in stringList )
				{
					LogText( text );
				}
			}
			mutex.ReleaseMutex();
		}

		public void writeEntry( string text )
		{
			mutex.WaitOne();
			{
				LogText( text );
			}
			mutex.ReleaseMutex();
		}
		protected override void LogText( string text )
		{
			string logText;
			string timeStamp = DateTime.Now.ToString();

			logText = timeStamp + " " + text;
			Console.WriteLine( logText );
		}
	}

	class FileLogger : BaseLogger, ILogger
	{
		private StreamWriter streamWriter;			//	Log file.

		public FileLogger( string fileName )
		{
			bool isAppend;
			isAppend = true;
			streamWriter = new StreamWriter( fileName, isAppend );
		}

		public void writeEntry( List<string> stringList )
		{
			mutex.WaitOne();
			{
				foreach( string text in stringList )
				{
					LogText( text );
				}
				streamWriter.Flush();
			}
			mutex.ReleaseMutex();
		}

		public void writeEntry( string text )
		{
			mutex.WaitOne();
			{
				LogText( text );
				streamWriter.Flush();
			}
			mutex.ReleaseMutex();
		}

		protected override void LogText( string text )
		{
			string logText;
			string timeStamp = DateTime.Now.ToString();

			logText = timeStamp + " " + text;
			streamWriter.WriteLine( logText );
		}
	}
}
