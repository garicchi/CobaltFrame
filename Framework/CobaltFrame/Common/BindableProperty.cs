using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
	/// <summary>
	/// 更新式を指定して値が更新された時にバインドしたプロパティの値も一緒に更新できるプロパティ
	/// </summary>
    public class BindableProperty<T>
    {
        private Dictionary<string,Action<T>> _bindExpressions;
        private T _value;

        public T Value
        {
            get { return _value; }
            set { _value = value; OnUpdate(value); }
        }
        public BindableProperty()
        {
            this._bindExpressions = new Dictionary<string,Action<T>>();
        }

		/// <summary>
		/// バインド開始
		/// </summary>
		/// <param name="key">一意に指定するキー</param>
		/// <param name="updateExpression">更新式</param>
        public void Bind(string key,Action<T> updateExpression)
        {
            this._bindExpressions.Add(key,updateExpression);
        }
		/// <summary>
		/// バインドを解除
		/// </summary>
		/// <param name="key">Key.</param>
        public void UnBind(string key)
        {
            this._bindExpressions.Remove(key);
        }
		/// <summary>
		/// バインドをすべて解除する
		/// </summary>
        public void ClearBind()
        {
            this._bindExpressions.Clear();
        }

		/// <summary>
		/// 値更新時
		/// </summary>
		/// <param name="newValue">新しい値</param>
        private void OnUpdate(T newValue)
        {
            foreach (var ex in this._bindExpressions)
            {
                ex.Value(newValue);
            }
        }
    }
}
