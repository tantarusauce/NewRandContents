using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandContents
{
    //データクラス
    class data
    {
        //ユーザー名保持
        public string user;
        //ユーザー消費ポイント保持
        public int pt, ptc;
        //抽選データ保持
        public int[] dt = new int[100];
        //抽選回数保持
        public int rouletteCount;
    }
}
