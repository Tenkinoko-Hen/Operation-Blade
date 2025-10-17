using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Editor
{
    /// <summary>
    /// QFramework 文件夹结构一键生成器
    /// </summary>
    public class QFrameworkFolderCreator : OdinEditorWindow
    {
        [MenuItem("工具/Qframework文件结构生成器")]
        private static void OpenWindow()
        {
            var window = GetWindow<QFrameworkFolderCreator>();
            window.titleContent = new GUIContent("QF 文件夹生成器");
            window.minSize = new Vector2(400, 300);
            window.Show();
        }

        [Title("QFramework 文件夹结构生成器")]
        [InfoBox("根据 QFramework 推荐的最佳实践，一键生成规范的文件夹结构。")]

        // --- 美术资源 (Art) ---
        [ToggleGroup("createArt", "美术资源 (Art)", Order = 1)]
        [GUIColor(0.9f, 0.9f, 1f)]
        public bool createArt = true;

        [ToggleGroup("createArt")]
        [ShowIf("createArt")]
        [HideLabel]
        [MultiLineProperty(10)]
        [ShowInInspector, ReadOnly]
        private const string ArtFoldersInfo =
            "Art/Animations: 动画控制器、动画片段\n" +
            "Art/Audio: 音频文件 (Music/Sound)\n" +
            "Art/Fonts: 字体文件\n" +
            "Art/Materials: 材质球\n" +
            "Art/Models: 3D模型\n" +
            "Art/Prefabs: 预制体\n" +
            "Art/Shaders: 着色器\n" +
            "Art/Sprites: 2D精灵/UI图片\n" +
            "Art/Textures: 贴图";


        // --- 代码脚本 (Scripts) ---
        [ToggleGroup("createScripts", "代码脚本 (Scripts)", Order = 2)]
        [GUIColor(0.9f, 1f, 0.9f)]
        public bool createScripts = true;

        [ToggleGroup("createScripts")]
        [ShowIf("createScripts")]
        [HideLabel]
        [MultiLineProperty(6)]
        [ShowInInspector, ReadOnly]
        private const string ScriptsFoldersInfo =
            "Scripts/Architecture: 框架核心结构 (System, Model, Controller, Command)\n" +
            "Scripts/Editor: 编辑器脚本\n" +
            "Scripts/ViewController: 游戏运行时逻辑\n" +
            "Scripts/UIPanel: UI界面逻辑\n" +
            "Scripts/Utlity: 工具脚本";


        // --- 核心目录 (Core) ---
        [ToggleGroup("createCore", "核心目录 (Core)", Order = 3)]
        [GUIColor(1f, 0.9f, 0.9f)]
        public bool createCore = true;

        [ToggleGroup("createCore")]
        [ShowIf("createCore")]
        [HideLabel]
        [MultiLineProperty(4)]
        [ShowInInspector, ReadOnly]
        private const string CoreFoldersInfo =
            "Scenes: 游戏场景文件\n" +
            "Resources: 需要通过 Resources.Load 加载的资源\n" +
            "Settings: 各种 ScriptableObject 配置文件";


        [Button("一键生成", ButtonSizes.Large), GUIColor(0, 1, 0)]
        [PropertySpace(20)]
        private void Generate()
        {
            var folders = new List<string>();

            if (createArt)
            {
                folders.AddRange(new[]
                {
                    "Art/Animations", "Art/Audio/Music", "Art/Audio/Sound", "Art/Fonts", "Art/Materials",
                    "Art/Models", "Art/Prefabs", "Art/Shaders", "Art/Sprites", "Art/Textures"
                });
            }

            if (createScripts)
            {
                folders.AddRange(new[]
                {
                    "Scripts/Architecture/System", "Scripts/Architecture/Model", "Scripts/Architecture/Controller", "Scripts/Architecture/Command",
                    "Scripts/Editor",
                    "Scripts/ViewController",
                    "Scripts/UIPanel",
                    "Scripts/Utlity"
                });
            }

            if (createCore)
            {
                folders.AddRange(new[]
                {
                    "Scenes", "Resources", "Settings"
                });
            }

            CreateAllFolders(folders);

            this.Close();
        }

        /// <summary>
        /// 创建所有指定的文件夹
        /// </summary>
        /// <param name="folders">要创建的文件夹路径列表 (相对于 Assets 目录)</param>
        private void CreateAllFolders(List<string> folders)
        {
            int createdCount = 0;
            foreach (var folder in folders)
            {
                // 使用 Path.Combine 安全地拼接路径
                var path = Path.Combine(Application.dataPath, folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    createdCount++;
                }
            }

            // 刷新 AssetDatabase 以在 Unity 编辑器中显示新创建的文件夹
            AssetDatabase.Refresh();

            // 根据创建结果显示不同的提示信息
            if (createdCount > 0)
            {
                EditorUtility.DisplayDialog("成功", $"成功创建了 {createdCount} 个新文件夹！", "好的");
            }
            else
            {
                EditorUtility.DisplayDialog("提示", "所有选中的文件夹均已存在，无需创建。", "好的");
            }
        }
    }
}