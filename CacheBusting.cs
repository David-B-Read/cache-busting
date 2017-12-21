using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace WebApplication1
{
	public class CacheBusting
	{
		public static string Url(string rootRelativePath)
		{
			if (HttpRuntime.Cache[rootRelativePath] == null)
			{
				string absolute = HostingEnvironment.MapPath("~" + rootRelativePath);

				DateTime date = File.GetLastWriteTime(absolute);
				
				var result = rootRelativePath + "?" + date.Ticks;

				HttpRuntime.Cache.Insert(rootRelativePath, result, new CacheDependency(absolute));
			}

			return HttpRuntime.Cache[rootRelativePath] as string;			
		}
	}
}