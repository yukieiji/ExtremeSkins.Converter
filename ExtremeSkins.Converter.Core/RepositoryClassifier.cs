using System;
using System.IO;
using System.Linq;

using LibGit2Sharp;

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
                string[] splitedUrl = targetPath.Split('/');
                string cloneTargetPath = Path.Combine(
                    Directory.GetCurrentDirectory(), CloneFolder, splitedUrl[^1]);

                Utility.ForceRecreateFolder(cloneTargetPath);

                string gitFolder = Repository.Clone(targetPath, cloneTargetPath);
                targetPath = gitFolder.Substring(0, gitFolder.Length - 5);
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
