using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Unclazz.Commons.Isbn
{
    /// <summary>
    /// 数字のシーケンスを表すクラスです。
    /// このクラスのインスタンスはイミュータブルです。
    /// </summary>
    public sealed class Digits
    {
        /// <summary>
        /// 整数値を元にこのクラスのインスタンスを生成します。
        /// <see cref="Length"/>の値は当該整数値の持つ桁数と同じ値となります。
        /// </summary>
        /// <param name="value">整数値</param>
        /// <returns>インスタンス</returns>
        /// <exception cref="ArgumentOutOfRangeException">整数値が負数である場合</exception>
        public static Digits Of(int value)
        {
            CheckUtility.MustBeGreaterThanOrEqual0(value, nameof(value));
            return new Digits(value);
        }
        /// <summary>
        /// 整数値を元にこのクラスのインスタンスを生成します。
        /// <see cref="Length"/>の値は第2引数と同じ値となります。
        /// </summary>
        /// <param name="value">整数値</param>
        /// <param name="length">桁数</param>
        /// <returns>インスタンス</returns>
        /// <exception cref="ArgumentException">第1引数の整数値の持つ桁数が第2引数で指定された桁数よりも大きい場合</exception>
        /// <exception cref="ArgumentOutOfRangeException">第1引数の整数値が負数である場合、
        /// もしくは、第2引数の整数値が1より小さい場合</exception>
        public static Digits Of(int value, int length)
        {
            CheckUtility.MustBeGreaterThanOrEqual0(value, nameof(value));
            CheckUtility.MustBeGreaterThan0(length, nameof(length));
            var s = value.ToString();
            if (s.Length > length)
            {
                throw new ArgumentException("Mismatch between number of digits and value of length.");
            }
            return Of(value.ToString().PadLeft(length, '0'));
        }
        /// <summary>
        /// 文字列を元にこのクラスのインスタンスを生成します。
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns>インスタンス</returns>
        /// <exception cref="ArgumentException">文字列に数字以外の文字が含まれているか、文字列が空である場合</exception>
        /// <exception cref="ArgumentNullException">文字列が<c>null</c>である場合</exception>
        public static Digits Of(string value)
        {
            CheckUtility.MustNotBeEmpty(value, nameof(value));
            if(Regex.IsMatch(value, "[^0-9]"))
            {
                throw new ArgumentException("Non-digit character is found.");
            }
            return new Digits(value);
        }
        /// <summary>
        /// このオブジェクトと別のオブジェクトとの間で等価性比較を行います。
        /// </summary>
        /// <param name="left">左被演算子</param>
        /// <param name="right">右被演算子</param>
        /// <returns>等価である場合<c>true</c></returns>
        public static bool operator ==(Digits left, Digits right)
        {
            if (object.ReferenceEquals(left, right))
            {
                return true;
            }
            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }
            return left.Equals(right);
        }
        /// <summary>
        /// このオブジェクトと別のオブジェクトとの間で等価性比較を行います。
        /// </summary>
        /// <param name="left">左被演算子</param>
        /// <param name="right">右被演算子</param>
        /// <returns>等価でない場合<c>true</c></returns>
        public static bool operator !=(Digits left, Digits right)
        {
            return !(left == right);
        }

        Digits(string value)
        {
            StringValue = value;
            Length = StringValue.Length;
        }
        Digits(int value)
        {
            StringValue = value.ToString();
            Length = StringValue.Length;
        }

        /// <summary>
        /// このオブジェクトが表す整数値です。
        /// </summary>
        public int IntValue
        {
            get
            {
                if (StringValue == "0")
                {
                    return 0;
                }
                var trimmed = StringValue.TrimStart('0');
                return trimmed.Length == 0 ? 0 : int.Parse(StringValue.TrimStart('0'));
            }
        }
        /// <summary>
        /// このオブジェクトが表す数字のシーケンスです。
        /// このシーケンスは1つもしくは複数の<c>'0'</c>から始まる可能性があります。
        /// </summary>
        public string StringValue { get; }
        /// <summary>
        /// このオブジェクトが表す数字のシーケンスの長さ（桁数）です。
        /// </summary>
        public int Length { get; }
        /// <summary>
        /// このオブジェクトの文字列表現を返します。
        /// このメソッドが返す値は<see cref="StringValue"/>プロパティが返す値と同じ内容です。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return StringValue;
        }
        /// <summary>
        /// このオブジェクトのハッシュ値を返します。
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return StringValue.GetHashCode();
        }
        /// <summary>
        /// このオブジェクトと別のオブジェクトとの間で等価性比較を行います。
        /// </summary>
        /// <param name="obj">別のオブジェクト</param>
        /// <returns>等価である場合<c>true</c></returns>
        public override bool Equals(object obj)
        {
            var that = obj as Digits;
            return that != null && StringValue == that.StringValue;
        }
    }
}
