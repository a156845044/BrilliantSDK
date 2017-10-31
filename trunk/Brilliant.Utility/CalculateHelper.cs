using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brilliant.Utility
{
    /// <summary>
    /// 数值计算帮助类
    /// </summary>
    public class CalculateHelper
    {
        /// <summary>
        /// 两个浮点数相减
        /// </summary>
        /// <param name="num1">被减数</param>
        /// <param name="num2">减数</param>
        /// <param name="digits">保留位数 0-15</param>
        /// <returns>差</returns>
        /// <remarks>作者：dfq 时间：2017-03-16</remarks>
        public static double AccSub(double num1, double num2, int? digits = null)
        {
            int r1, r2, n;

            double m;
            try
            {
                r1 = num1.ToString().Split('.')[1].Length;
            }
            catch (Exception)
            {
                r1 = 0;
            }
            try
            {
                r2 = num2.ToString().Split('.')[1].Length;
            }
            catch (Exception)
            {
                r2 = 0;
            }

            m = Math.Pow(10, Math.Max(r1, r2));
            n = (r1 >= r2) ? r1 : r2;

            //自定义保留位数
            if (digits != null)
            {
                if (digits.HasValue)
                {
                    n = digits.Value;
                }
            }

            if (n > 15)
            {
                n = 15;
            }
            return Math.Round(((num1 * m - num2 * m) / m), n);
        }

        /// <summary>
        /// 两个浮点数求和
        /// </summary>
        /// <param name="num1">被加数</param>
        /// <param name="num2">加数</param>
        /// <returns>和</returns>
        /// <remarks>作者：dfq 时间：2017-03-16</remarks>
        public static double AccAdd(double num1, double num2)
        {
            int r1, r2;
            double m;
            try
            {
                r1 = num1.ToString().Split('.')[1].Length;
            }
            catch (Exception)
            {
                r1 = 0;
            }
            try
            {
                r2 = num2.ToString().Split('.')[1].Length;
            }
            catch (Exception)
            {
                r2 = 0;
            }
            m = Math.Pow(10, Math.Max(r1, r2));
            return Math.Round(num1 * m + num2 * m) / m;
        }

        /// <summary>
        /// 两个浮点数两数相除
        /// </summary>
        /// <param name="num1">被除数</param>
        /// <param name="num2">除数</param>
        /// <returns>商</returns>
        /// <remarks>作者：dfq 时间：2017-03-16</remarks>
        public static double AccDiv(double num1, double num2)
        {
            int t1, t2;
            double r1, r2;
            try
            {
                t1 = num1.ToString().Split('.')[1].Length;
            }
            catch (Exception)
            {
                t1 = 0;
            }
            try
            {
                t2 = num2.ToString().Split('.')[1].Length;
            }
            catch (Exception)
            {
                t2 = 0;
            }
            r1 = Convert.ToDouble(num1.ToString().Replace(".", ""));
            r2 = Convert.ToDouble(num2.ToString().Replace(".", ""));
            return (r1 / r2) * Math.Pow(10, t2 - t1);
        }

        /// <summary>
        /// 两个浮点数两数乘
        /// </summary>
        /// <param name="num1">被乘数</param>
        /// <param name="num2">乘数</param>
        /// <returns>乘积</returns>
        /// <remarks>作者：dfq 时间：2017-03-16</remarks>
        public static double AccMul(double num1, double num2)
        {
            int m = 0;
            string s1 = num1.ToString(), s2 = num2.ToString();
            try
            {
                m += s1.Split('.')[1].Length;
            }
            catch (Exception)
            {

            }

            try
            {
                m += s2.Split('.')[1].Length;
            }
            catch (Exception)
            {

            }

            return Convert.ToDouble(s1.Replace(".", "")) * Convert.ToDouble(s2.Replace(".", "")) / Math.Pow(10, m);
        }

        /// <summary>
        /// 多个数进行求和
        /// </summary>
        /// <param name="param">可边长参数（两个及两个以上）</param>
        /// <returns>和</returns>
        /// <remarks>作者：dfq 时间：2017.05.05</remarks>
        public static double AccMultiAdd(params double[] param)
        {
            double total = 0;
            foreach (double item in param)
            {
                total = AccAdd(total, item);
            }
            return total;
        }

        /// <summary>
        /// 多个数进行求积
        /// </summary>
        /// <param name="param">可边长参数（两个及两个以上）</param>
        /// <returns>积</returns>
        /// <remarks>作者：dfq 时间：2017.05.05</remarks>
        public static double AccMultiMul(params double[] param)
        {
            double total = 0;
            foreach (double item in param)
            {
                total = AccMul(total, item);
            }
            return total;
        }

        /// <summary>
        /// 多个数进行求差
        /// </summary>
        /// <param name="num1">被减数</param>
        /// <param name="param">可边长参数（两个及两个以上）</param>
        /// <returns>积</returns>
        /// <remarks>作者：dfq 时间：2017.05.05</remarks>
        public static double AccMultiSub(double num1, params double[] param)
        {
            if (param.Length <= 1)
            {
                return 0;
            }

            double total = num1;
            foreach (double item in param)
            {
                total = AccSub(total, item);
            }
            return total;
        }

        /// <summary>
        /// 多个数进行求商
        /// </summary>
        /// <param name="num1">被减数</param>
        /// <param name="param">可边长参数（两个及两个以上）</param>
        /// <returns>商</returns>
        /// <remarks>作者：dfq 时间：2017.05.05</remarks>
        public static double AccMultiDiv(double num1, params double[] param)
        {
            if (param.Length <= 1)
            {
                return 0;
            }
            double total = num1;
            foreach (double item in param)
            {
                total = AccDiv(total, item);
            }
            return total;
        }
    }
}
