using UnityEngine;
using System;

namespace Nekoslibrary
{
    /// <summary>
    ///  自作計算関数　いろいろあるよ
    ///  <para>困ったらIndexerを見てね</para>
    /// </summary>
    public static class MathN
    {
        /// <summary>
        /// <para>目次　効果はない</para>
        /// <para>Clamp ( _min / _max )</para>
        /// <para>Pow , Abs</para>
        /// <para>Vector3系 (Distance / Normalize)</para>
        /// <para></para>
        /// </summary>
        /// <returns></returns>
        public static string Indexer()
        {
            return null;
        }

        /// <summary>
        /// 円周率
        /// </summary>
        public static float PI = 3.14159f;

        #region Clamp系関数　制限する関数

        /// <summary>
        /// 値の範囲を制限する関数
        /// <para>制限した値を返す</para>
        /// </summary>
        /// <param name="Value">制限する値</param>
        /// <param name="Minimun">下限値</param>
        /// <param name="Maximum">上限値</param>
        /// <returns></returns>
        public static float Clamp(float Value, float Minimun, float Maximum)
        {
            if (Maximum < Value)
            {
                Value = Maximum;
            }
            else if (Value < Minimun)
            {
                Value = Minimun;
            }

            return Value;
        }

        /// <summary>
        /// Clamp関数
        /// <para>下限のみ指定する</para>
        /// </summary>
        /// <param name="Value">制限する値</param>
        /// <param name="Minimun">下限値</param>
        /// <returns></returns>
        public static float Clamp_min(float Value, float Minimun)
        {
            if (Value < Minimun)
            {
                Value = Minimun;
            }

            return Value;
        }

        /// <summary>
        /// Clamp関数
        /// <para>上限のみ指定する</para>
        /// </summary>
        /// <param name="Value">制限する値</param>
        /// <param name="Maximum">上限値</param>
        /// <returns></returns>
        public static float Clamp_max(float Value, float Maximum)
        {
            if (Maximum < Value)
            {
                Value = Maximum;
            }

            return Value;
        }

        #endregion

        /// <summary>
        /// ２乗する関数
        /// </summary>
        /// <param name="Value">２乗する値</param>
        /// <returns></returns>
        public static float Pow(float Value)
        {
            return Value * Value;
        }

        /// <summary>
        /// 絶対値を取得する関数
        /// </summary>
        /// <param name="Value">取得する値</param>
        /// <returns></returns>
        public static float Abs(float Value)
        {
            if (Value < 0)
            {
                Value = Value * -1f;
            }
            return Value;
        }

        /// <summary>
        /// ２つの座標の直線距離を取得する関数
        /// </summary>
        /// <param name="startVector">始点の座標</param>
        /// <param name="endVector">目標の座標</param>
        /// <returns></returns>
        public static float Vector3_Distance(Vector3 startVector, Vector3 endVector)
        {
            float value = Pow(endVector.x - startVector.x) + Pow((endVector.y - startVector.y));
            value = (float)Math.Sqrt(value + Pow((endVector.z - startVector.z)));
            return value;
        }

        /// <summary>
        /// ２つの座標間のベクトルのノーマライズした値を取得する関数
        /// </summary>
        /// <param name="startVector">始点の座標</param>
        /// <param name="endVector">目標の座標</param>
        public static Vector3 Vector3_Normalize(Vector3 startVector, Vector3 endVector)
        {
            return (endVector - startVector).normalized;
        }

        /// <summary>
        /// Vector3をXY軸のみの値に変化させる
        /// </summary>
        /// <param name="Vector"></param>
        /// <returns></returns>
        public static Vector3 Vector3To2_XY(Vector3 Vector)
        {
            Vector3 tmp = new Vector3(Vector.x, Vector.y, 0f);
            return tmp;
        }

        /// <summary>
        /// Vector3をXZ軸のみの値に変化させる
        /// </summary>
        /// <param name="Vector"></param>
        /// <returns></returns>
        public static Vector3 Vector3To2_XZ(Vector3 Vector)
        {
            Vector3 tmp = new Vector3(Vector.x, 0f, Vector.z);
            return tmp;
        }

        /// <summary>
        /// Vector3をYZ軸のみの値に変化させる
        /// </summary>
        /// <param name="Vector"></param>
        /// <returns></returns>
        public static Vector3 Vector3To2_YZ(Vector3 Vector)
        {
            Vector3 tmp = new Vector3(0f, Vector.y, Vector.z);
            return tmp;
        }

        /// <summary>
        /// 単位変換　度数法から弧度法に
        /// </summary>
        /// <param name="degree">度数法の角度</param>
        /// <returns></returns>
        public static float Chenge_DegToRad(float degree)
        {
            return (degree / 180) * PI;
        }

        /// <summary>
        /// 単位変換　弧度法から度数法に
        /// </summary>
        /// <param name="radian">弧度法の角度</param>
        /// <returns></returns>
        public static float Chenge_RadToDeg(float radian)
        {
            return (radian / PI) * 180;
        }

    }
}
