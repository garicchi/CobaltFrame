using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame
{
    /// <summary>
    /// セーブデータ
    /// このクラスのメンバーを変更することで自動保存できる
    /// 使い方はDataContext<SaveData>.Data
    /// </summary>
    public class SaveData
    {
        public int Score { get; set; }
    }
}
