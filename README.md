# WinDash
WinDash is a Dashboard/Welcome screen replacement for Windows 10 IoT Core.

## Intro

This project aims to replace the horribly written and utterly disfunctional dashboard shipped with Windows 10 IoT Core. Initially it will bring only the functions included within the stock dashboard, but later on it will be expanded.

Planned functions:
- Application list (Application Launcher)
- Screensaver
- More settings options (above network and language)

Further functions might be implemented.

Versioning:

    [Major].[Minor].[Sub].[Build]

Example:

    1.0.5.20160128 - Version 1.0.5, Build 20160128

Build number always corresponds with the build date (appended with an extra number if there are more builds a day).

## Status & Changelog

### v0.0.1.20151110

- Initial project setup
- VERY basic functionality:
  - Device information (board name, board type, board image)
  - Network information (current network name)
  - OS information (OS version)
- Basic MVVM structure
- Basic UI layout using the AppShell model


## Dependencies

WinDash aims to require the least possible dependencies - especially third parties. However for some advanced features, it is unavoidable to add some libraries.

These additions include:

- Json.Net 7.0.1
- MVVMLight Libs 5.2.0

## Thanks

A big thanks goes to Microsoft - if they didn't screw up the Dashboard so badly, I'd be sitting bored thinking what to write for my first IoT project. Thanks to them, now I'm not bored, and actually have a nice long project to work on!

Also, MS guys, thanks for the many many code samples for accessing and writing various information about the device.
