#! /bin/bash

set -e

BUILD_TARGET=$1

BASE_URL=https://download.unity3d.com/download_unity
HASH=fc0fe30d6d91
VERSION=2018.3.8f1

download() {
  file=$1
  url="$BASE_URL/$HASH/$package"

  echo "Downloading from $url: "
  cd Unity
  curl -o `basename "$package"` "$url"
  cd ../
}

install() {
  package=$1
  filename=`basename "$package"`
  packagePath="Unity/$filename"
  if [ ! -f $packagePath ] ; then
    echo "$packagePath not found. downloading `basename "$packagePath"`"
    download "$package"
  fi

  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package $packagePath -target /
}

if [ ! -d "Unity" ] ; then
  mkdir -p -m 777 Unity
fi

install "MacEditorInstaller/Unity.pkg"
if [ "$BUILD_TARGET" = "windows" ]; then install "MacEditorTargetInstaller/UnitySetup-Windows-Mono-Support-for-Editor-$VERSION.pkg" ;fi
if [ "$BUILD_TARGET" = "linux" ];   then install "MacEditorTargetInstaller/UnitySetup-Linux-Support-for-Editor-$VERSION.pkg"        ;fi
if [ "$BUILD_TARGET" = "webgl" ];   then install "MacEditorTargetInstaller/UnitySetup-WebGL-Support-for-Editor-$VERSION.pkg"        ;fi
if [ "$BUILD_TARGET" = "android" ]; then install "MacEditorTargetInstaller/UnitySetup-Android-Support-for-Editor-$VERSION.pkg"      ;fi
if [ "$BUILD_TARGET" = "ios" ];     then install "MacEditorTargetInstaller/UnitySetup-iOS-Support-for-Editor-$VERSION.pkg"          ;fi
