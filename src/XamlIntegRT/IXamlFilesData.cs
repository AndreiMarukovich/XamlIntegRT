using System.Collections.Generic;

namespace XamlIntegRT
{
    public interface IXamlFilesData
    {
        Dictionary<string, string> FilesToVerify { get; }
    }
}