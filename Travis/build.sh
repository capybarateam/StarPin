#! /bin/sh

# Example build script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# Change this the name of your project. This will be the name of the final executables as well.
project="${APP_NAME}"
# DATE=`date +%y.%m.%d`
# versionName="a.${DATE}"

BUILD_TARGET=$1

if [ "$BUILD_TARGET" = "windows" ]; then
  echo "Attempting to build $project for Windows"
  /Applications/Unity/Unity.app/Contents/MacOS/Unity \
    -batchmode \
    -nographics \
    -silent-crashes \
    -logFile \
    -serial ${UNITY_SERIAL_NUMBER} \
    -username ${UNITY_USER_NAME} \
    -password ${UNITY_USER_PASSWORD} \
    -projectPath $(pwd)/ \
    -executeMethod BuildScript.Windows \
    -quit
fi

if [ "$BUILD_TARGET" = "osx" ]; then
  echo "Attempting to build $project for OS X"
  /Applications/Unity/Unity.app/Contents/MacOS/Unity \
    -batchmode \
    -nographics \
    -silent-crashes \
    -logFile \
    -serial ${UNITY_SERIAL_NUMBER} \
    -username ${UNITY_USER_NAME} \
    -password ${UNITY_USER_PASSWORD} \
    -projectPath $(pwd)/ \
    -executeMethod BuildScript.OSX \
    -quit
fi

if [ "$BUILD_TARGET" = "linux" ]; then
  echo "Attempting to build $project for Linux"
  /Applications/Unity/Unity.app/Contents/MacOS/Unity \
    -batchmode \
    -nographics \
    -silent-crashes \
    -logFile \
    -serial ${UNITY_SERIAL_NUMBER} \
    -username ${UNITY_USER_NAME} \
    -password ${UNITY_USER_PASSWORD} \
    -projectPath $(pwd)/ \
    -executeMethod BuildScript.Linux \
    -quit
fi

if [ "$BUILD_TARGET" = "webgl" ]; then
  echo "Attempting to build $project for WebGL"
  /Applications/Unity/Unity.app/Contents/MacOS/Unity \
    -batchmode \
    -nographics \
    -silent-crashes \
    -logFile \
    -serial ${UNITY_SERIAL_NUMBER} \
    -username ${UNITY_USER_NAME} \
    -password ${UNITY_USER_PASSWORD} \
    -projectPath $(pwd)/ \
    -quit \
    -executeMethod BuildScript.WebGL
fi

if [ "$BUILD_TARGET" = "android" ]; then
  echo "Attempting to build $project for Android"
  /Applications/Unity/Unity.app/Contents/MacOS/Unity \
    -batchmode \
    -nographics \
    -silent-crashes \
    -logFile \
    -serial ${UNITY_SERIAL_NUMBER} \
    -username ${UNITY_USER_NAME} \
    -password ${UNITY_USER_PASSWORD} \
    -projectPath $(pwd)/ \
    -quit \
    -executeMethod BuildScript.Android
fi

if [ "$BUILD_TARGET" = "ios" ]; then
  echo "Attempting to build $project for iOS"
  /Applications/Unity/Unity.app/Contents/MacOS/Unity \
     -batchmode \
     -nographics \
     -silent-crashes \
     -logFile \
     -serial ${UNITY_SERIAL_NUMBER} \
     -username ${UNITY_USER_NAME} \
     -password ${UNITY_USER_PASSWORD} \
     -projectPath $(pwd)/ \
     -quit \
     -executeMethod BuildScript.iOS
fi

echo 'Attempting to zip builds'
cd $(pwd)/Build
if [ "$BUILD_TARGET" = "windows" ]; then zip -r windows.zip windows/                ;fi
if [ "$BUILD_TARGET" = "osx" ];     then hdiutil create osx.dmg -srcfolder osx/ -ov ;fi
if [ "$BUILD_TARGET" = "linux" ];   then tar -czvf linux.tar.gz linux/              ;fi
if [ "$BUILD_TARGET" = "ios" ];     then zip -r ios.zip ios/                        ;fi
