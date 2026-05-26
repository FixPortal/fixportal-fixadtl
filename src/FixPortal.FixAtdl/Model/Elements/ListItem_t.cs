// FP Enhancement: 2026-05-24 — modernised for net10 (file-scoped, nullable, FixPortal namespace).
#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace FixPortal.FixAtdl.Model.Elements;

public class ListItem_t
{
    public string EnumId { get; set; } = null!;
    public string UiRep { get; set; } = null!;
    public bool IsSelected { get; set; }

    public override string ToString()
    {
        return UiRep;
    }
}
