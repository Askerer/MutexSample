using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexSample
{
	class Program
	{
		static void Main(string[] args)
		{

			bool is_createdNew1;
			bool is_createdNew2;
			Mutex mu1 = null;
			Mutex mu2 = null;

			try
			{
				#region 檢查程式是否重複執行

				// 第一關：在同目錄執行相同程式的情況下不允許重複執行
				string mutexName1 = Process.GetCurrentProcess().MainModule.FileName
							.Replace(Path.DirectorySeparatorChar, '_');
				mu1 = new Mutex(true, "Global\\" + mutexName1, out is_createdNew1);
				if (!is_createdNew1)
					return;

				// 第二關：在完全相同的傳入參數下不允許重複執行，避免數據重複計算
				string mutexName2 = "Args_" + String.Join("_", args)
										.Replace(Path.DirectorySeparatorChar, '_');
				mu2 = new Mutex(true, "Global\\" + mutexName2, out is_createdNew2);
				if (!is_createdNew2)
					return;

				#endregion

				Console.ReadKey();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
	}
}
