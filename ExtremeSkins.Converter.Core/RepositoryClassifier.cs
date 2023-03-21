using System;
using System.IO;
using System.Linq;

using ExtremeSkins.Converter.Core.Analyzer.NebulaOnTheShip;
using ExtremeSkins.Converter.Core.Analyzer.SuperNewRoles;
using ExtremeSkins.Converter.Core.Analyzer.TheOtherRoles;
using ExtremeSkins.Converter.Core.Interface;

namespace ExtremeSkins.Converter.Core;

public static class RepositoryClassifier
{
    public const string CloneFolder = "clonedRepo";

    private static readonly Type[] analyzerType = new Type[]
    { 
        typeof(MoreCosmicAnalyzer),
        typeof(SuperNewNamePlatesAnalyzer),
        typeof(TheOtherHatsAnalyzer)
    };

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

        foreach (Type analyzer in analyzerType)
        {
            var instanceAnalyzer = (IRepositoryAnalyzer)Activator.CreateInstance(
                analyzer, targetPath);
            if (instanceAnalyzer.IsValid())
            {
                return instanceAnalyzer;
            }
        }

        throw new Exception();
    }
}
