using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unclazz.Commons.Isbn
{
    /// <summary>
    /// 計算ユーティリティ・クラスです。
    /// </summary>
    static class CalcUtility
    {
        /// <summary>
        /// ISBN13コードのためモジュラス10ウエイト3-1(一括)による検査数字の算出を行います。
        /// </summary>
        /// <param name="digits">数字のみからなる文字シーケンス</param>
        /// <returns>検査数字</returns>
        internal static char CheckDigitForIsbn13(IEnumerable<char> digits)
        {
            if (digits.Count() != 12)
            {
                throw new ArgumentException("Number of digits must be 12.");
            }
            var fromRight = digits.Reverse().Select((d, i) => new { Digit = i + 1, Value = charToInt(d) });
            var sum = fromRight.Select(d => d.Value * (d.Digit % 2 == 1 ? 3 : 1)).Sum();
            var remainder = sum % 10;
            return intToChar(remainder == 0 ? 0 : 10 - remainder);
        }

        /// <summary>
        /// ISBN10コードのためモジュラス10ウエイト10〜2による検査数字の算出を行います。
        /// </summary>
        /// <param name="digits">数字のみからなる文字シーケンス</param>
        /// <returns>検査数字</returns>
        internal static char CheckDigitForIsbn10(IEnumerable<char> digits)
        {
            if (digits.Count() != 9)
            {
                throw new ArgumentException("Number of digits must be 9.");
            }
            var fromRight = digits.Reverse().Select((d, i) => new { Digit = i + 1, Value = charToInt(d) });
            var sum = fromRight.Select(d => d.Value * (d.Digit + 1)).Sum();
            var remainder = sum % 11;
            var sub = 11 - remainder;
            if (sub == 11) sub = 0;
            return intToChar(sub);
        }

        /// <summary>
        /// 整数をそれを表すASCII文字に変換します。
        /// サポートする整数は<c>0</c>から<c>10</c>までです。
        /// とくに<c>10</c>の場合は<c>'X'</c>を返します。
        /// </summary>
        /// <param name="v">整数</param>
        /// <returns>ASCII文字</returns>
        static char intToChar(int v)
        {
            CheckUtility.MustBeBetween(v, 0, 10, nameof(v));
            return v == 10 ? 'X' : (char)(v + '0');
        }

        /// <summary>
        /// ASCII文字をそれが表す整数に変換します。
        /// サポートする文字は<c>'0'</c>から<c>'9'</c>までと<c>'X'</c>です。
        /// とくに<c>'X'</c>の場合は<c>10</c>を返します。
        /// </summary>
        /// <param name="v">ASCII文字</param>
        /// <returns>整数</returns>
        static int charToInt(char v)
        {
            if ((v < '0' || '9' < v) && v != 'X')
            {
                throw new ArgumentOutOfRangeException("'0' <= v <= '9' or v == 'X'");
            }
            return v == 'X' ? 10 : v - '0';
        }
    }
}
