using System.Collections.Generic;
using System.Linq;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly HashSet<string> CSharpReservedKeywords = new()
        {
            "abstract",
            "as",
            "base",
            "bool",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "do",
            "double",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "lock",
            "long",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "using",
            "virtual",
            "void",
            "volatile",
            "while"
        };

        private static readonly HashSet<string> CSharpContextualKeywords = new()
        {
            "add",
            "and",
            "alias",
            "ascending",
            "async",
            "await",
            "by",
            "descending",
            "dynamic",
            "equals",
            "from",
            "get",
            "global",
            "group",
            "init",
            "into",
            "join",
            "let",
            "managed",
            "nameof",
            "nint",
            "not",
            "notnull",
            "nuint",
            "on",
            "or",
            "orderby",
            "partial",
            "record",
            "remove",
            "select",
            "set",
            "unmanaged",
            "value",
            "var",
            "when",
            "where",
            "with",
            "yield"
        };

        public static bool IsValidIdentifierStartCharacter(this char c)
        {
            return char.IsLetter(c) || (c == '_') || (c == '@');
        }

        public static bool IsValidIdentifierPartCharacter(this char c)
        {
            return char.IsLetter(c) || char.IsNumber(c) || (c == '_') || (c == '@');
        }

        public static bool IsCSharpReservedKeyword(this string identifier)
        {
            return CSharpReservedKeywords.Contains(identifier);
        }

        public static bool IsCSharpContextualKeyword(this string identifier)
        {
            return CSharpContextualKeywords.Contains(identifier);
        }

        public static bool IsCSharpKeyword(this string identifier)
        {
            return IsCSharpReservedKeyword(identifier) || IsCSharpContextualKeyword(identifier);
        }

        public static bool IsValidIdentifier(this string identifier)
        {
            switch (identifier)
            {
                case "":
                case null:
                    return false;
                default:
                    if (identifier.IndexOf('.') >= 0)
                    {
                        var str = identifier;
                        var chArray = new[] {'.'};
                        return str.Split(chArray)
                                  .All(identifier1 => identifier1.IsValidIdentifier());
                    }

                    if (identifier.IsCSharpReservedKeyword() ||
                        !identifier[0].IsValidIdentifierStartCharacter())
                    {
                        return false;
                    }

                    for (var index = 1; index < identifier.Length; ++index)
                    {
                        if (!identifier[index].IsValidIdentifierPartCharacter())
                        {
                            return false;
                        }
                    }

                    return true;
            }
        }
    }
}
