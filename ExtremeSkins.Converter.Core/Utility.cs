using System.IO;

namespace ExtremeSkins.Converter.Core;

public static class Utility
{
    public static void ForceRecreateFolder(string targetPath)
    {
        if (Directory.Exists(targetPath))
        {
            DirectoryInfo di = new DirectoryInfo(targetPath);
            RemoveReadonlyAttribute(di);
            di.Delete(true);
        }

        Directory.CreateDirectory(targetPath);
    }

    private static void RemoveReadonlyAttribute(DirectoryInfo dirInfo)
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
