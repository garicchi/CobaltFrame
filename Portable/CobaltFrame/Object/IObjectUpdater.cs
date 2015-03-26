using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public interface IObjectUpdater
    {
        void AddObject(ObjectUpdater obj);

        void RemoveObject(ObjectUpdater obj);

        bool HasObject(ObjectUpdater obj);
    }
}
