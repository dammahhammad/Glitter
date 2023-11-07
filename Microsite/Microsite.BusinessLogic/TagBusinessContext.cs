using Microsite.Data;
using Microsite.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsite.BusinessLogic
{
    public class TagBusinessContext
    {
        private TagDbContext tagdbcontext;

        public TagBusinessContext()
        {
            tagdbcontext = new TagDbContext(); 
        }
        public bool NewTag(NewTweetDTO tweet)
        {
            string[] message = tweet.Message.Split(' ');
            List<string> tags = new List<string>();
            foreach (string tag in message)
            {
                if (tag.Contains('#')) { tags.Add(tag);}
            }
            if(tags.Count > 0)
            {
                bool result = tagdbcontext.AddTag(tags, tweet.Id);
                return result;
            }
            return false; 
        }
    }
}
