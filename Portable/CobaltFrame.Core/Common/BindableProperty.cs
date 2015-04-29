using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Common
{
    public class BindableProperty<T>
    {
        private List<Action<T>> _bindExpressions;
        private T _value;

        public T Value
        {
            get { return _value; }
            set { _value = value; OnUpdate(value); }
        }
        public BindableProperty()
        {
            this._bindExpressions = new List<Action<T>>();
        }
        public void AddBind(Action<T> updateExpression)
        {
            this._bindExpressions.Add(updateExpression);
        }

        public void ClearBind()
        {
            this._bindExpressions.Clear();
        }

        private void OnUpdate(T newValue)
        {
            foreach (var ex in this._bindExpressions)
            {
                ex(newValue);
            }
        }
    }
}
