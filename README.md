# WindowsFormsAero

WindowsFormsAero is a *Windows Forms* library that provides native controls with many of the modern features introduced with Windows Vista and more recent Windows versions.
Many controls—such as buttons, command buttons, and textboxes—support the functional and stylistic features introduced with “Aero”.
For instance shield icons, cue banners, and so on.

WindowsFormsAero was started by [Marco Minerva](https://github.com/marcominerva) in January 2007 and was initially hosted on [Codeplex](http://windowsformsaero.codeplex.com).

## Download

[![NuGet](https://img.shields.io/nuget/v/Windows-Forms-Aero.svg)](https://www.nuget.org/packages/Windows-Forms-Aero)

Get the latest version through NuGet:

```
Install-Package Windows-Forms-Aero
```

## Version history

### 3.1

* Add support for additional DWM window attributes (`DWMWA_CLOAKED` and `DWMWA_FREEZE_REPRESENTATION`).
* Add support for public Virtual Desktop APIs (Windows&nbsp;10).

### 3.0.1

* Add simple `StoreAppHelper.IsRunningAsStoreApp()` helper to check whether a program is running as a packaged Windows Store app.
* Add .NET&nbsp;4.0 support.
* Add XML documentation to NuGet.

### 3.0

First release after migration to GitHub.
* Breaking changes from v2.*.
* Major code refactoring and clean up.
* Minor memory leaks fixed.
* Progress bars now correctly change state.

## Contributors

* [Marco Minerva](https://github.com/marcominerva)
* [Lorenz Cuno Klopfenstein](https://github.com/lorenzck)
* [Julie Koubová](https://github.com/juliekoubova)
* Blake Pell
* multippt
* Nicholas Kwan
