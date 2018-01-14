// Code may be different between EF6-SQLite and EF6-MySQL
// Use compiler directive to control different dialect
#if MYSQL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAssignment.Persistence {
    public partial class Assign {
    }
}
#endif