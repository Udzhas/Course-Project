using System;
using System.Collections.Generic;
using System.Text;

namespace Course_Project
{
    class YouTubeBloger:Bloger
    {
        public int averageCountOFViewers { get; set; }

        public YouTubeBloger(string name, int countOfSubs, int countOfPosts, int averageCountOFViewers) : base(name, countOfSubs, countOfPosts)
        {
            if (averageCountOFViewers >= 0)
            {
                this.averageCountOFViewers = averageCountOFViewers;
            }
            else
            {
                throw new ArgumentException("Average number of viewers can not be negative!");
            }
        }
    }
}
