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
                if (Directory.Exists(cloneTargetPath))
                {
                    DirectoryInfo di = new DirectoryInfo(cloneTargetPath);
                    RemoveReadonlyAttribute(di);
                    di.Delete(true);
                }

                Directory.CreateDirectory(cloneTargetPath);

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

    public static void RemoveReadonlyAttribute(DirectoryInfo dirInfo)
    {
        //基のフォルダの属性を変更
        if ((dirInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
        {
            dirInfo.Attributes = FileAttributes.Normal;
        }
        //フォルダ内のすべてのファイルの属性を変更
        foreach (FileInfo fi in dirInfo.GetFiles())
        {
            if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                fi.Attributes = FileAttributes.Normal;
            }
        }
            
        //サブフォルダの属性を回帰的に変更
        foreach (DirectoryInfo di in dirInfo.GetDirectories())
        {
            RemoveReadonlyAttribute(di);
        }
    }
}
