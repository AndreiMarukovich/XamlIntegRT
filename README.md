XamlIntegRT
===========

XamlIntegRT helps to prevent unauthorized modifications of XAML files for Windows 8 apps.
It includes command line hash codes generator and C# library for XAML files validation.

## Usage

### 1. Add XamlIntegRT library to project using NuGet
    PM> Install-Package XamlIntegRT

### 2. Download XAML hash data generator
    https://github.com/downloads/AndreiMarukovich/XamlIntegRT/XamlIntegRTGen.exe

### 3. Generate hash data for XAML files
    XamlIntegRTGen.exe <project_folder_path> <file_path> <namespace> [/ads]

    <project_folder_path> - path to project source files, e.g. C:\sandbox\BookReader\src
    <file_path> - path to project source files, e.g. C:\sandbox\BookReader\src\App\XamlData.cs
    <namespace> - desired namespace to place data in
    /ads - optional parameter to hash XAML file with AdControls only

### 4. Add generated .cs file to your project

### 5. Use XamlIntegRT to validate XAML during run-time
    async void MainPageLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        if (!await XamlValidator.Validate(new XamlFilesData()))
        {
            var dialog = new MessageDialog("XAML modifications detected");
            await dialog.ShowAsync();
        }
    }

### 6. Integrate XamlIntegRTGen.exe into your build process
To automatically update hash codes for modified XAML files you can call XamlIntegRTGen.exe from your build script or add this call to project options as pre-build event.

