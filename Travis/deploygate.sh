#! /bin/bash

DG_USER=$1
DG_TOKEN=$2

if [ "$TRAVIS_BRANCH" = "develop" -a "$TRAVIS_PULL_REQUEST" = "false" ]; then
  curl -F "file=@./Build/android.apk" -F "token=$DG_TOKEN" https://deploygate.com/api/users/$DG_USER/apps
fi