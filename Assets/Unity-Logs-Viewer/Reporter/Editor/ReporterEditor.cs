using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

using System.IO;
using System.Collections;


public class ReporterEditor : Editor
{
	[MenuItem("Reporter/Create")]
	public static void CreateReporter()
	{
		const int ReporterExecOrder = -12000;
		GameObject reporterObj = new GameObject();
		reporterObj.name = "Reporter";
		Reporter reporter = reporterObj.AddComponent<Reporter>();
		reporterObj.AddComponent<ReporterMessageReceiver>();
		//reporterObj.AddComponent<TestReporter>();
		
		// Register root object for undo.
		Undo.RegisterCreatedObjectUndo(reporterObj, "Create Reporter Object");

		MonoScript reporterScript = MonoScript.FromMonoBehaviour(reporter);
		string reporterPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(reporterScript));

		if (MonoImporter.GetExecutionOrder(reporterScript) != ReporterExecOrder) {
			MonoImporter.SetExecutionOrder(reporterScript, ReporterExecOrder);
			//Palast.Log("Fixing exec order for " + reporterScript.name);
		}

		reporter.images = new Images
		{
			clearImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/clear.png"), typeof(Texture2D)),
			collapseImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/collapse.png"), typeof(Texture2D)),
			clearOnNewSceneImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/clearOnSceneLoaded.png"), typeof(Texture2D)),
			showTimeImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/timer_1.png"), typeof(Texture2D)),
			showSceneImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/UnityIcon.png"), typeof(Texture2D)),
			userImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/user.png"), typeof(Texture2D)),
			showMemoryImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/memory.png"), typeof(Texture2D)),
			softwareImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/software.png"), typeof(Texture2D)),
			dateImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/date.png"), typeof(Texture2D)),
			showFpsImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/fps.png"), typeof(Texture2D)),
			infoImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/info.png"), typeof(Texture2D)),
			searchImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/search.png"), typeof(Texture2D)),
			closeImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/close.png"), typeof(Texture2D)),
			buildFromImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/buildFrom.png"), typeof(Texture2D)),
			systemInfoImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/ComputerIcon.png"), typeof(Texture2D)),
			graphicsInfoImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/graphicCard.png"), typeof(Texture2D)),
			backImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/back.png"), typeof(Texture2D)),
			logImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/log_icon.png"), typeof(Texture2D)),
			warningImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/warning_icon.png"), typeof(Texture2D)),
			errorImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/error_icon.png"), typeof(Texture2D)),
			barImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/bar.png"), typeof(Texture2D)),
			button_activeImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/button_active.png"), typeof(Texture2D)),
			even_logImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/even_log.png"), typeof(Texture2D)),
			odd_logImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/odd_log.png"), typeof(Texture2D)),
			selectedImage = (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/selected.png"), typeof(Texture2D)),
			reporterScrollerSkin = (GUISkin) AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/reporterScrollerSkin.guiskin"), typeof(GUISkin))
		};
		//reporter.images.graphImage           = (Texture2D)AssetDatabase.LoadAssetAtPath(Path.Combine(reporterPath, "Images/chart.png"), typeof(Texture2D));

	}
}

public class ReporterModificationProcessor : UnityEditor.AssetModificationProcessor
{
	[InitializeOnLoad]
	public class BuildInfo
	{
		static BuildInfo()
		{
			EditorApplication.update += Update;
		}

		static bool isCompiling = true;
		static void Update()
		{
			if (!EditorApplication.isCompiling && isCompiling) {
				//Palast.Log("Finish Compile");
				if (!Directory.Exists(Application.dataPath + "/StreamingAssets")) {
					Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");
				}
				string info_path = Application.dataPath + "/StreamingAssets/build_info.txt";
				StreamWriter build_info = new StreamWriter(info_path);
//				build_info.Write("Build from " + SystemInfo.deviceName + " at " + System.DateTime.Now.ToString());
				build_info.Close();
			}

			isCompiling = EditorApplication.isCompiling;
		}
	}
}
