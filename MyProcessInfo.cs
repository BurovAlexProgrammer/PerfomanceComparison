using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Структура, используемая для обмена данными между приложениями
/// </summary>
public struct MyProcessInfo
{
    public long executeTime;
    public long executeCount;
    public long memoryUsed;
    public long peakMemoryUsed;
}
