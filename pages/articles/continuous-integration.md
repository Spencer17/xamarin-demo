# Continious integration

**EduCATS** project uses [Bitrise](https://app.bitrise.io/app/40deffc8ec9f68cb) for automatic deployment.

There are two main workflows: **[build](#build-steps)** and **[release](#release-steps)**.

## build steps

- Activate SSH key;
- Clone git repository;
- Install certificates and profiles;
- Restore NuGet packages;
- Build and archive iOS and Android projects;
- Run Unit Tests;
- Upload code coverage to Codecov.

## release steps

- Activate SSH key;
- Clone git repository;
- Install certificates and profiles;
- Restore NuGet packages;
- Run Unit Tests;
- Upload code coverage to Codecov;
- Set version and build numbers for iOS;
- Set version and build numbers for Android;
- Build and archive iOS and Android projects;
- Sign output *.aab;
- Deploy to Google Play beta;
- Deploy to iTunes Connect;
- Download keystore;
- Export universal *.apk from *.aab;
- Deploy *.apk to Bitrise artifacts;
- Create GitHub release with *.apk;
- Generate and push docs to the current branch with skipping build for this commit.
