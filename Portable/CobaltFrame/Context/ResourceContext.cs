using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using CobaltFrame.Common;

namespace CobaltFrame.Context
{
    /// <summary>
    /// ゲームリソースを統括的に扱うクラス
    /// </summary>
	public static class ResourceContext
	{
        /// <summary>
        /// リソースのリスト
        /// </summary>
		private static List<Resource> _contentList;
        /// <summary>
        /// MonoGameのContentManager
        /// </summary>
		private static ContentManager _manager;

        /// <summary>
        /// 初回時に必ず呼ぶ
        /// </summary>
        /// <param name="manager"></param>
		public static void Setup(ContentManager manager)
		{
			_manager = manager;
			_contentList = new List<Resource> ();
		}

        /// <summary>
        /// リソースをロードする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
		public static T Load<T>(string name)
		{
			if (_contentList.Any (q => q.Name == name)) {
				var content = _contentList.Where (q => q.Name == name).First ();
			    return (T)content.Refer;
			} else {
				var content = new Resource ();
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
				var content = new Resource ();
				content.Name = name;
				content.IsUsedPipeline = true;
				content.OnLoad = onload;
				var c = (T)onload ();
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

