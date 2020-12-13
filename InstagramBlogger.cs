using System;
using System.Collections.Generic;
using System.Text;

namespace Course_Project
{
    class InstagramBlogger:Bloger
    {
        public int postsPerDay { get; set; }

        public InstagramBlogger(string name, int countOfSubs, int countOfPosts, int postsPerDay) : base(name, countOfSubs, countOfPosts)
        {
            if (postsPerDay >= 0)
            {
                this.postsPerDay = postsPerDay;
            }
            else
            {
                throw new ArgumentException("Number of posts can not be negative!");
            }
        }

    }
}
