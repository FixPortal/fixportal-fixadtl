// FP Enhancement: 2026-05-24 — modernised for net10 (file-scoped, nullable, FixPortal namespace).
#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using FixPortal.FixAtdl.Model.Collections;

namespace FixPortal.FixAtdl.Utility;

public interface IResolvable<THost, TValueSource>
{
    void Resolve(THost host, ISimpleDictionary<TValueSource> sourceCollection);
}
