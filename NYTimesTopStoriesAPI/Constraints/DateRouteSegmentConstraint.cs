using Nancy.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI.Constraints
{
    public class DateRouteSegmentConstraint : RouteSegmentConstraintBase<string>
    {
        public override string Name
        {
            get { return "date"; }
        }

        protected override bool TryMatch(string constraint, string segment, out string matchedValue)
        {
            var regex = @"\d{4}-\d{2}-\d{2}";

            if (Regex.Match(segment, regex, RegexOptions.IgnoreCase).Success)
            {
                matchedValue = segment;
                return true;
            }

            matchedValue = null;
            return false;
        }
    }
}
