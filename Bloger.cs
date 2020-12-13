using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Course_Project
{
    [Table("Blogers")]
    abstract class Bloger
    {
        [Key]
        public string name { get; set; }
        public int countOfSubs { get; set; }
        public int countOfPosts { get; set; }

        public Bloger(string name, int countOfSubs, int countOfPosts)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.name = name;
            }
            else
            {
                throw new ArgumentException("Name can not be null!");
            }
            if (countOfSubs > 0)
            {
                this.countOfSubs = countOfSubs;
            }
            else
            {
                throw new ArgumentException("Number of subscribers can not be null or negative!");
            }
            if (countOfPosts > 0)
            {
                this.countOfPosts = countOfPosts;
            }
            else
            {
                throw new ArgumentException("Number of posts can not be null or negative!");
            }
        }
    }
}
