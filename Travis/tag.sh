#! /bin/sh

git config --local user.name "traviscibot"
git config --local user.email "traviscibot@travisci.org"
git tag "$RELEASE_GIT_TAG"
