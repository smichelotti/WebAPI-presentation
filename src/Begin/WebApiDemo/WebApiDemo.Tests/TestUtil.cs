using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Tests
{
    static class TestUtil
    {
        public static List<Tag> CreateTagsList()
        {
            return new List<Tag>
            {
                new Tag { Name = "Ball Handling" },
                new Tag { Name = "Passing" },
                new Tag { Name = "Shooting" },
                new Tag { Name = "Rebounding" },
                new Tag { Name = "Transition" },
                new Tag { Name = "Defense" },
                new Tag { Name = "Team Offense" },
                new Tag { Name = "Team Defense" }
            };
        }

        public static Tag CreateValidTag()
        {
            return new Tag { Name = "Defensive Transition" };
        }
    }
}
