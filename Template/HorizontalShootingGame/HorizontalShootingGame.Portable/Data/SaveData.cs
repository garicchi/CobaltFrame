using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Data
{
    public class SaveData
    {
		public SaveData()
		{
			PreviousScore = 0;
		}
		public int PreviousScore{get;set;}
    }
}
