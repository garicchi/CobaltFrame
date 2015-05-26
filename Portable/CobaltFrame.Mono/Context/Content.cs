using System;

namespace CobaltFrame.Mono
{
    /// <summary>
    /// リソースへの参照を保持するクラス
    /// </summary>
	public class Content
	{
		public Content ()
		{
		}
        /// <summary>
        /// リソースの名前
        /// </summary>
		public string Name{get;set;}
		/// <summary>
		/// リソースへの参照
		/// </summary>
        public object Refer{ get; set; }
        /// <summary>
        /// ロードされたときのデリゲート
        /// </summary>
		public Func<object> OnLoad{ get; set; }
        /// <summary>
        /// パイプラインを使うリソースかどうか
        /// </summary>
		public bool IsUsedPipeline{get;set;}
	}
}

