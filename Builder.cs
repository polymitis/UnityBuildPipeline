using System;
using System.Collections.Generic;
using System.IO;
using Nordeus.Build.Reporters;
using UnityEditor;

namespace Nordeus.Build
{
	public static class Builder
	{
		/// <summary>
		/// Builds an APK at the specified path with the specified texture compression.
		/// </summary>
		/// <param name="buildPath">Path for the output APK.</param>
		/// <param name="textureCompression">If not null, will override the texture compression subtarget.</param>
		public static void BuildAndroid(string buildPath, MobileTextureSubtarget? textureCompression = null, bool devBuild = false)
		{
			if (textureCompression != null)
			{
				EditorUserBuildSettings.androidBuildSubtarget = textureCompression.Value;
			}

			var options = new BuildPlayerOptions();
            options.scenes = GetEnabledScenePaths().ToArray();
			options.locationPathName = buildPath;
			options.target = BuildTarget.Android;
			options.options = BuildOptions.AcceptExternalModificationsToPlayer;
			if (devBuild) options.options |= BuildOptions.Development;

			var report = BuildPipeline.BuildPlayer(options);
			string buildMessage = report.ToString();

			if (string.IsNullOrEmpty(buildMessage)) BuildReporter.Current.IndicateSuccessfulBuild();
			else
			{
				BuildReporter.Current.Log(buildMessage, BuildReporter.MessageSeverity.Error);
			}
		}

		/// <summary>
		/// Builds an XCode project at the specified path.
		/// </summary>
		/// <param name="buildPath">Path for the XCode project.</param>
		public static void BuildIos(string buildPath, bool devBuild = false)
		{
			var options = new BuildPlayerOptions();
			options.scenes = GetEnabledScenePaths().ToArray();
			options.locationPathName = buildPath;
			options.target = BuildTarget.iOS;
			options.options = BuildOptions.AcceptExternalModificationsToPlayer;
			if (devBuild) options.options |= BuildOptions.Development;

			var report = BuildPipeline.BuildPlayer(options);
			string buildMessage = report.ToString();

			if (string.IsNullOrEmpty(buildMessage)) BuildReporter.Current.IndicateSuccessfulBuild();
			else
			{
				BuildReporter.Current.Log(buildMessage, BuildReporter.MessageSeverity.Error);
			}
		}

		/// <summary>
		/// Returns a list of all the enabled scenes.
		/// </summary>
		private static List<string> GetEnabledScenePaths()
		{
			List<string> scenePaths = new List<string>();

			foreach (var scene in EditorBuildSettings.scenes)
			{
				scenePaths.Add(scene.path);
			}

			return scenePaths;
		}

		/// <summary>
		/// Builds the specified build target.
		/// </summary>
		/// <param name="parsedBuildTarget">Build target to build.</param>
		/// <param name="buildPath">Output path for the build.</param>
		/// <param name="parsedTextureSubtarget">Texture compression subtarget for Android.</param>
		public static void Build(BuildTarget parsedBuildTarget, string buildPath, MobileTextureSubtarget? parsedTextureSubtarget = null, bool devBuild = false)
		{
			Directory.CreateDirectory(buildPath);

			switch (parsedBuildTarget)
			{
				case BuildTarget.Android:
					BuildAndroid(buildPath, parsedTextureSubtarget, devBuild);
					break;
				case BuildTarget.iOS:
					BuildIos(buildPath, devBuild);
					break;
				default:
					throw new ArgumentException(parsedBuildTarget + " is not a supported build target.");
			}
		}
	}
}