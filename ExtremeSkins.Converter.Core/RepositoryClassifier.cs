using System;
using System.IO;
using System.Linq;

using ExtremeSkins.Converter.Core.Interface;

namespace ExtremeSkins.Converter.Core;

public static class RepositoryClassifier
{
    public const string CloneFolder = "clonedRepo";

    public static IRepositoryAnalyzer Classify(string targetPath)
    {
        if (string.IsNullOrEmpty(targetPath))
        {
            throw new ArgumentNullException();
        }

        if (targetPath.StartsWith("http"))
        {
            try
            {
                string cloneTargetPath = Path.Combine(
                    Directory.GetCurrentDirectory(), CloneFolder);
                if (!Directory.Exists(cloneTargetPath))
                {
                    Directory.CreateDirectory(cloneTargetPath);
                }
                targetPath = LibGit2Sharp.Repository.Clone(targetPath, cloneTargetPath);
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }

        if (!Directory.Exists(targetPath) ||
            !Directory.EnumerateFiles(targetPath).Any())
        {
            throw new ArgumentNullException();
        }



        throw new Exception();
    }
}
