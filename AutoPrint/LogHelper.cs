using System;
using System.Collections.Generic;
using System.Text;



public static class LogHelper
{
    public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

    public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

    /// <summary>
    /// 信息日志
    /// </summary>
    /// <param name="info"></param>
    public static void WriteLog(string info)
    {
        if (loginfo.IsInfoEnabled)
        {
            loginfo.Info(info);
        }
    }
    /// <summary>
    /// 错误日志
    /// </summary>
    /// <param name="info"></param>
    /// <param name="se"></param>
    public static void WriteLog(string info, Exception se)
    {
        if (logerror.IsErrorEnabled)
        {
            logerror.Error(info, se);
        }
    }
    /// <summary>
    /// 取得当前源码的哪一行
    /// </summary>
    /// <returns></returns>
    public static int GetLineNum()
    {
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
        return st.GetFrame(0).GetFileLineNumber();
    }

    /// <summary>
    /// 取当前源码的源文件名
    /// </summary>
    /// <returns></returns>
    public static string GetCurSourceFileName()
    {
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);

        return st.GetFrame(0).GetFileName();

    }
}
