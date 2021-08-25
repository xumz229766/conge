using System;
using System.Collections.Generic;
using System.Drawing;
using System.Toolkit;
using System.Linq;
namespace Motion.Tray
{
    /// <summary>
    /// 托盘类，包含托盘类型，计数，移位，坐标等
    /// </summary>
    public class Tray
    {
        [NonSerialized]
        public Action updateColor;//更新点位时的委托
        /// <summary>
        /// 有效排序位置
        /// </summary>
        [NonSerialized]
        public Dictionary<int, Index> dic_Index = new Dictionary<int, Index>();
        public TrayType Data { get; set; }
        /// <summary>
        /// 建立托盘
        /// </summary>
        /// <param name="id">托盘序号</param>
        /// <param name="name">托盘命名</param>
        /// <param name="Row">托盘行数</param>
        /// <param name="Column">托盘列数</param>
        public Tray()
        {

        }
        /// <summary>
        /// 判断数据是否为空
        /// </summary>
        public bool IsTrayTypeNull { get { return Data == null; } }
        public void LoadTrayData(TrayType data) {this.Data = data;}
        public TrayType GetTrayData() { return Data; }
        public void InitTrayValue(Color c)
        {
            for (int i = Data.StartPos; i <= Data.EndPos; i++)
            {
                Index index = dic_Index[i];
                index.color = c;
                dic_Index[i] = index;
                //SetNumColor(i, c);
            }
            Data.CurrentPos = Data.StartPos;
            updateColor?.Invoke();
        }

