#! /bin/bash

echo "Attempting to Install Android SDK and NDK"
brew tap homebrew/cask
brew tap homebrew/cask-versions 
brew cask uninstall java # uninstall java9 
brew cask install java8 # install java8 
mkdir -p ~/.android/
touch ~/.android/repositories.cfg
brew cask install android-sdk
brew cask install android-ndk
yes | sdkmanager --licenses > /dev/null
sdkmanager "platform-tools" "platforms;android-28" "build-tools;28.0.3" > /dev/null