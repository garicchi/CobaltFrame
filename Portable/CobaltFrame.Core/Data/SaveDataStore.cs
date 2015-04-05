using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CobaltFrame.Core.Data
{
    public class SaveDataStore<T>
    {
        private static string _saveFileName;
        private static T _instance;
        private static Func<string, T> _onLoad;
        private static Func<string, T, bool> _onSave;
        private SaveDataStore()
        {
        }

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

        public static bool IsLoaded { get; private set; }

        public static void Setup(string fileName,Func<string,T> onLoad, Func<string, T, bool> onSave)
        {
            IsLoaded = false;
            _saveFileName = fileName;
            _onLoad = onLoad;
            _onSave = onSave;
            
        }

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

        public static bool Save()
        {
            if (string.IsNullOrEmpty(_saveFileName))
            {
                throw new NullReferenceException("SaveData can save after Setup");
            }
            return _onSave(_saveFileName,_instance);
        }

        public static void Clear(T instance)
        {
            _instance = instance;
            IsLoaded = false;
        }


    }
}
