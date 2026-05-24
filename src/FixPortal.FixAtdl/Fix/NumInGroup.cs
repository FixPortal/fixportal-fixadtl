#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using Atdl4net.Diagnostics;
using Atdl4net.Resources;

namespace Atdl4net.Fix;

/// <summary>
/// Represents a FIX NumInGroup value, the number of elements in a repeating block.
/// </summary>
public readonly struct NumInGroup
{
    private readonly int _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="NumInGroup"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    public NumInGroup(int value)
    {
        if (value < 0)
            throw ThrowHelper.New<ArgumentOutOfRangeException>(typeof(NumInGroup).FullName, ErrorMessages.NonNegativeIntRequired, value);

        _value = value;
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="int"/> to <see cref="Atdl4net.Fix.NumInGroup"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator NumInGroup(int value)
    {
        return new NumInGroup(value);
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Atdl4net.Fix.NumInGroup"/> to <see cref="int"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator int(NumInGroup value)
    {
        return value._value;
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return _value.ToString();
    }
}
