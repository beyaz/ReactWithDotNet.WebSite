namespace HopGogoEndUserWebUI;

sealed record PageRouteInfo(string Url, Type page);

static class Routing
{
    // H o m e
    public static readonly PageRouteInfo Home = new("/", typeof(PagePackageDetail));
}