        /// <summary>
        /// 找出盘中有效点，按起始位置和方向排列点位
        /// </summary>
        /// <param name="start">起始位置</param>
        /// <param name="direct">方向</param>
        /// <param name="lineType">换行方式</param>
        public void SortTray()
        {
            dic_Index.Clear();
       
            int i = 1;
            switch (Data.StartPose)
            {
                #region"左上角"
                case EStartPos.左上角:
                    if (Data.Direction == EIndexDirect.行)
                    {
                        for (int r = 0; r < Data.Row; r++)
                        {
                            //Z型换行
                            if (r % 2 == 1 && Data.ChangeLineType == EChangeLine.S)
                            {
                                for (int c = Data.Column - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                 

                                    }
                                }
                            }
                            else
                            {
                                for (int c = 0; c < Data.Column; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                     
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int c = 0; c < Data.Column; c++)
                        {
                            if (c % 2 == 1 && Data.ChangeLineType == EChangeLine.S)
                            {
                                for (int r = Data.Row - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                      
                                    }

                                }
                            }
                            else
                            {
                                for (int r = 0; r < Data.Row; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;                                   
                                    }

                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"左下角"
                case EStartPos.左下角:
                    if (Data.Direction == EIndexDirect.行)
                    {
                        for (int r = Data.Row - 1; r >= 0; r--)
                        {
                            if (((Data.Row % 2 == 1 && r % 2 == 1) || (Data.Row % 2 == 0 && r % 2 == 0))
                                 && Data.ChangeLineType == EChangeLine.S)
                            {
                                for (int c = Data.Column - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                            else
                            {
                                for (int c = 0; c < Data.Column; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        for (int c = 0; c < Data.Column; c++)
                        {
                            if (c % 2 == 1 && Data.ChangeLineType == EChangeLine.S)
                            {
                                for (int r = 0; r < Data.Row; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = Data.Row - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"右上角"
                case EStartPos.右上角:
                    if (Data.Direction == EIndexDirect.行)
                    {
                        for (int r = 0; r < Data.Row; r++)
                        {
                            if (r % 2 == 1 && Data.ChangeLineType == EChangeLine.S)
                            {
                                for (int c = 0; c < Data.Column; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int c = Data.Column - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int c = Data.Column - 1; c >= 0; c--)
                        {
                            if (((Data.Column %2==0 && c % 2 == 0)|| (Data.Column % 2 == 1 && c % 2 == 1))
                                && Data.ChangeLineType == EChangeLine.S)
                            {
                                for (int r = Data.Row - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = 0; r < Data.Row; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"右下角"
                case EStartPos.右下角:
                    if (Data.Direction == EIndexDirect.行)
                    {
                        for (int r = Data.Row - 1; r >= 0; r--)
                        {
                            if (((Data.Row % 2 ==1 && r % 2 == 1)|| (Data.Row % 2 == 0 && r % 2 == 0))
                                && Data.ChangeLineType == EChangeLine.S)
                            {
                                for (int c = 0; c < Data.Column; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int c = Data.Column - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int c = Data.Column - 1; c >= 0; c--)
                        {
                            if (((Data.Column %2==0 && c % 2 == 0)|| (Data.Column % 2 == 1 && c % 2 == 1))
                                && Data.ChangeLineType == EChangeLine.S)
                            {
                                for (int r = 0; r < Data.Row; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = Data.Row - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                    #endregion
            }
            Data.StartPos = 1;
            Data.EndPos = dic_Index.Count;
        }
        /// <summary>
        /// 从指定的索引位置开始查找有效穴号，并返回该穴号位置
        /// </summary>
        /// <param name="_pos">开始查找的位置</param>
        /// <returns>返回有效穴号位置,如果返回-1则代表没有找到</returns>
        public int FindPos(Index _pos)
        {
            int result = -1;
            foreach (int i in dic_Index.Keys)
            {
                if (dic_Index[i].Equals(_pos))
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 往盘中添加屏蔽位置
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        public void AddEmptyPos(int row, int col)
        {
            Index pos = new Index(row, col);
            if (!IsExistEmpty(pos))
            {
                Data.ListEmpty.Add(pos);
            }
        }
        /// <summary>
        /// 移除屏蔽位置
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        public void RemoveEmptyPos(int row, int col)
        {
            var r_c= row.ToString() + "," + col.ToString();
            Index pos = new Index(r_c);
            if (IsExistEmpty(pos))
            {
                var index = Data.ListEmpty.FirstOrDefault(o => o.Row == row & o.Col == col);
                Data.ListEmpty.Remove(index);
            }
        }
        //判断屏蔽位置是否存在
        public bool IsExistEmpty(Index _pos)
        {
            foreach(var list in Data.ListEmpty)
            {
               if(list.ToString() == _pos.ToString())
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 获取所有屏蔽点的字符串形式
        /// </summary>
        /// <returns>以"r_c,r_c,...,r_c"形式返回字符串</returns>
        public string GetStringEmpty()
        {
            string strReturn = "";
            int iLen = Data.ListEmpty.Count;
            for (int i = 0; i < iLen; i++)
            {
                if (i == iLen - 1)
                {
                    strReturn += Data.ListEmpty[i].ToString();
                    break;
                }
                strReturn += Data.ListEmpty[i].ToString() + ",";
            }
            return strReturn;
        }
        /// <summary>
        /// 设置指定序号控件的背景颜色
        /// </summary>
        /// <param name="num">穴号</param>
        /// <param name="bColor">颜色</param>
        public void SetNumColor(int num, Color bColor)
        {
            Index index = dic_Index[num];
            index.color = bColor;
            dic_Index[num] = index;
            updateColor?.Invoke();
        }
        /// <summary>
        /// 将某一行的颜色都设置为指定的颜色
        /// </summary>
        /// <param name="num">穴号</param>
        /// <param name="bColor">颜色</param>
        public void SetRowColor(int num, Color bColor)
        {
            Index index = dic_Index[num];
            for(var i=0;i < Data.Row;i++)
            {
                var temp = dic_Index.FirstOrDefault(o => o.Value.Row == i & o.Value.Col == index.Col);
                var value = temp.Value;
                value.color = bColor;
                dic_Index[temp.Key] = value;
            }
            updateColor?.Invoke();
        }
        /// <summary>
        /// 将某一列的颜色都设置为指定的颜色
        /// </summary>
        /// <param name="num">穴号</param>
        /// <param name="bColor">颜色</param>
        public void SetColumnColor(int num, Color bColor)
        {
            Index index = dic_Index[num];
            for (var i = 0; i < Data.Column; i++)
            {
                var temp = dic_Index.FirstOrDefault(o => o.Value.Row == index.Row & o.Value.Col == i);
                var value = temp.Value;
                value.color = bColor;
                dic_Index[temp.Key] = value;
            }
            updateColor?.Invoke();
        }
        /// <summary>
        /// 托盘颜色复位
        /// </summary>
        /// <param name="bColor"></param>
        public void ResetTrayColor(Color bColor)
        {
            var keylist = new List<int>();
            foreach (var key in dic_Index.Keys)
            {
                keylist.Add(key);
            }
            foreach (var key in keylist)
            {
                var index = dic_Index[key];
                index.color = bColor;
                dic_Index[key] = index;
            }
            updateColor?.Invoke();
        }
        /// <summary>
        /// 设置托盘起始结束位
        /// </summary>
        /// <param name="_startPos">起始位置</param>
        /// <param name="_endPos">结束位置</param>
        /// <param name="fillColor">起始位到结束位的显示颜色</param>
        /// <param name="fillColor2">无效位置的显示颜色</param>
        public void SetStartEndPos(int _startPos, int _endPos, Color fillColor, Color fillColor2)
        {
            if (_startPos > _endPos) return;
            Data.CurrentPos = _startPos;
            Data.StartPos = _startPos;
            Data.EndPos = _endPos;
            for (int i = 1; i < _startPos; i++)
            {
                Index index = dic_Index[i];
                index.color = fillColor2;
                dic_Index[i] = index;
            }
            int count = dic_Index.Count;
            for (int i = _endPos; i < count + 1; i++)
            {
                Index index = dic_Index[i];
                index.color = fillColor2;
                dic_Index[i] = index;
            }
            for (int i = _startPos; i <= _endPos; i++)
            {
                Index index = dic_Index[i];
                index.color = fillColor;
                dic_Index[i] = index;
            }
            updateColor?.Invoke();
        }
        /// <summary>
        /// 获取当前点数据位置
        /// </summary>
        /// <param name="point">参考点坐标</param>
        /// <param name="Trayindex">托盘索引</param>
        /// <param name="quadrant">机械轴象限</param>
        /// <returns></returns>
        public Point3D<double> GetPosition(Point3D<double> point, int Trayindex, Quadrant quadrant)
        {
            var pos = new Point3D<double>();
            Index Traypos = dic_Index[Trayindex];
            Index TrayBasePos = dic_Index[1];
            bool Xdir = false, Ydir = false;
            switch (quadrant)
            {
                case Quadrant.First:
                    Xdir = true;
                    Ydir = true;
                    break;
                case Quadrant.Second:
                    Xdir = false;
                    Ydir = true;
                    break;
                case Quadrant.Third:
                    Xdir = false;
                    Ydir = false;
                    break;
                case Quadrant.Fourth:
                    Xdir = true;
                    Ydir = false;
                    break;
            }
            if (point.X == 0 && point.Y == 0)
            {
                pos.X = 0;
                pos.Y = 0;
                pos.Z = 0;
                return pos;
            }
            pos.X = point.X + ((Traypos.Row - TrayBasePos.Row) * Data.RowColOffset
                + (Traypos.Col - TrayBasePos.Col) * Data.ColDistance) * (Xdir ? 1 : -1);
            pos.Y = point.Y + ((Traypos.Row - TrayBasePos.Row) * Data.RowDistance
                + (Traypos.Col - TrayBasePos.Col) * Data.ColRowOffset) * (Ydir ? 1 : -1);
            pos.Z = point.Z;
            return pos;
        }
        /// <summary>
        /// 获取当前点数据位置
        /// </summary>
        /// <param name="point">参考点坐标</param>
        /// <param name="Trayindex">托盘索引</param>
        /// <param name="quadrant">机械轴象限</param>
        /// <returns></returns>
        public Point<double> GetPosition(Point<double> point, int Trayindex, Quadrant quadrant)
        {
            var pos = new Point<double>();
            Index Traypos = dic_Index[Trayindex];
            Index TrayBasePos = dic_Index[1];
            bool Xdir=false, Ydir=false;
            switch (quadrant)
            {
                case Quadrant.First:
                    Xdir = true;
                    Ydir = true;
                    break;
                case Quadrant.Second:
                    Xdir = false;
                    Ydir = true;
                    break;
                case Quadrant.Third:
                    Xdir = true;
                    Ydir = false;
                    break;
                case Quadrant.Fourth:
                    Xdir = false;
                    Ydir = false;
                    break;
            }
            if (point.X == 0 && point.Y == 0)
            {
                pos.X = 0;
                pos.Y = 0;
                return pos;
            }
            pos.X = point.X + ((Traypos.Row - TrayBasePos.Row) * Data.RowColOffset
                + (Traypos.Col - TrayBasePos.Col) * Data.ColDistance) * (Xdir ? 1 : -1);
            pos.Y = point.Y + ((Traypos.Col - TrayBasePos.Col) * Data.ColRowOffset
                + (Traypos.Row - TrayBasePos.Row) * Data.RowDistance) * (Ydir ? 1 : -1);
            return pos;
        }
        private struct Dir
        {
            public bool Xdir;
            public bool Ydir;
        }
    }
}
