using System;

namespace MethodRaid.Domain.Models
{
    [Flags]
    public enum ETypeModel { book = 1, client = 2, getbook=3, empty = 4 };

    [Flags]
    public enum ETypeSelectedBook { read=1, free=2, all=3, empty = 4 }
}
