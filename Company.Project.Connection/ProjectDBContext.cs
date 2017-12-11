using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Company.Project.Connection
{
    public partial class ProjectDBContext : DbContext, IDisposable
    {
        public ProjectDBContext()
            : base("DefaultConnection")
        {
        }

        static ProjectDBContext()
        {
        }

        public static ProjectDBContext Create()
        {
            return new ProjectDBContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
