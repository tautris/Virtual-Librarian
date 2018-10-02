using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Unclazz.Commons.Isbn
{
    /// <summary>
    /// ISBNコードを表すクラスです。
    /// </summary>
    public sealed class IsbnCode
    {
        /// <summary>
        /// 文字列からISBNコードを読み取ります。
        /// <para>
        /// 文字列に数字以外の文字が含まれている場合、それらは単に無視されます。
        /// ISBNコードの各部分を区分するハイフンはあってもなくても読み取り結果に影響を与えません。
        /// </para>
        /// </summary>
        /// <param name="s">文字列</param>
        /// <returns>読み取り結果</returns>
        /// <exception cref="ArgumentNullException">引数が<c>null</c>の場合</exception>
        /// <exception cref="ArgumentException">グループ記号(Group identifier)や出版者記号(Publisher prefix)が特定できなかった場合</exception>
        public static IsbnCode Parse(string s)
        {
            CheckUtility.MustNotBeEmpty(s, nameof(s));

            var digits = GetNormalizedDigits(s);
            var flagDigits = digits.Substring(0, 3);
            var checkDigit = digits[12];
            var group = RegistrationGroups.GetGroups()
                .FirstOrDefault(g => digits.StartsWith(g.PrefixWithoutHyphen));

            if (group == null)
            {
                throw new ArgumentException("Unknown group.");
            }
            var groupDigits = digits.Substring(3, group.PrefixWithoutHyphen.Length - 3);
            var restDigits = digits.Substring(group.PrefixWithoutHyphen.Length, 
                digits.Length - group.PrefixWithoutHyphen.Length - 1);
            var rule = group.Rules.Where(r => r.Length > 0)
                .FirstOrDefault(r => RestDigitsFollowToRule(restDigits, r));

            if (rule == null)
            {
                throw new ArgumentException("Unknown publisher");
            }

            var pubDigits = restDigits.Substring(0, rule.Length);
            var titleDigits = restDigits.Substring(rule.Length, restDigits.Length - rule.Length);

            return new IsbnCode(flagDigits, groupDigits, pubDigits, titleDigits, checkDigit, group.Agency);
        }

        static bool RestDigitsFollowToRule(string restDigits, RegistrationGroupRule rule)
        {
            var publisher = restDigits.Substring(0, rule.Length);
            return rule.RangeStart.CompareTo(publisher) <= 0 
                && publisher.CompareTo(rule.RangeEnd) <= 0;
        }

        static readonly Regex ignoredCharsPattern = new Regex(@"^\s*isbn[^0-9]?(10|13)[^0-9\-]|[^0-9]+", RegexOptions.IgnoreCase);

        static string GetNormalizedDigits(string s)
        {
            var digits = ignoredCharsPattern.Replace(s, string.Empty);
            if (!digits.StartsWith("978") && !digits.StartsWith("979"))
            {
                digits = "978" + digits;
            }
            if (digits.Length != 13)
            {
                throw new ArgumentException("Number of digits in ISBN code must be 13.");
            }
            return digits;
        }

        /// <summary>
        /// <see cref="Parse(string)"/>メソッドによる読み取りを試みます。
        /// 読み取りに失敗してもこのメソッドは例外をスローしません。
        /// </summary>
        /// <param name="s">文字列</param>
        /// <param name="result">読み取り結果</param>
        /// <returns>読み取りに成功した場合<c>true</c></returns>
        public static bool TryParse(string s, out IsbnCode result)
        {
            try
            {
                result = Parse(s);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// このオブジェクトと別のオブジェクトとの間で等価性比較を行います。
        /// </summary>
        /// <param name="left">左被演算子</param>
        /// <param name="right">右被演算子</param>
        /// <returns>等価である場合<c>true</c></returns>
        public static bool operator ==(IsbnCode left, IsbnCode right)
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
        public static bool operator !=(IsbnCode left, IsbnCode right)
        {
            return !(left == right);
        }

        IsbnCode(string f, string g, string p, string t, char cd, string a)
        {
            Flag = Digits.Of(f);
            Group = Digits.Of(g);
            Publisher = Digits.Of(p);
            Title = Digits.Of(t);
            CheckDigit = cd;
            Agency = a;

            IEnumerable<char> seed = f;
            DigitsOnly = seed.Concat(g).Concat(p).Concat(t).ToArray();
        }

        /// <summary>
        /// フラグ(Flag)です。
        /// <c>"978"</c>もしくは<c>"979"</c>が設定されます。
        /// </summary>
        public Digits Flag { get; }
        /// <summary>
        /// グループ記号(Group identifier)です。
        /// </summary>
        public Digits Group { get; }
        /// <summary>
        /// 出版者記号(Publisher prefix)です。
        /// </summary>
        public Digits Publisher { get; }
        /// <summary>
        /// 書名記号(Title identifier)です。
        /// </summary>
        public Digits Title { get; }
        /// <summary>
        /// 検査数字(Check digit)です。
        /// </summary>
        public char CheckDigit { get; }
        /// <summary>
        /// 国・地域・言語圏におけるISBN登録エージェンシー(Agency)の名前です。
        /// </summary>
        public string Agency { get; }
        private char[] DigitsOnly { get; }
        private string _ToStringCache = null;

        /// <summary>
        /// このオブジェクトが表すISBNの文字列表現を返します。
        /// 文字列のフォーマットは： "ISBN-13 &lt;flag>-&lt;group>-&lt;publisher>-&lt;title>-&lt;checkdigit>"
        /// </summary>
        /// <returns>文字列表現</returns>
        public override string ToString()
        {
            if (_ToStringCache == null)
            {
                _ToStringCache = ToString(IsbnCodeStyles.WithIsbnPrefix | 
                    IsbnCodeStyles.WithIsbnLength | IsbnCodeStyles.WithHyphens);
            }
            return _ToStringCache;
        }

        /// <summary>
        /// このオブジェクトが表すISBNの文字列表現を返します。
        /// </summary>
        /// <param name="styles">フォーマットを行う際の形式</param>
        /// <returns>文字列表現</returns>
        public string ToString(IsbnCodeStyles styles)
        {
            if (Flag.StringValue != "978" && styles.HasFlag(IsbnCodeStyles.AsIsbn10Code))
            {
                throw new ArgumentException("This ISBN code cannot be represented by ISBN-10 style.");
            }

            var buff = new StringBuilder();
            if (styles.HasFlag(IsbnCodeStyles.WithIsbnPrefix))
            {
                buff.Append("ISBN");
                if (styles.HasFlag(IsbnCodeStyles.WithIsbnLength))
                {
                    buff.Append(styles.HasFlag(IsbnCodeStyles.AsIsbn10Code) ? "-10 " : "-13 ");
                }
                else if (styles.HasFlag(IsbnCodeStyles.WithSpaceAfterPrefix))
                {
                    buff.Append(' ');
                }
            }

            var mayHyphen = styles.HasFlag(IsbnCodeStyles.WithHyphens) ? "-" : string.Empty;
            var checkDigit = 'Z';
            
            if (styles.HasFlag(IsbnCodeStyles.AsIsbn10Code))
            {
                checkDigit = CalcUtility.CheckDigitForIsbn10(DigitsOnly.Skip(3));
            }
            else
            {
                buff.Append(Flag).Append(mayHyphen);
                checkDigit = CalcUtility.CheckDigitForIsbn13(DigitsOnly);
            }

            return buff
                .Append(Group).Append(mayHyphen).Append(Publisher).Append(mayHyphen)
                .Append(Title).Append(mayHyphen).Append(CheckDigit).ToString();
        }

        /// <summary>
        /// このオブジェクトと別のオブジェクトとの間で等価性比較を行います。
        /// </summary>
        /// <param name="obj">別のオブジェクト</param>
        /// <returns>等価である場合<c>true</c></returns>
        public override bool Equals(object obj)
        {
            var that = obj as IsbnCode;
            return that != null && this.ToString() == that.ToString();
        }
        /// <summary>
        /// このオブジェクトのハッシュ値を返します。
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
