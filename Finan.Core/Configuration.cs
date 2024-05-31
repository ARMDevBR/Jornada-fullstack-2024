using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Core
{
    public static class Configuration
    {
        public const int DefaultStatusCode = 200;
        public const int FirstSuccessfulStatusCode = 200;
        public const int LastSuccessfulStatusCode = 299;
        public const int DefaultCurrentPage = 1;
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 25;
    }
}
