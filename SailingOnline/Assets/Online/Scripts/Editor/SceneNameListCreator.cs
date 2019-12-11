using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Online.Editor
{

    /// <summary>
    /// @brief シーン名を定数で管理するくらすを作成するクラス
    /// </summary>
    public static class SceneNameListCreator
    {

        //無効な文字を管理する配列
        private static readonly string[] Invalud_Chars =
        {
            " " , "!", "\"", "#", "$",
            "%", "&", "\'", "(", ")",
            "-", "=", "^" , "~", "\\",
            "|", "[", "{" , "@", "`",
            "]", "}", ":" , "*", ";",
            "+", "/", "?" , ".", ">",
            ",", "<"
        };

        //プルダウンメニューにつける名前
        private const string MenuName = "Online/Create/";
        //ファイルパス
        private const string FilePass = "Assets/Online/Scripts/Scene/SceneNameList.cs";

        //ファイル名（上が拡張子あり）
        private static readonly string FileName = Path.GetFileName(FilePass);
        private static readonly string FileName_Without_Extension = Path.GetFileNameWithoutExtension(FilePass);

        /// <summary>
        /// @brief シーン名を定数で管理するクラスを作成する
        /// </summary>
        [MenuItem(MenuName + "Scene Name List",priority = 0)]
        public static void Create()
        {

            if (!CanCreate())
            {
                return;
            }

            CreateScript();

            EditorUtility.DisplayDialog(FileName, "作成が完了しました", "OK");

        }

        /// <summary>
        /// @brief シーン名を定数で管理するクラスを作成できるかどうかを返す
        /// </summary>
        /// <returns>作成できる場合true、できなければfalseを返す</returns>
        [MenuItem(MenuName + "Scene Name List", true)]
        public static bool CanCreate()
        {

            return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
        }

        /// <summary>
        /// @brief ファイル内の記述部分を作成する
        /// </summary>
        private static void CreateScript()
        {

            var builder = new StringBuilder();

            //namespaceの挿入
            builder.AppendLine("namespace Online.Editor");
            builder.AppendLine("{\n");

            //クラス部分の挿入
            builder.AppendLine("\t/// <summary>");
            builder.AppendLine("\t/// @brief シーン名を定数で管理するクラス");
            builder.AppendLine("\t/// <summary>");
            builder.AppendFormat("\tpublic static class {0}", FileName_Without_Extension).AppendLine();
            builder.AppendLine("\t{\n");

            //定数部分の挿入
            foreach (var n in EditorBuildSettings.scenes
                .Select(c => Path.GetFileNameWithoutExtension(c.path))
                .Distinct()
                .Select(c => new { var = RemoveInvalidChars(c), val = c }))
            {
                builder.Append("\t\t").AppendFormat(@"public const string {0} = ""{1}"";", n.var, n.val).AppendLine();
            }

            builder.AppendLine("\n\t}\n");
            builder.AppendLine("}");

            var directoryName = Path.GetDirectoryName(FilePass);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            File.WriteAllText(FilePass, builder.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

        }

        /// <summary>
        /// @brief 使用できない文字を削除する
        /// </summary>
        /// <param name="str">調べる文字列</param>
        /// <returns>削除後の文字列</returns>
        private static string RemoveInvalidChars(string str)
        {

            Array.ForEach(Invalud_Chars, c => str = str.Replace(c, string.Empty));

            return str;
        }

    }

}