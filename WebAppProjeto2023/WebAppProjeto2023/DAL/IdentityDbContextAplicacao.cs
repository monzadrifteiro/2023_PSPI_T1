using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebAppProjeto2023.Areas.Seguranca.Data;

namespace WebAppProjeto2023.DAL
{
    public class IdentityDbContextAplicacao : IdentityDbContext<Usuario>
    {
        public IdentityDbContextAplicacao() : base("IdentityDB")
        { }
        static IdentityDbContextAplicacao()
        {
            Database.SetInitializer<IdentityDbContextAplicacao>(new IdentityDbInit());
        }
        public static IdentityDbContextAplicacao Create()
        {
            return new IdentityDbContextAplicacao();
        }

        public System.Data.Entity.DbSet<WebAppProjeto2023.Areas.Seguranca.Data.Usuario> Usuarios { get; set; }
    }
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<IdentityDbContextAplicacao>{}
}