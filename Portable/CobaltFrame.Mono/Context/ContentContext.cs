using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.Linq;

namespace CobaltFrame.Mono
{
	public static class ContentContext
	{
		private static List<Content> _contentList;
		private static ContentManager _manager;

		public static void Setup(ContentManager manager)
		{
			_manager = manager;
			_contentList = new List<Content> ();
		}

		public static T Load<T>(string name)
		{
			if (_contentList.Any (q => q.Name == name)) {
				var content = _contentList.Where (q => q.Name == name).First ();
			    return (T)content.Refer;
			} else {
				var content = new Content ();
				content.Name = name;
				content.IsUsedPipeline = true;
				content.OnLoad = () => 
				{
					return _manager.Load<T> (content.Name);
				};
				var c = (T)content.OnLoad ();
				content.Refer = c;
				_contentList.Add (content);
				return c;
			}
		}

		public static T LoadWithoutManager<T>(string name,Func<object> onload)
		{
			if (_contentList.Any (q => q.Name == name)) {
				var content = _contentList.Where (q => q.Name == name).First ();
				return (T)content.Refer;
			} else {
				var content = new Content ();
				content.Name = name;
				content.IsUsedPipeline = true;
				content.OnLoad = onload;
				var c = (T)content.OnLoad ();
				content.Refer = c;
				_contentList.Add (content);
				return c;
			}
		}

		public static void UnloadAll()
		{
			_manager.Unload ();
			_contentList.Clear ();
		}
	}
}

