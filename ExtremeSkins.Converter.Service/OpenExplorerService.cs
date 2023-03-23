using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExtremeSkins.Converter.Service.Interface;

namespace ExtremeSkins.Converter.Service;

public sealed class OpenExplorerService
{
    public class Setting
    {
        public string Arg { get; init; } = string.Empty;
        public string TargetPath { get; init; } = string.Empty;
    }

    public void Open(Setting setting)
    {
        if (string.IsNullOrEmpty(setting.TargetPath))
        {
            return;
        }

        string targetPath = setting.TargetPath;
        string arg = setting.Arg;

        string processArg = 
            string.IsNullOrEmpty(arg) ? 
            $@"""{targetPath}""" :
            $@"{arg}, ""{targetPath}""";

        System.Diagnostics.Process.Start("EXPLORER.EXE", processArg);
    }
}
