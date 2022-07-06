using CodeHollow.FeedReader;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace RSSFeedReader.RobotCodes.Modules
{
    public class FeedComparer : IEqualityComparer<FeedItem>
    {
        public bool Equals(FeedItem x, FeedItem y)
        {
            if (x.Link == y.Link)
                return true;

            return false;
        }

        public int GetHashCode([DisallowNull] FeedItem obj)
        {
            return obj.Link.GetHashCode();
        }
    }
}