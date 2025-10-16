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
    /// ---------------------------------
    /// - 使用方法:
    ///   1. 将此脚本放置在项目的 Editor 文件夹下。
    ///   2. 在 Unity 顶部菜单栏选择 "QFramework/Tools/Folder Structure Creator"。
    ///   3. 在弹出的窗口中，勾选需要创建的文件夹组。
    ///   4. 点击 "一键生成" 按钮。
    ///
    /// - 依赖:
    ///   - 需要项目中已导入 Odin Inspector 插件。
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

        [ToggleGroup("FolderGroups", "美术资源 (Art)", Order = 1)]
        [GUIColor(0.9f, 0.9f, 1f)]
        public bool createArt = true;

        [ToggleGroup("FolderGroups", Order = 1)]
        [HideLabel, ShowIf("createArt")]
        [DetailedInfoBox(
            "Art/Animations: 动画控制器、动画片段\n" +
            "Art/Audio: 音频文件 (Music/Sound)\n" +
            "Art/Fonts: 字体文件\n" +
            "Art/Materials: 材质球\n" +
            "Art/Models: 3D模型\n" +
            "Art/Prefabs: 预制体\n" +
            "Art/Shaders: 着色器\n" +
            "Art/Sprites: 2D精灵/UI图片\n" +
            "Art/Textures: 贴图", null,
            InfoMessageType.None)]
        // 这个虚拟字段只是为了显示下面的 DetailedInfoBox
        private readonly bool _artDescriptionDummy;


        [ToggleGroup("FolderGroups", "代码脚本 (Scripts)", Order = 2)]
        [GUIColor(0.9f, 1f, 0.9f)]
        public bool createScripts = true;

        [ToggleGroup("FolderGroups", Order = 2)]
        [HideLabel, ShowIf("createScripts")]
        [DetailedInfoBox(
            "Scripts/Architecture: 框架核心结构 (System, Model, Controller, Command)\n" +
            "Scripts/Editor: 编辑器脚本\n" +
            "Scripts/Runtime: 游戏运行时逻辑\n" +
            "Scripts/UI: UI界面逻辑", null,
            InfoMessageType.None)]
        private readonly bool _scriptsDescriptionDummy;

        [ToggleGroup("FolderGroups", "核心目录 (Core)", Order = 3)]
        [GUIColor(1f, 0.9f, 0.9f)]
        public bool createCore = true;

        [ToggleGroup("FolderGroups", Order = 3)]
        [HideLabel, ShowIf("createCore")]
        [DetailedInfoBox(
            "Scenes: 游戏场景文件\n" +
            "Resources: 需要通过 Resources.Load 加载的资源\n" +
            "Settings: 各种 ScriptableObject 配置文件", null,
            InfoMessageType.None)]
        private readonly bool _coreDescriptionDummy;

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
                    "Scripts/Architecture", "Scripts/Editor", "Scripts/Runtime", "Scripts/UI"
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

            // 操作完成后自动关闭窗口
            this.Close();
        }

        /// <summary>
        /// 遍历列表并创建所有文件夹
        /// </summary>
        private void CreateAllFolders(List<string> folders)
        {
            int createdCount = 0;
            foreach (var folder in folders)
            {
                // 使用 Path.Combine 组合路径，避免平台差异
                var path = Path.Combine(Application.dataPath, folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    createdCount++;
                }
            }

            // 刷新 AssetDatabase 以在 Unity 编辑器中显示新文件夹
            AssetDatabase.Refresh();

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