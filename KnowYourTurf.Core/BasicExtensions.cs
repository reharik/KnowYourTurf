namespace KnowYourTurf.Core
{
    public static class KYTBasicExtensions 
    {
        public static string AddImageSizeToName(string imageNameOrUrl, string imageSize)
        {
            return imageNameOrUrl.Insert(imageNameOrUrl.LastIndexOf("."), "_" + imageSize);
        }
    }
}