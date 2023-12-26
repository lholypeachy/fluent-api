﻿using System;

namespace ObjectPrinting
{
    public interface IWrap<TOwner>
    {
        PrintingConfig<TOwner> And { get; }

        IWrap<TOwner> Wrap(Func<string, string> modify);
    }
}