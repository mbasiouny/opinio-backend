using System.ComponentModel;

namespace Opinio.Core.Enums;

public enum EntityStatus
{
    [Description("1")]
    Pending = 1,

    [Description("2")]
    Active = 2,

    [Description("3")]
    Rejected = 3,
}

