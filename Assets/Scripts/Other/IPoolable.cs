using System;
using System.Collections;
using System.Collections.Generic;

public interface IPoolable
{
    event System.Action<IPoolable> OnDestroy;
    void Reset();
}
