using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Backend.DataContexts.EFCore.Options
{
	public class DBOptions
	{
		public const string SectionKey = nameof(DBOptions);
		public string ConnectionString { get; set; }
	}
}
