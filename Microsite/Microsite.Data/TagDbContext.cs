using Microsite.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsite.Data
{
    public class TagDbContext
    {
        readonly MicrositeDbContext DbContext;
        public TagDbContext()
        {
            DbContext = new MicrositeDbContext();
        }

        public bool AddTag(List<string> tag, int Id)
        {
            foreach (string s in tag)
            {
                TagDTO newtag = new()
                {
                    TweetId = Id,
                    TagName = s
                };
                DbContext.Tag.Add(newtag);
                DbContext.SaveChanges();
            }
            return true;
        }
    }
}
