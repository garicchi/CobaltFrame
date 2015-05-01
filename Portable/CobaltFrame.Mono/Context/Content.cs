using System;

namespace CobaltFrame.Mono
{
	public class Content
	{
		public Content ()
		{
		}

		public string Name{get;set;}
		public object Refer{ get; set; }

		public Func<object> OnLoad{ get; set; }

		public bool IsUsedPipeline{get;set;}
	}
}

