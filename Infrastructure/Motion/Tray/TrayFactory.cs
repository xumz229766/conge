using System;
using System.Collections.Generic;
using System.Toolkit;
using System.Toolkit.Helpers;
namespace Motion.Tray
{
    /// <summary>
    /// 托盘工厂模式
    /// </summary>
    /// <typeparam name="T">泛型:int,uint,long,double,float,short</typeparam>
    public class TrayFactory
    {

        /// <summary>
        /// 标定计算
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool Calibration(ref Tray tray)
        {
            if (tray == null) throw new Exception("托盘不存在！");
            var retR12 = (tray.dic_Index[tray.Data.RowIndex].Row - tray.dic_Index[tray.Data.BaseIndex].Row) != 0;
            var retC12 = (tray.dic_Index[tray.Data.RowIndex].Col - tray.dic_Index[tray.Data.BaseIndex].Col) != 0;
            var retR13 = (tray.dic_Index[tray.Data.ColumnIndex].Row - tray.dic_Index[tray.Data.BaseIndex].Row) != 0;
            var retC13 = (tray.dic_Index[tray.Data.ColumnIndex].Col - tray.dic_Index[tray.Data.BaseIndex].Col) != 0;
            if ((retR12 == retR13) || (retC12 == retC13)) throw new Exception("三点重合，或者三点再同一直线上！");
            if ((retR12 == retC12) || (retR13 == retC13)) throw new Exception("三点无法形成直角坐标系，非有效点！");
            var iRow = 0;
            var iColumn = 0;
            double detaRowX, detaRowY, detaColX, detaColY;
            if (retR12 && !retR13)
            {
                iRow = tray.dic_Index[tray.Data.RowIndex].Row - tray.dic_Index[tray.Data.BaseIndex].Row;
                iColumn = tray.dic_Index[tray.Data.ColumnIndex].Col - tray.dic_Index[tray.Data.BaseIndex].Col;
                detaRowX = tray.Data.RowPosition.X - tray.Data.BasePosition.X;
                detaRowY = tray.Data.RowPosition.Y - tray.Data.BasePosition.Y;
                detaColX = tray.Data.ColumnPosition.X - tray.Data.BasePosition.X;
                detaColY = tray.Data.ColumnPosition.Y - tray.Data.BasePosition.Y;
            }
            else
            {
                iRow = tray.dic_Index[tray.Data.ColumnIndex].Row - tray.dic_Index[tray.Data.BaseIndex].Row;
                iColumn = tray.dic_Index[tray.Data.RowIndex].Col - tray.dic_Index[tray.Data.BaseIndex].Col;
                detaColX = tray.Data.RowPosition.X - tray.Data.BasePosition.X;
                detaColY = tray.Data.RowPosition.Y - tray.Data.BasePosition.Y;
                detaRowX = tray.Data.ColumnPosition.X - tray.Data.BasePosition.X;
                detaRowY = tray.Data.ColumnPosition.Y - tray.Data.BasePosition.Y;
            }
            tray.Data.RowDistance = Math.Abs(detaRowY / iRow);
            tray.Data.ColDistance = Math.Abs(detaColX / iColumn);
            tray.Data.RowColOffset = detaRowX / iRow;
            tray.Data.ColRowOffset = detaColY / iColumn;
            tray.Data.IsCalibration = true;
            return true;
        }
    }
}
