using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Motion.Tray
{
    public struct Index
    {
        public int Row;//行
        public int Col;//列
        public bool IsEmpty;
        public Color color;//在工作状态起作用，工位状态；Gray代表初始化，green代表OK,red代表NG,其它代表被屏蔽
        public Index(int r, int c) { Row = r; Col = c; color = Color.Gray;IsEmpty = false; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="r_c">以字符口串"R_C"的形式传递值</param>
        public Index(string r_c)
        {
            try
            {
                string[] value = r_c.Trim().Split(',');
                Row = Convert.ToInt32(value[0]);
                Col = Convert.ToInt32(value[1]);
            }
            catch (Exception ex)
            {
                Row = -1;
                Col = -1;
            }
            color = Color.Gray;
            IsEmpty = false;
        }
        public override string ToString()
        {
            return Row.ToString() + "," + Col.ToString();
        }
        public void SetColor(Color c)
        {
            color = c;
        }
    }
}
