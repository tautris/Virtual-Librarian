using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unclazz.Commons.Isbn
{
    /// <summary>
    /// <see cref="IsbnCode"/>を文字列化する際のスタイルを表す列挙型です。
    /// </summary>
    [Flags]
    public enum IsbnCodeStyles
    {
        /// <summary>
        /// ISBN10コードとして文字列化
        /// </summary>
        AsIsbn10Code   = 1,
        /// <summary>
        /// 先頭に<c>"ISBN"</c>を伴うかたちで文字列化
        /// </summary>
        WithIsbnPrefix = 2,
        /// <summary>
        /// 先頭に<c>"ISBN-10 "</c>もしくは<c>"ISBN-13 "</c>を伴うかたちで文字列化
        /// </summary>
        WithIsbnLength = 4,
        /// <summary>
        /// 先頭の<c>"ISBN"</c>の直後に空白文字<c>' '</c>を伴うかたちで文字列化
        /// </summary>
        WithSpaceAfterPrefix = 8,
        /// <summary>
        /// ISBNコード本体の各部分を区切るハイフンを伴うかたちで文字列化
        /// </summary>
        WithHyphens = 16,
    }
}
