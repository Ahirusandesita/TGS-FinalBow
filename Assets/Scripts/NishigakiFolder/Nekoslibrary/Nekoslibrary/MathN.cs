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
        /// <para>PI</para>
        /// <para>Art (Pow)</para>
        /// <para>Mod (Abs ,Red)</para>
        /// <para>Clamp (Both ,Min ,Max)</para>
        /// <para>Vector (Distance ,Normalize)</para>
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

        #region Sqrt用変数

        static readonly float[] _SqrtValue = {  1E19f ,  1E18f ,  1E17f ,  1E16f ,  1E15f ,  //　０～　４
                                                1E14f ,  1E13f ,  1E12f ,  1E11f ,  1E10f ,  //　５～　９
                                                 1E9f ,   1E8f ,   1E7f ,   1E6f ,   1E5f ,  //１０～１４
                                                 1E4f ,   1E3f ,   1E2f ,   1E1f ,   1E0f ,  //１５～１９
                                                1E-1f ,  1E-2f ,  1E-3f ,  1E-4f ,  1E-5f ,  //２０～２４
                                                1E-6f ,  1E-7f ,  1E-8f ,  1E-9f , 1E-10f ,  //２５～２９
                                               1E-11f , 1E-12f , 1E-13f , 1E-14f , 1E-15f ,  //３０～３４
                                               1E-16f , 1E-17f , 1E-18f , 1E-19f , 1E-20f ,  //３５～３９
                                               1E-21f , 1E-22f , 1E-23f , 1E-24f , 1E-25f ,  //４０～４４
                                               1E-26f , 1E-27f , 1E-28f , 1E-29f , 1E-30f ,  //４５～４９
                                               1E-31f , 1E-32f , 1E-33f , 1E-34f , 1E-35f }; //５０～５４

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // 未満
        static readonly float[] _comparisonInteger = {   1E2f,   1E4f,   1E6f,   1E8f,  1E10f,  1E12f,  1E14f,  1E16f,  1E18f,  1E20f,  1E22f,  1E24f,  1E26f,  1E28f,  1E30f,  1E32f,  1E34f,  1E36f,  1E38f,  1f/0f}; // ０～１９

        static readonly   int[] _maxValueInteger   = {     19,     18,    17,     16,      15,     14,     13,     12,     11,     10,      9,      8,      7,      6,      5,      4,      3,      2,      1,      0}; // ０～１９

        static readonly float[] _compHalfInteger   = {  25E0f,  25E2f,  25E4f,  25E5f,  25E8f, 25E10f, 25E12f, 25E14f, 25E16f, 25E18f, 25E20f, 25E22f, 25E24f, 25E26f, 25E28f, 25E30f, 25E32f, 25E34f, 25E36f,  1f/0f}; // ０～１９

        static readonly float[] _halfValueInteger  = {   5E0f,   5E1f,   5E2f,   5E3f,   5E4f,   5E5f,   5E6f,   5E7f,   5E8f,   5E9f,  5E10f,  5E11f,  5E12f,  5E13f,  5E14f,  5E15f,  5E16f,  5E17f,  5E18f,  1f/0f}; // ０～１９

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // 以下
        static readonly float[] _comparisonDecimal = {  1E-2f,  1E-4f,  1E-6f,  1E-8f, 1E-10f, 1E-12f, 1E-14f, 1E-16f, 1E-18f, 1E-20f, 1E-22f, 1E-24f, 1E-26f, 1E-28f, 1E-30f, 1E-32f, 1E-34f, 1E-36f, 1E-38f, 1E-40f, 1E-42f, 1E-44f, 1E-46f,     0f}; //　０～２３

        static readonly   int[] _maxValueDecimal   = {     20,     21,     22,     23,     24,     25,     26,     27,     28,     29,     30,     31,     32,     33,     34,     35,     36,     37,     38,     39,     40,     41,     42,     43}; //　０～２３

        static readonly float[] _compHalfDecimal   = { 25E-2f, 25E-4f, 25E-6f, 25E-8f,25E-10f,25E-12f,25E-14f,25E-16f,25E-18f,25E-20f,25E-22f,25E-24f,25E-26f,25E-28f,25E-30f,25E-32f,25E-34f,25E-36f,25E-38f,25E-40f,25E-42f,25E-44f,25E-46f,     0f}; //　０～２３

        static readonly float[] _halfValueDecimal  = {  5E-1f,  5E-2f,  5E-3f,  5E-4f,  5E-5f,  5E-6f,  5E-7f,  5E-8f,  5E-9f, 5E-10f, 5E-11f, 5E-12f, 5E-13f, 5E-14f, 5E-15f, 5E-16f, 5E-17f, 5E-18f, 5E-19f, 5E-20f, 5E-21f, 5E-22f, 5E-23f,     0f}; //　０～２３

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        static readonly float _checkLangth = 11f;
        static private int _startDigit = default;
        static private int _SearchCounter = 0;

        #endregion

        /// <summary>
        /// 基本演算関数群
        /// </summary>
        public static class Art // Art は Arithmetic の略
        {
            /// <summary>
            /// ２乗する関数
            /// </summary>
            /// <param name="value">２乗する値</param>
            /// <returns></returns>
            public static float Pow(float value)
            {
                return value * value;
            }

            public static float Pow(float value,float exponent)
            {
                float result = value;
                for(int i = 1; i < exponent; i++)
                {
                    result *= value;
                }
                return result;
            }


            public static float Sqrt(float value)
            {
                float _sign = 1f;
                bool _isOver = default;
                float _root = default;

                // floatは上位10桁 以下切り捨て
                // 1.5E-45 ～ 3.4E38  が 範囲
                // 分岐は　１. １より大きいか小さいか
                //         ２．大きい場合は比較する配列よりちいさいか、小さい場合は配列以下か
                //         ３．最大桁数を求めたら、半数をこえる（最上桁が５の値以上）かを判定（１０が最大桁なら２５００と比較、１０００なら２５００００００と比較など）
                //         ４．加算して超えるまで、を繰り返して求める　最後に８桁目を四捨五入して終わり

                // 3.40000000000000000000000000000000000000 ～ 0.0000000000000000000000000000000000000000000015~

                // 値の正負を確認
                if (value < 0)
                {
                    // 負数なら正数に直し符号を記録
                    value = -value;
                    _sign = -1f;
                }

                // 値が小数点を超えているかどうか
                if (value > 1)
                {
                    // 超えている
                    _isOver = true;
                }
                else
                {
                    // 超えていない
                    _isOver = false;
                }

                // 最大桁
                GetMaxDigit(value , _isOver);

                // ５の倍数
                CheckOverHalf(value , _isOver);

                // 比較と加算
                _root = CheckBorder(value, _root, _startDigit);

                // 値を返す
                return _root * _sign;
            }

            /// <summary>
            /// 比較と加算を行い各桁の値を求める
            /// </summary>
            /// <param name="value">元の値</param>
            /// <param name="root">平方根</param>
            /// <param name="digit">最上桁</param>
            /// <returns></returns>
            private static float CheckBorder(float value , float root , int digit)
            {
                const int MAX_DIGIT = 10;

                for (int i = 0; i < MAX_DIGIT; i++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        root += _SqrtValue[digit + i];
                        if (Pow(root) > value)
                        {
                            root -= _SqrtValue[digit];
                            i = 10;
                        }
                    }
                }
                return root;
            }

            private static void GetMaxDigit(float value , bool isOver)
            {
                bool endSearch = false;

                if (isOver)
                {
                    for (_SearchCounter = 0 ; !endSearch ; _SearchCounter++)
                    {
                        // 
                        if (value < _comparisonInteger[_SearchCounter])
                        {
                            _startDigit = _maxValueInteger[_SearchCounter];
                            endSearch = true;
                        }
                    }
                }
                else
                {
                    for (_SearchCounter = 0 ; !endSearch ; _SearchCounter++)
                    {
                        // 
                        if (value >= _comparisonDecimal[_SearchCounter])
                        {
                            _startDigit = _maxValueDecimal[_SearchCounter];
                            endSearch = true;
                        }
                    }
                }
            }

            private static float CheckOverHalf(float value , bool isOver)
            {
                if (isOver)
                {
                    if(value > _compHalfInteger[_SearchCounter])
                    {
                        return _halfValueInteger[_SearchCounter];
                    }
                    else
                    {
                        return 0f;
                    }
                }
                else
                {
                    if (value > _compHalfDecimal[_SearchCounter])
                    {
                        return _halfValueDecimal[_SearchCounter];
                    }
                    else
                    {
                        return 0f;
                    }
                }
            }

            /// <summary>
            /// 平方根の最上桁が５を超えるか
            /// </summary>
            /// <param name="isOver">小数点を超えているか</param>
            /// <returns></returns>
            private static float[] ComparisonHalfValue(bool isOver)
            {
                if (isOver)
                {
                    return _compHalfInteger;
                }
                else
                {
                    return _compHalfDecimal;
                }
            }


            /// <summary>
            /// 最上桁が５を超える時の初期値
            /// </summary>
            /// <param name="isOver">小数点を超えているか</param>
            /// <returns></returns>
            private static float[] HalfValue(bool isOver)
            {
                if (isOver)
                {
                    return _halfValueInteger;
                }
                else
                {
                    return _halfValueDecimal;
                }
            }
        }

        /// <summary>
        /// 単位・数値変換関数群
        /// </summary>
        public static class Mod
        {
            /// <summary>
            /// 絶対値を取得する関数
            /// </summary>
            /// <param name="Value">取得する値</param>
            /// <returns></returns>
            public static float Abs(float Value)
            {
                if (Value < 0)
                {
                    Value = -Value;
                }
                return Value;
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

        /// <summary>
        /// 制限関数群
        /// </summary>
        public static class Clamp
        {
            /// <summary>
            /// 上限と下限の両方を制限
            /// <para>制限した値を返す</para>
            /// </summary>
            /// <param name="Value">制限する値</param>
            /// <param name="Minimun">下限値</param>
            /// <param name="Maximum">上限値</param>
            /// <returns></returns>
            public static float Both(float Value, float Minimun, float Maximum)
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
            /// 下限のみ制限
            /// <para>制限した値を返す</para>
            /// </summary>
            /// <param name="Value">制限する値</param>
            /// <param name="Minimun">下限値</param>
            /// <returns></returns>
            public static float Min(float Value, float Minimun)
            {
                if (Value < Minimun)
                {
                    Value = Minimun;
                }

                return Value;
            }

            /// <summary>
            /// 上限のみ制限
            /// <para>制限した値を返す</para>
            /// </summary>
            /// <param name="Value">制限する値</param>
            /// <param name="Maximum">上限値</param>
            /// <returns></returns>
            public static float Max(float Value, float Maximum)
            {
                if (Maximum < Value)
                {
                    Value = Maximum;
                }

                return Value;
            }

        }


        /// <summary>
        /// ベクトル関数群
        /// </summary>
        public static class Vector
        {
            /// <summary>
            /// ２つの座標の直線距離を取得する関数
            /// </summary>
            /// <param name="startVector">始点の座標</param>
            /// <param name="endVector">目標の座標</param>
            /// <returns></returns>
            public static float Distance(Vector3 startVector, Vector3 endVector)
            {
                float value = Art.Pow(endVector.x - startVector.x) + Art.Pow((endVector.y - startVector.y));
                value = (float)Math.Sqrt(value + Art.Pow((endVector.z - startVector.z)));
                return value;
            }

            /// <summary>
            /// ２つの座標間のベクトルのノーマライズした値を取得する関数
            /// </summary>
            /// <param name="startVector">始点の座標</param>
            /// <param name="endVector">目標の座標</param>
            public static Vector3 Normalize(Vector3 startVector, Vector3 endVector)
            {
                return (endVector - startVector).normalized;
            }

            /// <summary>
            /// Vector3をXY軸のみの値に変化させる
            /// </summary>
            /// <param name="Vector"></param>
            /// <returns></returns>
            public static Vector3 XY(Vector3 Vector)
            {
                Vector3 tmp = new Vector3(Vector.x, Vector.y, 0f);
                return tmp;
            }

            /// <summary>
            /// Vector3をXZ軸のみの値に変化させる
            /// </summary>
            /// <param name="Vector"></param>
            /// <returns></returns>
            public static Vector3 XZ(Vector3 Vector)
            {
                Vector3 tmp = new Vector3(Vector.x, 0f, Vector.z);
                return tmp;
            }

            /// <summary>
            /// Vector3をYZ軸のみの値に変化させる
            /// </summary>
            /// <param name="Vector"></param>
            /// <returns></returns>
            public static Vector3 YZ(Vector3 Vector)
            {
                Vector3 tmp = new Vector3(0f, Vector.y, Vector.z);
                return tmp;
            }
        }
    }
}
