# Unity Build Pipeline

This is a Unity 2019.2.x build pipeline based on the Nordeus/UnityBuildPipeline. It is capable of building Android and iOS. When building, Unity should be started with the following parameters:
```sh
unity.exe -projectPath "pathToYourProject" -batchMode -quit -executeMethod Nordeus.Build.CommandLineBuild.Build <aditional build parameters>
```

Aditional required build parameters can be:
  - **-target** - Platform for the build, it can be iOS or Android
  
Aditional optional build parameters can be:
  - **-out** - Output path for the built project
  - **-textureCompression** - Texture compression subtarget for Android (ETC1, ATC, ETC2...)
  - **-buildNumber** - Integer value for the number of the build. It is copied to Android's bundle version code and iOS's build number.
  - **-buildVersion** - Pretty build version string (eg. 1.2.3f). It is copied to bundle version on both Android and iOS.
  - **-development** - 1 enables a development build, otherwise not.
  - **-reporter** - Build report to use. By default UnityReporter is used which reprots all emssages to Unity's console. TeamCityReporter also reports errors as service messages to TeamCity (This is from the original fork and has not been used/tested).
