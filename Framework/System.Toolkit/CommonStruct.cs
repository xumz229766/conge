﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Toolkit
{
    /// <summary>
    /// 用户等级
    /// </summary>
    public enum UserLevel
    {
        None=0x0,//空
        Operator=0x1,//操作员
        Technician=0x3,//技术员
        Admin=0x7//管理员
    };
    public static class UserLevelExtensions
    {
        public static bool RightsJudge(this UserLevel handle, UserLevel userLevel) => (userLevel & handle) == userLevel;
    }
    public struct ArcParam<T>
    {
        public T X;
        public T Y;
        public T R;
        public int DIR;
    }
    public struct Point3D<T>
    {
        public T X;
        public T Y;
        public T Z;
        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString() + "," + Z.ToString();
        }
        public static Point3D<T> Parse(string str)
        {
            string[] strValue = str.Split(',');
            var point3D = new Point3D<T>();
            point3D.X = (T)Convert.ChangeType(strValue[0], typeof(T));
            point3D.Y = (T)Convert.ChangeType(strValue[1], typeof(T));
            point3D.Z = (T)Convert.ChangeType(strValue[2], typeof(T));
            return point3D;
        }
    }
    public struct Point3Dxzc<T>
    {
        public T X;
        public T Z;
        public T C;
        public override string ToString()
        {
            return X.ToString() + "," + Z.ToString() + "," + C.ToString();
        }
        public static Point3Dxzc<T> Parse(string str)
        {
            string[] strValue = str.Split(',');
            var point3D = new Point3Dxzc<T>();
            point3D.X = (T)Convert.ChangeType(strValue[0], typeof(T));
            point3D.Z = (T)Convert.ChangeType(strValue[1], typeof(T));
            point3D.C = (T)Convert.ChangeType(strValue[2], typeof(T));
            return point3D;
        }
    }
    public struct Point5Dxzc<T>
    {
        public T X;
        public T Z;
        public T C;
        public T Z2;
        public T C2;
        public override string ToString()
        {
            return X.ToString() + "," + Z.ToString() + "," + C.ToString() + "," + Z2.ToString() + "," + C2.ToString();
        }
        public static Point5Dxzc<T> Parse(string str)
        {
            string[] strValue = str.Split(',');
            var point5D = new Point5Dxzc<T>();
            point5D.X = (T)Convert.ChangeType(strValue[0], typeof(T));
            point5D.Z = (T)Convert.ChangeType(strValue[1], typeof(T));
            point5D.C = (T)Convert.ChangeType(strValue[2], typeof(T));
            point5D.Z2 = (T)Convert.ChangeType(strValue[3], typeof(T));
            point5D.C2 = (T)Convert.ChangeType(strValue[4], typeof(T));
            return point5D;
        }
    }
    public struct Point<T>
    {
        public T X;
        public T Y;
        public override string ToString() => X.ToString() + "," + Y.ToString();
        public static Point<T> Parse(string str)
        {
            var pos = new Point<T>();
            string[] strValue = str.Split(',');
            pos.X = (T)Convert.ChangeType(strValue[0], typeof(T));
            pos.Y = (T)Convert.ChangeType(strValue[1], typeof(T));
            return pos;
        }
    }
    public struct Pointxz<T>
    {
        public T X;
        public T Z;
        public override string ToString() => X.ToString() + "," + Z.ToString();
        public static Pointxz<T> Parse(string str)
        {
            var pos = new Pointxz<T>();
            string[] strValue = str.Split(',');
            pos.X = (T)Convert.ChangeType(strValue[0], typeof(T));
            pos.Z = (T)Convert.ChangeType(strValue[1], typeof(T));
            return pos;
        }
    }
    public struct Pointyz<T>
    {
        public T Y;
        public T Z;
        public override string ToString() => Y.ToString() + "," + Z.ToString();
        public static Pointyz<T> Parse(string str)
        {
            var pos = new Pointyz<T>();
            string[] strValue = str.Split(',');
            pos.Y = (T)Convert.ChangeType(strValue[0], typeof(T));
            pos.Z = (T)Convert.ChangeType(strValue[1], typeof(T));
            return pos;
        }
    }

    public struct Pointxmy<T>
    {
        public T X;
        public T M;
        public T Y;
        public override string ToString() => X.ToString() + "," + M.ToString() + "," + Y.ToString();
        public static Pointxmy<T> Parse(string str)
        {
            var pos = new Pointxmy<T>();
            string[] strValue = str.Split(',');
            pos.X = (T)Convert.ChangeType(strValue[0], typeof(T));
            pos.M = (T)Convert.ChangeType(strValue[0], typeof(T));
            pos.Y = (T)Convert.ChangeType(strValue[1], typeof(T));
            return pos;
        }
    }
    public struct Pointxm<T>
    {
        public T X;
        public T M;
        public override string ToString() => X.ToString() + "," + M.ToString();
        public static Pointxm<T> Parse(string str)
        {
            var pos = new Pointxm<T>();
            string[] strValue = str.Split(',');
            pos.X = (T)Convert.ChangeType(strValue[0], typeof(T));
            pos.M = (T)Convert.ChangeType(strValue[0], typeof(T));
            return pos;
        }
    }
    public enum ImageMirror
    {
        none,
        row,
        column,
        diagonal
    }
}