// FP Enhancement: 2026-05-24 — modernised for net10 (file-scoped, nullable, FixPortal namespace).
#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Collections.Generic;

namespace FixPortal.FixAtdl.Xml.Serialization;

public class EnumDefinition(Type enumType, Dictionary<string, Enum> textValues)
{
    public Type EnumType { get; private set; } = enumType;
    public Dictionary<string, Enum> TextValues { get; private set; } = textValues;
}

