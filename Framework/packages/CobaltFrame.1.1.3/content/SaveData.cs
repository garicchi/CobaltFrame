using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame
{
    /// <summary>
    /// セーブデータ
    /// このクラスのメンバーを変更することで自動保存、自動復元される
    /// 使い方はDataContext<SaveData>.Data
    /// </summary>
    public class SaveData
    {
        //ゲームのスコア
        public int Score { get; set; }
    }
}
