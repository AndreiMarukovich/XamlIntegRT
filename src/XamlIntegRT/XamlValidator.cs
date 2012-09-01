using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Search;

namespace XamlIntegRT
{
    public static class XamlValidator
    {
        public async static Task<bool> Validate(IXamlFilesData filesData)
        {
            var sha1 = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
            var sha1Hash = sha1.CreateHash();

            var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".xaml" }) {FolderDepth = FolderDepth.Deep};
            var query = Package.Current.InstalledLocation.CreateFileQueryWithOptions(queryOptions);
            var files = await query.GetFilesAsync();

            foreach (var file in files)
            {
                string hash;
                if (filesData.FilesToVerify.TryGetValue(file.Name, out hash))
                {
                    var xaml = (await FileIO.ReadTextAsync(file)).Trim();
                    var buff = CryptographicBuffer.ConvertStringToBinary(xaml, BinaryStringEncoding.Utf16BE);
                    sha1Hash.Append(buff);
                    var newHash = CryptographicBuffer.EncodeToBase64String(sha1Hash.GetValueAndReset());

                    if (newHash != hash)
                        return false;
                }
            }

            return true;
        }
    }
}
