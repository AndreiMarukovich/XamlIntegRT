using System.Collections.Generic;
using XamlIntegRT;

namespace TestApp
{
    public class XamlFilesData : IXamlFilesData
    {
        public Dictionary<string, string> FilesToVerify { get; private set; }

        public XamlFilesData()
        {
            FilesToVerify = new Dictionary<string, string>
            {
                		{"App.xaml", "D9bXUIlfGE615HkPoktrzwOjf7U="},
		{"MainPage.xaml", "qKtb/tqoYvuxUmhclT8Lf98UHDA="},
		{"StandardStyles.xaml", "GTogVOQhFAUgJH6TKy4BcExKqoI="},

            };
        }
    }
}