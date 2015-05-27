using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobaltFrame.Context
{
	/// <summary>
	/// セーブデータを統一的に管理するクラス
	/// Tはセブデータの型
	/// </summary>
    public class DataContext<T>
    {
        private static string _saveFileName;
        private static T _instance;
        private static Func<string, T> _onLoad;
        private static Func<string, T, bool> _onSave;
        private DataContext()
        {
        }

		/// <summary>
		/// セーブデータの本体
		/// </summary>
		/// <value>The data.</value>
        public static T Data
        {
            get
            {
                if (string.IsNullOrEmpty(_saveFileName))
                {
                    throw new NullReferenceException("SaveData can refer after Setup");
                }
                if (_instance == null)
                {
                    throw new NullReferenceException("SavaData can refer after Load");
                }
                return _instance;
            }
        }

		/// <summary>
		/// セーブデータがロードされているか
		/// </summary>
        public static bool IsLoaded { get; private set; }

		/// <summary>
		/// 初期化 最初に呼ぶ必要がある
		/// </summary>
		/// <param name="fileName">セーブデータのファイル名</param>
		/// <param name="onLoad">データロードの式</param>
		/// <param name="onSave">データセーブの式</param>
        public static void Setup(string fileName,Func<string,T> onLoad, Func<string, T, bool> onSave)
        {
            IsLoaded = false;
            _saveFileName = fileName;
            _onLoad = onLoad;
            _onSave = onSave;
            
        }
		/// <summary>
		/// データロード開始
		/// </summary>
		/// <param name="newInstance">初回起動の場合渡される新しいセーブデータのインスタンス</param>
        public static bool Load(T newInstance)
        {
            if (string.IsNullOrEmpty(_saveFileName))
            {
                throw new NullReferenceException("SaveData can load after Setup");
            }
            T instance = _onLoad(_saveFileName);
            if (instance != null)
            {
                _instance = instance;
                IsLoaded = true;
                return true;
            }
            else
            {
                _instance = newInstance;
                return false;
            }
        }
		/// <summary>
		/// データセーブ
		/// </summary>
        public static bool Save()
        {
            if (string.IsNullOrEmpty(_saveFileName))
            {
                throw new NullReferenceException("SaveData can save after Setup");
            }
            return _onSave(_saveFileName,_instance);
        }
		/// <summary>
		/// セーブデータ初期化
		/// </summary>
		/// <param name="instance">新しいセーブデータのインスタンス</param>
        public static void Clear(T instance)
        {
            _instance = instance;
            IsLoaded = false;
        }


    }
}
