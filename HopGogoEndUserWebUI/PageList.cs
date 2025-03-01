namespace HopGogoEndUserWebUI;

sealed record PageRouteInfo(string Url, Type page);

sealed class PageList
{
    // H o m e
    public static readonly PageRouteInfo Home = new("/", typeof(PagePackageDetail));
}